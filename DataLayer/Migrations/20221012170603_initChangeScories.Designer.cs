﻿// <auto-generated />
using System;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataLayer.Migrations
{
    [DbContext(typeof(EFDBContext))]
    [Migration("20221012170603_initChangeScories")]
    partial class initChangeScories
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataLayer.Entities.Discipline", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("IdGroupId")
                        .HasColumnType("int");

                    b.Property<int?>("IdTeacherId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("availabilityOfCoursework")
                        .HasColumnType("bit");

                    b.Property<int>("block")
                        .HasColumnType("int");

                    b.Property<string>("formAttestation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("yearsEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("yearsStart")
                        .HasColumnType("datetime2");

                    b.Property<int>("zet")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdGroupId");

                    b.HasIndex("IdTeacherId");

                    b.ToTable("Discipline");
                });

            modelBuilder.Entity("DataLayer.Entities.Groups", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("DataLayer.Entities.Lesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("IdDisciplineId")
                        .HasColumnType("int");

                    b.Property<int?>("IdTypeLessonId")
                        .HasColumnType("int");

                    b.Property<string>("NameLesson")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdDisciplineId");

                    b.HasIndex("IdTypeLessonId");

                    b.ToTable("Lesson");
                });

            modelBuilder.Entity("DataLayer.Entities.RolesOfUsers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RolesOfUsers");
                });

            modelBuilder.Entity("DataLayer.Entities.Score", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Attendance")
                        .HasColumnType("bit");

                    b.Property<string>("Evalution")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdLessonId")
                        .HasColumnType("int");

                    b.Property<int?>("IdStudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdLessonId");

                    b.HasIndex("IdStudentId");

                    b.ToTable("Score");
                });

            modelBuilder.Entity("DataLayer.Entities.TypeLesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NameType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TypeLessons");
                });

            modelBuilder.Entity("DataLayer.Entities.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Fio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdGroupId")
                        .HasColumnType("int");

                    b.Property<int?>("IdRoleId")
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdGroupId");

                    b.HasIndex("IdRoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DataLayer.Entities.Discipline", b =>
                {
                    b.HasOne("DataLayer.Entities.Groups", "IdGroup")
                        .WithMany()
                        .HasForeignKey("IdGroupId");

                    b.HasOne("DataLayer.Entities.Users", "IdTeacher")
                        .WithMany()
                        .HasForeignKey("IdTeacherId");

                    b.Navigation("IdGroup");

                    b.Navigation("IdTeacher");
                });

            modelBuilder.Entity("DataLayer.Entities.Lesson", b =>
                {
                    b.HasOne("DataLayer.Entities.Discipline", "IdDiscipline")
                        .WithMany()
                        .HasForeignKey("IdDisciplineId");

                    b.HasOne("DataLayer.Entities.TypeLesson", "IdTypeLesson")
                        .WithMany()
                        .HasForeignKey("IdTypeLessonId");

                    b.Navigation("IdDiscipline");

                    b.Navigation("IdTypeLesson");
                });

            modelBuilder.Entity("DataLayer.Entities.Score", b =>
                {
                    b.HasOne("DataLayer.Entities.Lesson", "IdLesson")
                        .WithMany("IdScore")
                        .HasForeignKey("IdLessonId");

                    b.HasOne("DataLayer.Entities.Users", "IdStudent")
                        .WithMany()
                        .HasForeignKey("IdStudentId");

                    b.Navigation("IdLesson");

                    b.Navigation("IdStudent");
                });

            modelBuilder.Entity("DataLayer.Entities.Users", b =>
                {
                    b.HasOne("DataLayer.Entities.Groups", "IdGroup")
                        .WithMany()
                        .HasForeignKey("IdGroupId");

                    b.HasOne("DataLayer.Entities.RolesOfUsers", "IdRole")
                        .WithMany()
                        .HasForeignKey("IdRoleId");

                    b.Navigation("IdGroup");

                    b.Navigation("IdRole");
                });

            modelBuilder.Entity("DataLayer.Entities.Lesson", b =>
                {
                    b.Navigation("IdScore");
                });
#pragma warning restore 612, 618
        }
    }
}
