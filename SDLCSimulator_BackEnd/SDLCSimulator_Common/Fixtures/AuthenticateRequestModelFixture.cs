using SDLCSimulator_BusinessLogic.Models.General;

namespace SDLCSimulator_Common.Fixtures
{
    public static class AuthenticateRequestModelFixture
    {
        public static AuthenticateRequestModel CreateValidEntity()
        {
            return new()
            {
                Email = "іван.іванов.пз@lpnu.ua",
                Password = "student1234"
            };
        }
    }
}
