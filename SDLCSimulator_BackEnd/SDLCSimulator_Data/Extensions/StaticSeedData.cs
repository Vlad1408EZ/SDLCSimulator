using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SDLCSimulator_Data.Enums;
using SDLCSimulator_Data.JsonTaskModels;

namespace SDLCSimulator_Data.Extensions
{
    public static class StaticSeedData
    {
        public static void AddSeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>()
                .HasData(CreateGroup(1,"ПЗ-41"),CreateGroup(2, "ПЗ-42"),CreateGroup(3, "ПЗ-43"),
                    CreateGroup(4, "ПЗ-44"),CreateGroup(5, "ПЗ-45"));

            modelBuilder.Entity<User>()
                .HasData(CreateStudent(1,"Іван","Іванов","іван.іванов.пз@lpnu.ua",
                    "AKXwmMIdR9fEXaikvLavw33r0zyiXHBLBk4MJELb5RNwoyMCsi8NBf8advWXCTQ54A==", RoleEnum.Student,1));

            modelBuilder.Entity<User>()
                .HasData(CreateTeacher(2, "Андрій", "Фоменко", "андрій.фоменко.викладач@lpnu.ua",
                    "AEIatg7ShLybH2927m5UTtOGO2EjSBB6JuXbkhhhUHDIQAH+tKRvN81u9R7ZqhzOcA==", RoleEnum.Teacher));

            modelBuilder.Entity<User>()
                .HasData(CreateAdmin(3, "Сергій", "Федецький", "сергій.федецький.адмін@lpnu.ua",
                    "AGiprihD8YNNbQk2w5XdYqNtbOxu8Qly+7gmJloWjaKPdPWSAHIb2SAbMsaRO08e6Q==", RoleEnum.Admin));

            modelBuilder.Entity<GroupTeacher>()
                .HasData(CreateGroupTeacher(1,2),CreateGroupTeacher(2,2));

            modelBuilder.Entity<Task>()
                .HasData(CreateTask(1, TaskTypeEnum.DragAndDrop, DifficultyEnum.Medium, MaxGradeEnum.MediumGrade, ErrorRateEnum.MediumErrorRate,
                    "Вимоги до системи роботи магазину ювелірних виробів",2));

            modelBuilder.Entity<GroupTask>()
                .HasData(CreateGroupTask(1,1));
        }

        private static Group CreateGroup(int id, string groupName)
        {
            return new() { Id = id,GroupName = groupName };
        }

        private static User CreateStudent(int id, string firstName, string lastName, string email, string password, RoleEnum role, int groupId)
        {
            return new() { Id = id, FirstName = firstName, LastName = lastName, Email = email, Password = password, Role = role, GroupId = groupId };
        }

        private static User CreateTeacher(int id, string firstName, string lastName, string email, string password, RoleEnum role)
        {
            return new() { Id = id, FirstName = firstName, LastName = lastName, Email = email, Password = password, Role = role };
        }

        private static User CreateAdmin(int id, string firstName, string lastName, string email, string password, RoleEnum role)
        {
            return new() { Id = id, FirstName = firstName, LastName = lastName, Email = email, Password = password, Role = role };
        }

        private static Task CreateTask(int id, TaskTypeEnum type, DifficultyEnum difficulty, MaxGradeEnum maxGrade, ErrorRateEnum errorRate,
            string topic, int teacherId)
        {
            return new()
            {
                Id = id,Type = type, Difficulty = difficulty,
                ErrorRate = errorRate,
                MaxGrade = maxGrade,
                Topic = topic, Description = CreateJsonTaskDescription(),
                Standard = CreateJsonTaskStandard(),
                TeacherId = teacherId
            };
        }

        private static GroupTeacher CreateGroupTeacher(int groupId, int teacherId)
        {
            return new() { GroupId = groupId, TeacherId = teacherId};
        }

        private static GroupTask CreateGroupTask(int groupId, int taskId)
        {
            return new() { GroupId = groupId, TaskId = taskId };
        }

        private static string CreateJsonTaskDescription()
        {
            var columns = new List<string> { "Функціональні","Нефункціональні" };
            var blocks = new List<string>
            {
                "Пошук товарів за назвою",
                "Користувацький інтерфейс має відповідати 10 евристикам графічного інтерфейсу",
                "Перегляд вмісту кошику","Вибір способу доставки",
                "Програмний продукт буде пов’язаний з поштовим клієнтом MS Outlook для отримання розсилки електронних повідомлень",
                "З’єднання з сайтом відбувається на основі протоколу HTTPS",
                "Програмний продукт має витримувати мінімум 100 запитів до серверу на секунду",
                "Наявність вбудованого графічного редактору","Наявність сторінки оформлення замовлення реклами",
                "Блокування облікового запису у разі підозрілої поведінки"
            };

            var description = new DescriptionDragAndDropModel() { Columns = columns, Blocks = blocks };

            return JsonConvert.SerializeObject(description);
        }

        private static string CreateJsonTaskStandard()
        {
            var dict = new Dictionary<string, List<string>>
            {
                {
                    "Функціональні",
                    new List<string>() { "Перегляд вмісту кошику", "Вибір способу доставки", "Пошук товарів за назвою",
                "Наявність сторінки оформлення замовлення реклами", "Наявність вбудованого графічного редактору" }
                },

                {
                    "Нефункціональні",
                    new List<string>() { "З’єднання з сайтом відбувається на основі протоколу HTTPS",
                "Користувацький інтерфейс має відповідати 10 евристикам графічного інтерфейсу",
                "Програмний продукт має витримувати мінімум 100 запитів до серверу на секунду", "Блокування облікового запису у разі підозрілої поведінки",
                "Програмний продукт буде пов’язаний з поштовим клієнтом MS Outlook для отримання розсилки електронних повідомлень" }
                }
            };

            var standard = new StandardAndResultDragAndDropModel() { StandardOrResult = dict };

            return JsonConvert.SerializeObject(standard);
        }
    }
}
