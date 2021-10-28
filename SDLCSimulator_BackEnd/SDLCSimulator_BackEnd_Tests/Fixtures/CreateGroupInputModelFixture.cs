using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDLCSimulator_BusinessLogic.Models.Input;

namespace SDLCSimulator_BackEnd_Tests.Fixtures
{
    public static class CreateGroupInputModelFixture
    {
        public static CreateGroupInputModel CreateValidEntity()
        {
            return new() {GroupName = "ПЗ-41"};
        }
    }
}
