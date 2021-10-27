namespace SDLCSimulator_BusinessLogic.Models.General
{
    public class EmailNotificationsConfig
    {
        public string SmtpServerHostname { get; set; }
        public int SmtpServerPort { get; set; }
        public string AddressFrom { get; set; }
        public bool AnonymousAuth { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool EnableSSL { get; set; }
    }
}
