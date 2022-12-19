﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecommendationService.Models;

namespace RecommendationService.Migrations;

[DbContext(typeof(DatabaseContext))]
partial class DatabaseContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "3.1.8")
            .HasAnnotation("Relational:MaxIdentifierLength", 128)
            .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

        modelBuilder.Entity("RecommendationService.Models.Comments.Comment", b =>
            {
                b.Property<long>("Id")
                    .HasColumnType("bigint");

                b.Property<DateTime?>("CreatedAt")
                    .HasColumnType("datetime2");

                b.Property<long?>("InterestId")
                    .HasColumnType("bigint");

                b.Property<bool>("IsDeleted")
                    .HasColumnType("bit");

                b.Property<long?>("RecommendationId")
                    .HasColumnType("bigint");

                b.Property<string>("Text")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Username")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.HasIndex("InterestId");

                b.HasIndex("RecommendationId");

                b.ToTable("Comments");
            });

        modelBuilder.Entity("RecommendationService.Models.Interests.Interest", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bigint")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("AddedBy")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Description")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("ImageUri")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("TypeString")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("WikiId")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.ToTable("Interests");
            });

        modelBuilder.Entity("RecommendationService.Models.Personas.Persona", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bigint")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("AddedBy")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Description")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("ImageUri")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("WikiId")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("WikipediaUri")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("Id");

                b.ToTable("Personas");
            });

        modelBuilder.Entity("RecommendationService.Models.Recommendations.Recommendation", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bigint")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("AddedBy")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Context")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("datetime2");

                b.Property<long>("InterestId")
                    .HasColumnType("bigint");

                b.Property<bool>("IsConfirmed")
                    .HasColumnType("bit");

                b.Property<long>("PersonaId")
                    .HasColumnType("bigint");

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
