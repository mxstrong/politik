﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Politics.Model;

namespace Politics.Migrations
{
    [DbContext(typeof(PoliticsDbContext))]
    partial class PoliticsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Politics.Model.ActivationToken", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ActivationTokens");
                });

            modelBuilder.Entity("Politics.Model.EmailChangeToken", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("NewEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("EmailChangeTokens");
                });

            modelBuilder.Entity("Politics.Model.Like", b =>
                {
                    b.Property<string>("LikeId")
                        .HasColumnType("text");

                    b.Property<string>("StatementId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LikeId");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("Politics.Model.Party", b =>
                {
                    b.Property<string>("PartyId")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedById")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LongName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UpdatedById")
                        .HasColumnType("text");

                    b.HasKey("PartyId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("UpdatedById");

                    b.ToTable("Parties");
                });

            modelBuilder.Entity("Politics.Model.Politician", b =>
                {
                    b.Property<string>("PoliticianId")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedById")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PartyId")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UpdatedById")
                        .HasColumnType("text");

                    b.HasKey("PoliticianId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("PartyId");

                    b.HasIndex("UpdatedById");

                    b.ToTable("Politicians");
                });

            modelBuilder.Entity("Politics.Model.Report", b =>
                {
                    b.Property<string>("ReportId")
                        .HasColumnType("text");

                    b.Property<string>("CreatedById")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsReviewed")
                        .HasColumnType("boolean");

                    b.Property<string>("ReviewedById")
                        .HasColumnType("text");

                    b.Property<string>("StatementId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ReportId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ReviewedById");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("Politics.Model.Role", b =>
                {
                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Politics.Model.Statement", b =>
                {
                    b.Property<string>("StatementId")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedById")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PoliticianId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UpdatedById")
                        .HasColumnType("text");

                    b.HasKey("StatementId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("PoliticianId");

                    b.HasIndex("UpdatedById");

                    b.ToTable("Statements");
                });

            modelBuilder.Entity("Politics.Model.StatementTag", b =>
                {
                    b.Property<string>("StatementTagId")
                        .HasColumnType("text");

                    b.Property<string>("StatementId")
                        .HasColumnType("text");

                    b.Property<string>("TagId")
                        .HasColumnType("text");

                    b.HasKey("StatementTagId");

                    b.HasIndex("StatementId");

                    b.HasIndex("TagId");

                    b.ToTable("StatementTags");
                });

            modelBuilder.Entity("Politics.Model.Tag", b =>
                {
                    b.Property<string>("TagId")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedById")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("TagId");

                    b.HasIndex("CreatedById");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Politics.Model.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<bool>("Activated")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Politics.Model.Party", b =>
                {
                    b.HasOne("Politics.Model.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Politics.Model.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("CreatedBy");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("Politics.Model.Politician", b =>
                {
                    b.HasOne("Politics.Model.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Politics.Model.Party", "Party")
                        .WithMany()
                        .HasForeignKey("PartyId");

                    b.HasOne("Politics.Model.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("CreatedBy");

                    b.Navigation("Party");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("Politics.Model.Report", b =>
                {
                    b.HasOne("Politics.Model.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Politics.Model.User", "ReviewedBy")
                        .WithMany()
                        .HasForeignKey("ReviewedById");

                    b.Navigation("CreatedBy");

                    b.Navigation("ReviewedBy");
                });

            modelBuilder.Entity("Politics.Model.Statement", b =>
                {
                    b.HasOne("Politics.Model.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Politics.Model.Politician", "Politician")
                        .WithMany()
                        .HasForeignKey("PoliticianId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Politics.Model.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("CreatedBy");

                    b.Navigation("Politician");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("Politics.Model.StatementTag", b =>
                {
                    b.HasOne("Politics.Model.Statement", "Statement")
                        .WithMany("StatementTags")
                        .HasForeignKey("StatementId");

                    b.HasOne("Politics.Model.Tag", "Tag")
                        .WithMany("StatementTags")
                        .HasForeignKey("TagId");

                    b.Navigation("Statement");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("Politics.Model.Tag", b =>
                {
                    b.HasOne("Politics.Model.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("Politics.Model.User", b =>
                {
                    b.HasOne("Politics.Model.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Politics.Model.Statement", b =>
                {
                    b.Navigation("StatementTags");
                });

            modelBuilder.Entity("Politics.Model.Tag", b =>
                {
                    b.Navigation("StatementTags");
                });
#pragma warning restore 612, 618
        }
    }
}
