﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SDLCSimulator_Data;

namespace SDLCSimulator_Data.Migrations
{
    [DbContext(typeof(SDLCSimulatorDbContext))]
    [Migration("20211114153554_AddAnotherTask")]
    partial class AddAnotherTask
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "6.0.0-preview.1.21102.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SDLCSimulator_Data.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GroupName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Group");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            GroupName = "ПЗ-41"
                        },
                        new
                        {
                            Id = 2,
                            GroupName = "ПЗ-42"
                        },
                        new
                        {
                            Id = 3,
                            GroupName = "ПЗ-43"
                        },
                        new
                        {
                            Id = 4,
                            GroupName = "ПЗ-44"
                        },
                        new
                        {
                            Id = 5,
                            GroupName = "ПЗ-45"
                        });
                });

            modelBuilder.Entity("SDLCSimulator_Data.GroupTask", b =>
                {
                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.HasKey("GroupId", "TaskId");

                    b.HasIndex("TaskId");

                    b.ToTable("GroupTask");

                    b.HasData(
                        new
                        {
                            GroupId = 1,
                            TaskId = 1
                        },
                        new
                        {
                            GroupId = 1,
                            TaskId = 2
                        });
                });

            modelBuilder.Entity("SDLCSimulator_Data.GroupTeacher", b =>
                {
                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("GroupId", "TeacherId");

                    b.HasIndex("TeacherId");

                    b.ToTable("GroupTeacher");

                    b.HasData(
                        new
                        {
                            GroupId = 1,
                            TeacherId = 2
                        },
                        new
                        {
                            GroupId = 2,
                            TeacherId = 2
                        });
                });

            modelBuilder.Entity("SDLCSimulator_Data.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Difficulty")
                        .HasColumnType("int");

                    b.Property<int>("ErrorRate")
                        .HasColumnType("int");

                    b.Property<int>("MaxGrade")
                        .HasColumnType("int");

                    b.Property<string>("Standard")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.Property<string>("Topic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Task");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "{\"Columns\":[\"Функціональні\",\"Нефункціональні\"],\"Blocks\":[\"Пошук товарів за назвою\",\"Користувацький інтерфейс має відповідати 10 евристикам графічного інтерфейсу\",\"Перегляд вмісту кошику\",\"Вибір способу доставки\",\"Програмний продукт буде пов’язаний з поштовим клієнтом MS Outlook для отримання розсилки електронних повідомлень\",\"З’єднання з сайтом відбувається на основі протоколу HTTPS\",\"Програмний продукт має витримувати мінімум 100 запитів до серверу на секунду\",\"Наявність вбудованого графічного редактору\",\"Наявність сторінки оформлення замовлення реклами\",\"Блокування облікового запису у разі підозрілої поведінки\"]}",
                            Difficulty = 2,
                            ErrorRate = 1,
                            MaxGrade = 40,
                            Standard = "{\"StandardOrResult\":{\"Функціональні\":[\"Перегляд вмісту кошику\",\"Вибір способу доставки\",\"Пошук товарів за назвою\",\"Наявність сторінки оформлення замовлення реклами\",\"Наявність вбудованого графічного редактору\"],\"Нефункціональні\":[\"З’єднання з сайтом відбувається на основі протоколу HTTPS\",\"Користувацький інтерфейс має відповідати 10 евристикам графічного інтерфейсу\",\"Програмний продукт має витримувати мінімум 100 запитів до серверу на секунду\",\"Блокування облікового запису у разі підозрілої поведінки\",\"Програмний продукт буде пов’язаний з поштовим клієнтом MS Outlook для отримання розсилки електронних повідомлень\"]}}",
                            TeacherId = 2,
                            Topic = "Вимоги до системи роботи магазину ювелірних виробів",
                            Type = 1
                        },
                        new
                        {
                            Id = 2,
                            Description = "{\"Columns\":[\"Вимоги до системи автозаправної станції\",\"Вимоги до сервісу пошуку та порівняння цін на товари інтернет-магазинів\",\"Вимоги до системи обліку роботи магазину будівельних матеріалів\"],\"Blocks\":[\"Можливість додавання нових палив/сервісів/продуктів\",\"Можливість пошуку автозаправної станції з необхідним паливом та сервісами\",\"Можливість накопичувати бонусні бали та витрачати їх при купівлі товарів інтернет-магазинів\",\"Наявність способу оформлення автоцивілки онлайн з отриманням на певній АЗС\",\"Можливість перегляду онлайн трансляції території АЗС у браузері\",\"Можливість оформлення замовлення будівельних матеріалів для клієнта\",\"Можливість подивитися наявні зображення та відеоматеріали стосовно товару інтернет-магазину\",\"Експорт інформації про товари інтернет-магазинів на платформу\",\"Можливість обліку заробітної плати працівників магазину будівельних матеріалів\",\"Можливість перегляду історії цін на товар інтернет-магазину\",\"Можливість оформлення замовлення на постачання будівельних матеріалів\",\"Можливість пошуку та фільтрації будівельних матеріалів\",\"Можливість внесення та редагування даних про постійних клієнтів магазину будівельних матеріалів\",\"Можливість придбання обраного товару інтернет-магазину\",\"Можливість редагування списку акцій/новин на сайті АЗС\",\"Додавання будівельного матеріалу в систему\",\"Можливість завантаження файлів в систему АЗС\",\"Можливість залишання відгуку про інтернет-магазин\"]}",
                            Difficulty = 3,
                            ErrorRate = 2,
                            MaxGrade = 60,
                            Standard = "{\"StandardOrResult\":{\"Вимоги до системи автозаправної станції\":[\"Можливість перегляду онлайн трансляції території АЗС у браузері\",\"Можливість додавання нових палив/сервісів/продуктів\",\"Можливість пошуку автозаправної станції з необхідним паливом та сервісами\"],\"Вимоги до сервісу пошуку та порівняння цін на товари інтернет-магазинів\":[\"Можливість придбання обраного товару інтернет-магазину\",\"Можливість перегляду історії цін на товар інтернет-магазину\",\"Експорт інформації про товари інтернет-магазинів на платформу\"],\"Вимоги до системи обліку роботи магазину будівельних матеріалів\":[\"Можливість оформлення замовлення будівельних матеріалів для клієнта\",\"Можливість пошуку та фільтрації будівельних матеріалів\",\"Можливість оформлення замовлення на постачання будівельних матеріалів\"]}}",
                            TeacherId = 2,
                            Topic = "Вибір найважливіших вимог для декількох систем",
                            Type = 2
                        });
                });

            modelBuilder.Entity("SDLCSimulator_Data.TaskResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ErrorCount")
                        .HasColumnType("int");

                    b.Property<decimal>("FinalMark")
                        .HasColumnType("decimal(4,2)");

                    b.Property<decimal>("Percentage")
                        .HasColumnType("decimal(4,2)");

                    b.Property<string>("Result")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("TaskId");

                    b.ToTable("TaskResult");
                });

            modelBuilder.Entity("SDLCSimulator_Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "іван.іванов.пз@lpnu.ua",
                            FirstName = "Іван",
                            GroupId = 1,
                            LastName = "Іванов",
                            Password = "AKXwmMIdR9fEXaikvLavw33r0zyiXHBLBk4MJELb5RNwoyMCsi8NBf8advWXCTQ54A==",
                            Role = 0
                        },
                        new
                        {
                            Id = 2,
                            Email = "андрій.фоменко.викладач@lpnu.ua",
                            FirstName = "Андрій",
                            LastName = "Фоменко",
                            Password = "AEIatg7ShLybH2927m5UTtOGO2EjSBB6JuXbkhhhUHDIQAH+tKRvN81u9R7ZqhzOcA==",
                            Role = 1
                        },
                        new
                        {
                            Id = 3,
                            Email = "сергій.федецький.адмін@lpnu.ua",
                            FirstName = "Сергій",
                            LastName = "Федецький",
                            Password = "AGiprihD8YNNbQk2w5XdYqNtbOxu8Qly+7gmJloWjaKPdPWSAHIb2SAbMsaRO08e6Q==",
                            Role = 2
                        });
                });

            modelBuilder.Entity("SDLCSimulator_Data.GroupTask", b =>
                {
                    b.HasOne("SDLCSimulator_Data.Group", "Group")
                        .WithMany("GroupTasks")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SDLCSimulator_Data.Task", "Task")
                        .WithMany("GroupTasks")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("SDLCSimulator_Data.GroupTeacher", b =>
                {
                    b.HasOne("SDLCSimulator_Data.Group", "Group")
                        .WithMany("GroupTeachers")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SDLCSimulator_Data.User", "Teacher")
                        .WithMany("GroupTeachers")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("SDLCSimulator_Data.Task", b =>
                {
                    b.HasOne("SDLCSimulator_Data.User", "Teacher")
                        .WithMany("Tasks")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("SDLCSimulator_Data.TaskResult", b =>
                {
                    b.HasOne("SDLCSimulator_Data.User", "Student")
                        .WithMany("TaskResults")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SDLCSimulator_Data.Task", "Task")
                        .WithMany("TaskResults")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("SDLCSimulator_Data.User", b =>
                {
                    b.HasOne("SDLCSimulator_Data.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Group");
                });

            modelBuilder.Entity("SDLCSimulator_Data.Group", b =>
                {
                    b.Navigation("GroupTasks");

                    b.Navigation("GroupTeachers");
                });

            modelBuilder.Entity("SDLCSimulator_Data.Task", b =>
                {
                    b.Navigation("GroupTasks");

                    b.Navigation("TaskResults");
                });

            modelBuilder.Entity("SDLCSimulator_Data.User", b =>
                {
                    b.Navigation("GroupTeachers");

                    b.Navigation("TaskResults");

                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
