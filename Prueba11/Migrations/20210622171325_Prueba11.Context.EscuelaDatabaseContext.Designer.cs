﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Prueba11.Context;

namespace Prueba11.Migrations
{
    [DbContext(typeof(EscuelaDatabaseContext))]
    [Migration("20210622171325_Prueba11.Context.EscuelaDatabaseContext")]
    partial class Prueba11ContextEscuelaDatabaseContext
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Prueba11.Models.Celebrity", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("biography")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("bornDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("genres")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("language")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("rating")
                        .HasColumnType("int");

                    b.Property<string>("surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Celebrities");
                });

            modelBuilder.Entity("Prueba11.Models.CommentCelebrity", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Celebrityid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ParentCommentCelebrityid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("userName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("id");

                    b.HasIndex("Celebrityid");

                    b.HasIndex("ParentCommentCelebrityid");

                    b.HasIndex("userName");

                    b.ToTable("CommentCelebrities");
                });

            modelBuilder.Entity("Prueba11.Models.CommentItem", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Itemid")
                        .HasColumnType("int");

                    b.Property<string>("ParentCommentItemid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("userName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("id");

                    b.HasIndex("Itemid");

                    b.HasIndex("ParentCommentItemid");

                    b.HasIndex("userName");

                    b.ToTable("CommentItems");
                });

            modelBuilder.Entity("Prueba11.Models.Item", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("budget")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("director")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("duration")
                        .HasColumnType("int");

                    b.Property<string>("earns")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("genre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("language")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("productor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("productorCountry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("rating")
                        .HasColumnType("float");

                    b.Property<string>("releaseDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("stars")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("subtitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("writers")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("year")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Prueba11.Models.LinkedCelebrityWithCelebrity", b =>
                {
                    b.Property<string>("celebrityId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("celebrityId2")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("celebrityId1", "celebrityId2");

                    b.ToTable("LinkedCelebrityWithCelebrities");
                });

            modelBuilder.Entity("Prueba11.Models.LinkedItemWithCelebrity", b =>
                {
                    b.Property<string>("itemId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("celebrityId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("itemId", "celebrityId");

                    b.ToTable("LinkedItemWithCelebrities");
                });

            modelBuilder.Entity("Prueba11.Models.LinkedItemWithItem", b =>
                {
                    b.Property<string>("itemId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("itemId2")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("itemId1", "itemId2");

                    b.ToTable("LinkedItemWithItems");
                });

            modelBuilder.Entity("Prueba11.Models.RatingCelebrity", b =>
                {
                    b.Property<string>("celebrityId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("userName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("rating")
                        .HasColumnType("int");

                    b.HasKey("celebrityId", "userName");

                    b.ToTable("RatingCelebrities");
                });

            modelBuilder.Entity("Prueba11.Models.RatingItem", b =>
                {
                    b.Property<string>("userName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("itemId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("rating")
                        .HasColumnType("int");

                    b.HasKey("userName", "itemId");

                    b.ToTable("RatingItems");
                });

            modelBuilder.Entity("Prueba11.Models.ReactionCommentCelebrity", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CommentCelebrityid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("reactionType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("id");

                    b.HasIndex("CommentCelebrityid");

                    b.HasIndex("userName");

                    b.ToTable("ReactionCommentCelebritys");
                });

            modelBuilder.Entity("Prueba11.Models.ReactionCommentItem", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CommentItemid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("reactionType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("id");

                    b.HasIndex("CommentItemid");

                    b.HasIndex("userName");

                    b.ToTable("ReactionCommentItems");
                });

            modelBuilder.Entity("Prueba11.Models.Session", b =>
                {
                    b.Property<string>("token")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("expired")
                        .HasColumnType("bit");

                    b.Property<string>("issuedAt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("token");

                    b.HasIndex("userName");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("Prueba11.Models.User", b =>
                {
                    b.Property<string>("userName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("role")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userName");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Prueba11.Models.CommentCelebrity", b =>
                {
                    b.HasOne("Prueba11.Models.Celebrity", "Celebrity")
                        .WithMany()
                        .HasForeignKey("Celebrityid");

                    b.HasOne("Prueba11.Models.CommentCelebrity", "ParentCommentCelebrity")
                        .WithMany()
                        .HasForeignKey("ParentCommentCelebrityid");

                    b.HasOne("Prueba11.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("userName");
                });

            modelBuilder.Entity("Prueba11.Models.CommentItem", b =>
                {
                    b.HasOne("Prueba11.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("Itemid");

                    b.HasOne("Prueba11.Models.CommentItem", "ParentCommentItem")
                        .WithMany()
                        .HasForeignKey("ParentCommentItemid");

                    b.HasOne("Prueba11.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("userName");
                });

            modelBuilder.Entity("Prueba11.Models.ReactionCommentCelebrity", b =>
                {
                    b.HasOne("Prueba11.Models.CommentCelebrity", "CommentCelebrity")
                        .WithMany()
                        .HasForeignKey("CommentCelebrityid");

                    b.HasOne("Prueba11.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("userName");
                });

            modelBuilder.Entity("Prueba11.Models.ReactionCommentItem", b =>
                {
                    b.HasOne("Prueba11.Models.CommentItem", "CommentItem")
                        .WithMany()
                        .HasForeignKey("CommentItemid");

                    b.HasOne("Prueba11.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("userName");
                });

            modelBuilder.Entity("Prueba11.Models.Session", b =>
                {
                    b.HasOne("Prueba11.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("userName");
                });
#pragma warning restore 612, 618
        }
    }
}
