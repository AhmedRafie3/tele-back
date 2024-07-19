namespace TeleperformanceTask
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string Audance { get; set; }
        public int LifeTime { get; set; }
        public string SigningKey { get; set; }
    }
}
