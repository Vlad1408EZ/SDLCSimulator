using SDLCSimulator_BusinessLogic.Models.Input;

namespace SDLCSimulator_Common.Fixtures
{
    public static class CreateGroupInputModelFixture
    {
        public static CreateGroupInputModel CreateValidEntity()
        {
            return new() {GroupName = "ПЗ-41"};
        }
    }
}
