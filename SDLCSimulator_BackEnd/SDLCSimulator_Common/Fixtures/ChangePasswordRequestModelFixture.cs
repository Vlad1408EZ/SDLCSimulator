using SDLCSimulator_BusinessLogic.Models.General;

namespace SDLCSimulator_Common.Fixtures
{
    public static class ChangePasswordRequestModelFixture
    {
        public static ChangePasswordRequestModel CreateValidEntity()
        {
            return new()
            {
                OldPassword = "student1234",
                NewPassword = "student123",
                ConfirmPassword = "student123"
            };
        }
    }
}
