using SDLCSimulator_BusinessLogic.Models.General;

namespace SDLCSimulator_BackEnd_Tests.Fixtures
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
