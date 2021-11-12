using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDLCSimulator_Data;
using SDLCSimulator_Data.Enums;

namespace SDLCSimulator_Common.Fixtures
{
    public static class UserFixture
    {
        public static User CreateValidEntity()
        {
            return new()
            {
                Id = 1,
                FirstName = "Іван",
                LastName = "Іванов",
                Email = "іван.іванов.пз@lpnu.ua",
                GroupId = 1,
                Password = "AKXwmMIdR9fEXaikvLavw33r0zyiXHBLBk4MJELb5RNwoyMCsi8NBf8advWXCTQ54A==",
                Role = RoleEnum.Student
            };
        }
    }
}
