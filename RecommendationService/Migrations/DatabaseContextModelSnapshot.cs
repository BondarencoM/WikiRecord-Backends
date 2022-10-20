﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecommendationService.Models;

namespace RecommendationService.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8");

            modelBuilder.Entity("RecommendationService.Models.Comments.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<long?>("InterestId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("RecommendationId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("InterestId");

                    b.HasIndex("RecommendationId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("RecommendationService.Models.Interests.Interest", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AddedBy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUri")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TypeString")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("WikiId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Interests");
                });

            modelBuilder.Entity("RecommendationService.Models.Personas.Persona", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AddedBy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUri")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("WikiId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("WikipediaUri")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Personas");
                });

            modelBuilder.Entity("RecommendationService.Models.Recommendations.Recommendation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AddedBy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Context")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<long>("InterestId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<long>("PersonaId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("InterestId");

                    b.HasIndex("PersonaId");

                    b.ToTable("Recommendations");
                });

            modelBuilder.Entity("RecommendationService.Models.Comments.Comment", b =>
                {
                    b.HasOne("RecommendationService.Models.Interests.Interest", "Interest")
                        .WithMany()
                        .HasForeignKey("InterestId");

                    b.HasOne("RecommendationService.Models.Recommendations.Recommendation", "Recommendation")
                        .WithMany()
                        .HasForeignKey("RecommendationId");
                });

            modelBuilder.Entity("RecommendationService.Models.Recommendations.Recommendation", b =>
                {
                    b.HasOne("RecommendationService.Models.Interests.Interest", "Interest")
                        .WithMany()
                        .HasForeignKey("InterestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecommendationService.Models.Personas.Persona", "Persona")
                        .WithMany("Recommendations")
                        .HasForeignKey("PersonaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
