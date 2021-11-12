using SDLCSimulator_BusinessLogic.Models.General;

namespace SDLCSimulator_Common.Fixtures
{
    public static class UpdateUserInfoModelFixture
    {
        public static UpdateUserInfoModel CreateValidEntity()
        {
            return new()
            {
                FirstName = "Андрій",
                LastName = "Федецький"
            };
        }
    }
}
