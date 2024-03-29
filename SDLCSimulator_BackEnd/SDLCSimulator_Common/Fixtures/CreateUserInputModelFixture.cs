﻿using System.Collections.Generic;
using SDLCSimulator_BusinessLogic.Models.Input;
using SDLCSimulator_Data.Enums;

namespace SDLCSimulator_Common.Fixtures
{
    public static class CreateUserInputModelFixture
    {
        public static CreateUserInputModel CreateValidStudent()
        {
            return new()
            {
                FirstName = "Іван",
                LastName = "Іванов",
                Email = "іван.іванов.пз@lpnu.ua",
                Password = "student1234",
                Role = RoleEnum.Student,
                Groups = new List<string> {"ПЗ-41"}
            };
        }

        public static CreateUserInputModel CreateValidTeacher()
        {
            return new()
            {
                FirstName = "Андрій",
                LastName = "Фоменко",
                Email = "андрій.фоменко.викладач@lpnu.ua",
                Password = "teacher1234",
                Role = RoleEnum.Teacher,
                Groups = new List<string> { "ПЗ-41", "ПЗ-42" }
            };
        }
    }
}
