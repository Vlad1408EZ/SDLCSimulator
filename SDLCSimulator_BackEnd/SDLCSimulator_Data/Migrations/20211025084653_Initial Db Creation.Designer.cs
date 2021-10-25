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
    [Migration("20211025084653_Initial Db Creation")]
    partial class InitialDbCreation
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
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Task");
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
                });

            modelBuilder.Entity("SDLCSimulator_Data.GroupTask", b =>
                {
                    b.HasOne("SDLCSimulator_Data.Group", "Group")
                        .WithMany("GroupTasks")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("SDLCSimulator_Data.Task", "Task")
                        .WithMany("GroupTasks")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("SDLCSimulator_Data.GroupTeacher", b =>
                {
                    b.HasOne("SDLCSimulator_Data.Group", "Group")
                        .WithMany("GroupTeachers")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("SDLCSimulator_Data.User", "Teacher")
                        .WithMany("GroupTeachers")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("SDLCSimulator_Data.Task", b =>
                {
                    b.HasOne("SDLCSimulator_Data.User", "Teacher")
                        .WithMany("Tasks")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("SDLCSimulator_Data.TaskResult", b =>
                {
                    b.HasOne("SDLCSimulator_Data.User", "Student")
                        .WithMany("TaskResults")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("SDLCSimulator_Data.Task", "Task")
                        .WithMany("TaskResults")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("SDLCSimulator_Data.User", b =>
                {
                    b.HasOne("SDLCSimulator_Data.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.ClientCascade);

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
