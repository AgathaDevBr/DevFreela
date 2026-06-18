namespace DevFreela.API.Authentication
{
    public class JwtOptions
    {
        public const string SectionName = "Jwt";

        public string Issuer { get; set; } = "DevFreela";
        public string Audience { get; set; } = "DevFreela";
        public string SecretKey { get; set; } = string.Empty;
        public int ExpirationMinutes { get; set; } = 60;
        public string DemoPassword { get; set; } = string.Empty;
    }
}
