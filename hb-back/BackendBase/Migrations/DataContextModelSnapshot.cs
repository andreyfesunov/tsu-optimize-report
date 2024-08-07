﻿// <auto-generated />
using System;
using BackendBase.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BackendBase.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BackendBase.Models.Activity", b =>
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

            modelBuilder.Entity("BackendBase.Models.ActivityEventType", b =>
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

            modelBuilder.Entity("BackendBase.Models.Comment", b =>
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

            modelBuilder.Entity("BackendBase.Models.Degree", b =>
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

            modelBuilder.Entity("BackendBase.Models.Department", b =>
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

            modelBuilder.Entity("BackendBase.Models.Event", b =>
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

            modelBuilder.Entity("BackendBase.Models.EventFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FileId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("FileId");

                    b.ToTable("EventsFiles");
                });

            modelBuilder.Entity("BackendBase.Models.EventType", b =>
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

            modelBuilder.Entity("BackendBase.Models.File", b =>
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

            modelBuilder.Entity("BackendBase.Models.Institute", b =>
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

            modelBuilder.Entity("BackendBase.Models.Job", b =>
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

            modelBuilder.Entity("BackendBase.Models.Lesson", b =>
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

            modelBuilder.Entity("BackendBase.Models.LessonType", b =>
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

            modelBuilder.Entity("BackendBase.Models.Rank", b =>
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

            modelBuilder.Entity("BackendBase.Models.Record", b =>
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

            modelBuilder.Entity("BackendBase.Models.State", b =>
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

            modelBuilder.Entity("BackendBase.Models.StateUser", b =>
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

            modelBuilder.Entity("BackendBase.Models.User", b =>
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

            modelBuilder.Entity("BackendBase.Models.Work", b =>
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

            modelBuilder.Entity("BackendBase.Models.ActivityEventType", b =>
                {
                    b.HasOne("BackendBase.Models.Activity", "Activity")
                        .WithMany("ActivitiesEventsTypes")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackendBase.Models.EventType", "EventType")
                        .WithMany("ActivitiesEventsTypes")
                        .HasForeignKey("EventTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");

                    b.Navigation("EventType");
                });

            modelBuilder.Entity("BackendBase.Models.Comment", b =>
                {
                    b.HasOne("BackendBase.Models.Event", "Event")
                        .WithMany("Comments")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("BackendBase.Models.Department", b =>
                {
                    b.HasOne("BackendBase.Models.Institute", "Institute")
                        .WithMany("Departments")
                        .HasForeignKey("InstituteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Institute");
                });

            modelBuilder.Entity("BackendBase.Models.Event", b =>
                {
                    b.HasOne("BackendBase.Models.EventType", "EventType")
                        .WithMany("Events")
                        .HasForeignKey("EventTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackendBase.Models.StateUser", "StateUser")
                        .WithMany("Events")
                        .HasForeignKey("StateUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventType");

                    b.Navigation("StateUser");
                });

            modelBuilder.Entity("BackendBase.Models.EventFile", b =>
                {
                    b.HasOne("BackendBase.Models.Event", "Event")
                        .WithMany("EventsFiles")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackendBase.Models.File", "File")
                        .WithMany()
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("File");
                });

            modelBuilder.Entity("BackendBase.Models.EventType", b =>
                {
                    b.HasOne("BackendBase.Models.Work", "Work")
                        .WithMany("EventsTypes")
                        .HasForeignKey("WorkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Work");
                });

            modelBuilder.Entity("BackendBase.Models.File", b =>
                {
                    b.HasOne("BackendBase.Models.StateUser", "StateUser")
                        .WithMany("Files")
                        .HasForeignKey("StateUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StateUser");
                });

            modelBuilder.Entity("BackendBase.Models.Lesson", b =>
                {
                    b.HasOne("BackendBase.Models.Event", "Event")
                        .WithMany("Lessons")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackendBase.Models.LessonType", "LessonType")
                        .WithMany("Lessons")
                        .HasForeignKey("LessonTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("LessonType");
                });

            modelBuilder.Entity("BackendBase.Models.Record", b =>
                {
                    b.HasOne("BackendBase.Models.Activity", "Activity")
                        .WithMany("Records")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackendBase.Models.LessonType", "LessonType")
                        .WithMany("Records")
                        .HasForeignKey("LessonTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackendBase.Models.StateUser", "StateUser")
                        .WithMany("Records")
                        .HasForeignKey("StateUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");

                    b.Navigation("LessonType");

                    b.Navigation("StateUser");
                });

            modelBuilder.Entity("BackendBase.Models.State", b =>
                {
                    b.HasOne("BackendBase.Models.Department", "Department")
                        .WithMany("States")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackendBase.Models.Job", "Job")
                        .WithMany("States")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Job");
                });

            modelBuilder.Entity("BackendBase.Models.StateUser", b =>
                {
                    b.HasOne("BackendBase.Models.State", "State")
                        .WithMany("StatesUsers")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackendBase.Models.User", "User")
                        .WithMany("StatesUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("State");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BackendBase.Models.User", b =>
                {
                    b.HasOne("BackendBase.Models.Degree", "Degree")
                        .WithMany("Users")
                        .HasForeignKey("DegreeId");

                    b.HasOne("BackendBase.Models.Rank", "Rank")
                        .WithMany("Users")
                        .HasForeignKey("RankId");

                    b.Navigation("Degree");

                    b.Navigation("Rank");
                });

            modelBuilder.Entity("BackendBase.Models.Activity", b =>
                {
                    b.Navigation("ActivitiesEventsTypes");

                    b.Navigation("Records");
                });

            modelBuilder.Entity("BackendBase.Models.Degree", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("BackendBase.Models.Department", b =>
                {
                    b.Navigation("States");
                });

            modelBuilder.Entity("BackendBase.Models.Event", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("EventsFiles");

                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("BackendBase.Models.EventType", b =>
                {
                    b.Navigation("ActivitiesEventsTypes");

                    b.Navigation("Events");
                });

            modelBuilder.Entity("BackendBase.Models.Institute", b =>
                {
                    b.Navigation("Departments");
                });

            modelBuilder.Entity("BackendBase.Models.Job", b =>
                {
                    b.Navigation("States");
                });

            modelBuilder.Entity("BackendBase.Models.LessonType", b =>
                {
                    b.Navigation("Lessons");

                    b.Navigation("Records");
                });

            modelBuilder.Entity("BackendBase.Models.Rank", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("BackendBase.Models.State", b =>
                {
                    b.Navigation("StatesUsers");
                });

            modelBuilder.Entity("BackendBase.Models.StateUser", b =>
                {
                    b.Navigation("Events");

                    b.Navigation("Files");

                    b.Navigation("Records");
                });

            modelBuilder.Entity("BackendBase.Models.User", b =>
                {
                    b.Navigation("StatesUsers");
                });

            modelBuilder.Entity("BackendBase.Models.Work", b =>
                {
                    b.Navigation("EventsTypes");
                });
#pragma warning restore 612, 618
        }
    }
}