﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Workout.Infrastructure.Database;

#nullable disable

namespace Workout.Infrastructure.Database.Migration
{
    [DbContext(typeof(WorkoutDbContext))]
    [Migration("20240808194707_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Workout.Domain.Entity.Exercise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("PrimaryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TrainerId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("WorkoutId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PrimaryId")
                        .IsUnique();

                    b.HasIndex("TrainerId");

                    b.HasIndex("WorkoutId");

                    b.HasIndex("Id", "TrainerId");

                    b.ToTable("Exercise", "workout");
                });

            modelBuilder.Entity("Workout.Domain.Entity.Set", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Repeat")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RepetitionRate")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<int>("RestTime")
                        .HasColumnType("integer");

                    b.Property<Guid?>("SetId")
                        .HasColumnType("uuid");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SetId");

                    b.ToTable("Set", "workout");
                });

            modelBuilder.Entity("Workout.Domain.Entity.Workout", b =>
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

                    b.Property<Guid?>("WorkoutId")
                        .HasColumnType("uuid");

                    b.Property<string>("_dates")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.HasKey("Id");

                    b.HasIndex("WorkoutId");

                    b.ToTable("Workout", "workout");
                });

            modelBuilder.Entity("Workout.Domain.Entity.WorkoutPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TrainerId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("WardId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TrainerId");

                    b.HasIndex("Id", "TrainerId");

                    b.ToTable("WorkoutPlan", "workout");
                });

            modelBuilder.Entity("Workout.Domain.ValueObject.Date", b =>
                {
                    b.Property<DateOnly>("Value")
                        .HasColumnType("date");

                    b.ToTable("Date");
                });

            modelBuilder.Entity("Workout.Domain.Entity.Exercise", b =>
                {
                    b.HasOne("Workout.Domain.Entity.Exercise", "Primary")
                        .WithOne()
                        .HasForeignKey("Workout.Domain.Entity.Exercise", "PrimaryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Workout.Domain.Entity.Workout", null)
                        .WithMany("Exercises")
                        .HasForeignKey("WorkoutId");

                    b.Navigation("Primary");
                });

            modelBuilder.Entity("Workout.Domain.Entity.Set", b =>
                {
                    b.HasOne("Workout.Domain.Entity.Exercise", null)
                        .WithMany("Sets")
                        .HasForeignKey("SetId");
                });

            modelBuilder.Entity("Workout.Domain.Entity.Workout", b =>
                {
                    b.HasOne("Workout.Domain.Entity.WorkoutPlan", null)
                        .WithMany("Workouts")
                        .HasForeignKey("WorkoutId");
                });

            modelBuilder.Entity("Workout.Domain.Entity.Exercise", b =>
                {
                    b.Navigation("Sets");
                });

            modelBuilder.Entity("Workout.Domain.Entity.Workout", b =>
                {
                    b.Navigation("Exercises");
                });

            modelBuilder.Entity("Workout.Domain.Entity.WorkoutPlan", b =>
                {
                    b.Navigation("Workouts");
                });
#pragma warning restore 612, 618
        }
    }
}
