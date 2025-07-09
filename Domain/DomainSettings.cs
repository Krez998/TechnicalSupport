namespace Domain
{
    public class DomainSettings
    {
        public string Secret { get; set; } = default!;
    }

    public class PasswordHashingSettings
    {
        public int Iterations { get; set; } = 100_000;
        public int SaltSize { get; set; } = 16;
        public int KeySize { get; set; } = 32;
        public string Algorithm { get; set; } = "PBKDF2";
        public int WorkFactor { get; set; } = 12;
    }
}
