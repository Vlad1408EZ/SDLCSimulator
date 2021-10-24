namespace SDLCSimulator_BusinessLogic.Models.Configuration
{
    public class JwtConfig
    {
        public string Key { get; set; }

        public int ExpirationDate { get; set; }

        public string Issuer { get; set; }
    }
}
