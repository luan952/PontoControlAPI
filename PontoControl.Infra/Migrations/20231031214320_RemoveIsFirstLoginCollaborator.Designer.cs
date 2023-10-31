﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PontoControl.Infra.RepositoryAccess;

#nullable disable

namespace PontoControl.Infra.Migrations
{
    [DbContext(typeof(PontoControlContext))]
    [Migration("20231031214320_RemoveIsFirstLoginCollaborator")]
    partial class RemoveIsFirstLoginCollaborator
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PontoControl.Domain.Entities.Marking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CollaboratorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Hour")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CollaboratorId");

                    b.ToTable("Markings");
                });

            modelBuilder.Entity("PontoControl.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Document")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsFirstLogin")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeUser")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("PontoControl.Domain.Entities.Admin", b =>
                {
                    b.HasBaseType("PontoControl.Domain.Entities.User");

                    b.ToTable("Admins", (string)null);
                });

            modelBuilder.Entity("PontoControl.Domain.Entities.Collaborator", b =>
                {
                    b.HasBaseType("PontoControl.Domain.Entities.User");

                    b.Property<Guid>("AdminId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("AdminId");

                    b.ToTable("Collaborators", (string)null);
                });

            modelBuilder.Entity("PontoControl.Domain.Entities.Marking", b =>
                {
                    b.HasOne("PontoControl.Domain.Entities.Collaborator", "Collaborator")
                        .WithMany("Markings")
                        .HasForeignKey("CollaboratorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Collaborator");
                });

            modelBuilder.Entity("PontoControl.Domain.Entities.Admin", b =>
                {
                    b.HasOne("PontoControl.Domain.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("PontoControl.Domain.Entities.Admin", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PontoControl.Domain.Entities.Collaborator", b =>
                {
                    b.HasOne("PontoControl.Domain.Entities.Admin", "Admin")
                        .WithMany("Collaborators")
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PontoControl.Domain.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("PontoControl.Domain.Entities.Collaborator", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("PontoControl.Domain.Entities.Admin", b =>
                {
                    b.Navigation("Collaborators");
                });

            modelBuilder.Entity("PontoControl.Domain.Entities.Collaborator", b =>
                {
                    b.Navigation("Markings");
                });
#pragma warning restore 612, 618
        }
    }
}
