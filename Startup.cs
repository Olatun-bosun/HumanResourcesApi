using HumanResourcesApi.Data.Repositories;
using HumanResourcesApi.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static System.Collections.Specialized.BitVector32;

namespace HumanResourcesApi
{
    public class Startup
    {
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            //var builder = WebApplication.CreateBuilder(Main.args);

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            services.AddScoped<Repository>();
            // configure strongly typed settings objects
            //           builder.Services.AddDbContext<DataContext>(options =>
            //options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerDb")));

            var section = Configuration.GetSection("ConnectionStrings");

            var settings = section.Get<Settings>();
            services.Configure<Settings>(section);

            services.AddDbContext<DataContext>(options =>
            {
                options
                    .UseSqlServer(settings.SqlServerDb);
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder =>
                    {
                       
                            builder.AllowAnyOrigin()
                                   .AllowAnyHeader()
                                   .SetIsOriginAllowed((host) => true)
                                   .AllowAnyMethod();
                       
                    });
            });

            services.AddSingleton(settings);
            services.AddHttpContextAccessor();
            // Other service configurations...
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors("AllowOrigin");
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();

            app.UseCorsMiddleware();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //app.Run();

            // Other middleware configurations...
        }
    }
}