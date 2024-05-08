namespace JWTProject
{
    public class appsettings
    {
        public Jwt Jwt { get; set; }
        public string ConnectionStrings { get; set; }

    }
    public class Jwt
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string Secret { get; set; }
        public int ClockSkew { get; set; }
        public int ExpiresIn { get; set; }
    }
}
