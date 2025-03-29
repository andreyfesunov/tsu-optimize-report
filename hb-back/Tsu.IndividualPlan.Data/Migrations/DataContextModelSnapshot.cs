﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Tsu.IndividualPlan.Data.Context;

#nullable disable

namespace Tsu.IndividualPlan.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.Activity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Column")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.ActivityEventType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ActivityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EventTypeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.HasIndex("EventTypeId");

                    b.ToTable("ActivitiesEventsTypes");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid");

                    b.Property<int?>("FactDate")
                        .HasColumnType("integer");

                    b.Property<int?>("PlanDate")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.Degree", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Degrees");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("InstituteId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("InstituteId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("EndedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("EventTypeId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("StartedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("StateUserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("EventTypeId");

                    b.HasIndex("StateUserId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.EventType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("WorkId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("WorkId");

                    b.ToTable("EventsTypes");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("StateUserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("StateUserId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.Institute", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Institutes");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.Job", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.Lesson", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid");

                    b.Property<int?>("FactDate")
                        .HasColumnType("integer");

                    b.Property<Guid>("LessonTypeId")
                        .HasColumnType("uuid");

                    b.Property<int?>("PlanDate")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("LessonTypeId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.LessonType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("LessonTypes");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.Rank", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Ranks");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.Record", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ActivityId")
                        .HasColumnType("uuid");

                    b.Property<int>("Hours")
                        .HasColumnType("integer");

                    b.Property<Guid>("LessonTypeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("StateUserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.HasIndex("LessonTypeId");

                    b.HasIndex("StateUserId");

                    b.ToTable("Records");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.State", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Hours")
                        .HasColumnType("integer");

                    b.Property<Guid>("JobId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("JobId");

                    b.ToTable("States");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.StateUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("Rate")
                        .HasColumnType("double precision");

                    b.Property<Guid>("StateId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.HasIndex("UserId");

                    b.ToTable("StatesUsers");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("DegreeId")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("RankId")
                        .HasColumnType("uuid");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DegreeId");

                    b.HasIndex("RankId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.Work", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Work");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.ActivityEventType", b =>
                {
                    b.HasOne("Tsu.IndividualPlan.Domain.Models.Business.Activity", "Activity")
                        .WithMany("ActivitiesEventsTypes")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tsu.IndividualPlan.Domain.Models.Business.EventType", "EventType")
                        .WithMany("ActivitiesEventsTypes")
                        .HasForeignKey("EventTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");

                    b.Navigation("EventType");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.Comment", b =>
                {
                    b.HasOne("Tsu.IndividualPlan.Domain.Models.Business.Event", "Event")
                        .WithMany("Comments")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.Department", b =>
                {
                    b.HasOne("Tsu.IndividualPlan.Domain.Models.Business.Institute", "Institute")
                        .WithMany("Departments")
                        .HasForeignKey("InstituteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Institute");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.Event", b =>
                {
                    b.HasOne("Tsu.IndividualPlan.Domain.Models.Business.EventType", "EventType")
                        .WithMany("Events")
                        .HasForeignKey("EventTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tsu.IndividualPlan.Domain.Models.Business.StateUser", "StateUser")
                        .WithMany("Events")
                        .HasForeignKey("StateUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventType");

                    b.Navigation("StateUser");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.EventType", b =>
                {
                    b.HasOne("Tsu.IndividualPlan.Domain.Models.Business.Work", "Work")
                        .WithMany()
                        .HasForeignKey("WorkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Work");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.File", b =>
                {
                    b.HasOne("Tsu.IndividualPlan.Domain.Models.Business.StateUser", "StateUser")
                        .WithMany("Files")
                        .HasForeignKey("StateUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StateUser");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.Lesson", b =>
                {
                    b.HasOne("Tsu.IndividualPlan.Domain.Models.Business.Event", "Event")
                        .WithMany("Lessons")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tsu.IndividualPlan.Domain.Models.Business.LessonType", "LessonType")
                        .WithMany("Lessons")
                        .HasForeignKey("LessonTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("LessonType");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.Record", b =>
                {
                    b.HasOne("Tsu.IndividualPlan.Domain.Models.Business.Activity", "Activity")
                        .WithMany("Records")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tsu.IndividualPlan.Domain.Models.Business.LessonType", "LessonType")
                        .WithMany("Records")
                        .HasForeignKey("LessonTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tsu.IndividualPlan.Domain.Models.Business.StateUser", "StateUser")
                        .WithMany("Records")
                        .HasForeignKey("StateUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");

                    b.Navigation("LessonType");

                    b.Navigation("StateUser");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.State", b =>
                {
                    b.HasOne("Tsu.IndividualPlan.Domain.Models.Business.Department", "Department")
                        .WithMany("States")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tsu.IndividualPlan.Domain.Models.Business.Job", "Job")
                        .WithMany("States")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Job");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.StateUser", b =>
                {
                    b.HasOne("Tsu.IndividualPlan.Domain.Models.Business.State", "State")
                        .WithMany("StatesUsers")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tsu.IndividualPlan.Domain.Models.Business.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("State");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.User", b =>
                {
                    b.HasOne("Tsu.IndividualPlan.Domain.Models.Business.Degree", null)
                        .WithMany("Users")
                        .HasForeignKey("DegreeId");

                    b.HasOne("Tsu.IndividualPlan.Domain.Models.Business.Rank", null)
                        .WithMany("Users")
                        .HasForeignKey("RankId");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.Activity", b =>
                {
                    b.Navigation("ActivitiesEventsTypes");

                    b.Navigation("Records");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.Degree", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.Department", b =>
                {
                    b.Navigation("States");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.Event", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.EventType", b =>
                {
                    b.Navigation("ActivitiesEventsTypes");

                    b.Navigation("Events");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.Institute", b =>
                {
                    b.Navigation("Departments");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.Job", b =>
                {
                    b.Navigation("States");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.LessonType", b =>
                {
                    b.Navigation("Lessons");

                    b.Navigation("Records");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.Rank", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.State", b =>
                {
                    b.Navigation("StatesUsers");
                });

            modelBuilder.Entity("Tsu.IndividualPlan.Domain.Models.Business.StateUser", b =>
                {
                    b.Navigation("Events");

                    b.Navigation("Files");

                    b.Navigation("Records");
                });
#pragma warning restore 612, 618
        }
    }
}
