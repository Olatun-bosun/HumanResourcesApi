namespace HumanResourcesApi
{
    public class Settings
    {
        public int JwtExpiresIn { get; set; }
        public string JwtSecret { get; set; }
        public string SqlServerDb { get; set; }
    }
}
