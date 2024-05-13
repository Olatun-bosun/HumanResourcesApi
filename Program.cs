using HumanResourcesApi;
using HumanResourcesApi.Data;
using HumanResourcesApi.Data.Repositories;
using HumanResourcesApi.Helper;
using HumanResourcesApi.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TangWeb_Api", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please Bearer and then token in the field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                });
}); 
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerDb")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<DataContext>();

var apiSettingsSection = builder.Configuration.GetSection("APISettings");
builder.Services.Configure<APISettings>(apiSettingsSection);

var apiSettings = apiSettingsSection.Get<APISettings>();
var key = Encoding.ASCII.GetBytes(apiSettings.SecretKey);

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidAudience = apiSettings.ValidAudience,
        ValidIssuer = apiSettings.ValidIssuer,
        ClockSkew = TimeSpan.Zero
    };
});


builder.Services.AddScoped<Repository>();
builder.Services.AddCors(o => o.AddPolicy("AllowOrigin", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));
var app = builder.Build();

app.UseSwagger();
if (!app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HR Blazor API v1");
        c.RoutePrefix = String.Empty;
    });
}
else
{
    app.UseSwaggerUI(c => {
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowOrigin");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCorsMiddleware();


app.MapControllers();

app.Run();