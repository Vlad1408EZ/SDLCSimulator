using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDLCSimulator_BusinessLogic.Models.Input;

namespace SDLCSimulator_BackEnd_Tests.Fixtures
{
    public static class CreateTaskResultInputFixture
    {
        public static CreateTaskResultInput CreateValidEntity()
        {
            return new()
            {
                TaskId = 1,
                ErrorCount = 2,
                Result = new()
                {
                    StandardOrResult = new()
                    {
                        {
                            "Функціональні",
                            new List<string>
                            {
                                "Перегляд вмісту кошику","Вибір способу доставки","Пошук товарів за назвою",
                                "Наявність вбудованого графічного редактору",
                                "Наявність сторінки оформлення замовлення реклами"
                            }
                        },

                        {
                            "Нефункціональні",
                            new List<string>
                            {
                                "З’єднання з сайтом відбувається на основі протоколу HTTPS",
                                "Блокування облікового запису у разі підозрілої поведінки",
                                "Програмний продукт буде пов’язаний з поштовим клієнтом MS Outlook для отримання розсилки електронних повідомлень",
                                "Користувацький інтерфейс має відповідати 10 евристикам графічного інтерфейсу",
                                "Програмний продукт має витримувати мінімум 100 запитів до серверу на секунду"
                            }
                        }
                    }
                }
            };
        }
    }
}
