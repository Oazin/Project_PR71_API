﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Project_PR71_API.Configuration;

#nullable disable

namespace Project_PR71_API.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Project_PR71_API.Models.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("User1Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("User2Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("User1Email");

                    b.HasIndex("User2Email");

                    b.ToTable("Chat");
                });

            modelBuilder.Entity("Project_PR71_API.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PostId")
                        .HasColumnType("integer");

                    b.Property<string>("WriterEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("PostId");

                    b.HasIndex("WriterEmail");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("Project_PR71_API.Models.Follow", b =>
                {
                    b.Property<string>("FollowingEmail")
                        .HasColumnType("text");

                    b.Property<string>("FollowerEmail")
                        .HasColumnType("text");

                    b.HasKey("FollowingEmail", "FollowerEmail");

                    b.HasIndex("FollowerEmail");

                    b.HasIndex("FollowingEmail", "FollowerEmail")
                        .IsUnique();

                    b.ToTable("Follow");
                });

            modelBuilder.Entity("Project_PR71_API.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Index")
                        .HasColumnType("integer");

                    b.Property<byte[]>("Picture")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<int>("PostId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("PostId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("Project_PR71_API.Models.Like", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("PostId")
                        .HasColumnType("integer");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("PostId");

                    b.HasIndex("UserEmail");

                    b.ToTable("Like");
                });

            modelBuilder.Entity("Project_PR71_API.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChatId")
                        .HasColumnType("integer");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SenderEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("SenderEmail");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("Project_PR71_API.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("UserEmail");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("Project_PR71_API.Models.SavePost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("PostId")
                        .HasColumnType("integer");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("PostId");

                    b.HasIndex("UserEmail");

                    b.ToTable("SavePost");
                });

            modelBuilder.Entity("Project_PR71_API.Models.Story", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("UserEmail");

                    b.ToTable("Story");
                });

            modelBuilder.Entity("Project_PR71_API.Models.User", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Bio")
                        .HasColumnType("text");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("Picture")
                        .HasColumnType("bytea");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Email");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("Project_PR71_API.Models.Chat", b =>
                {
                    b.HasOne("Project_PR71_API.Models.User", "User1")
                        .WithMany()
                        .HasForeignKey("User1Email")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_PR71_API.Models.User", "User2")
                        .WithMany()
                        .HasForeignKey("User2Email")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User1");

                    b.Navigation("User2");
                });

            modelBuilder.Entity("Project_PR71_API.Models.Comment", b =>
                {
                    b.HasOne("Project_PR71_API.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_PR71_API.Models.User", "Writer")
                        .WithMany("Comments")
                        .HasForeignKey("WriterEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Writer");
                });

            modelBuilder.Entity("Project_PR71_API.Models.Follow", b =>
                {
                    b.HasOne("Project_PR71_API.Models.User", "Follower")
                        .WithMany("Followings")
                        .HasForeignKey("FollowerEmail")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Project_PR71_API.Models.User", "Following")
                        .WithMany("Followers")
                        .HasForeignKey("FollowingEmail")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Follower");

                    b.Navigation("Following");
                });

            modelBuilder.Entity("Project_PR71_API.Models.Image", b =>
                {
                    b.HasOne("Project_PR71_API.Models.Post", "Post")
                        .WithMany("Images")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Project_PR71_API.Models.Like", b =>
                {
                    b.HasOne("Project_PR71_API.Models.Post", "Post")
                        .WithMany("Likes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_PR71_API.Models.User", "User")
                        .WithMany("Likes")
                        .HasForeignKey("UserEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Project_PR71_API.Models.Message", b =>
                {
                    b.HasOne("Project_PR71_API.Models.Chat", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_PR71_API.Models.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderEmail")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Project_PR71_API.Models.Post", b =>
                {
                    b.HasOne("Project_PR71_API.Models.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Project_PR71_API.Models.SavePost", b =>
                {
                    b.HasOne("Project_PR71_API.Models.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_PR71_API.Models.User", "User")
                        .WithMany("SavePosts")
                        .HasForeignKey("UserEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Project_PR71_API.Models.Story", b =>
                {
                    b.HasOne("Project_PR71_API.Models.User", "User")
                        .WithMany("Stories")
                        .HasForeignKey("UserEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Project_PR71_API.Models.Chat", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Project_PR71_API.Models.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Images");

                    b.Navigation("Likes");
                });

            modelBuilder.Entity("Project_PR71_API.Models.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Followers");

                    b.Navigation("Followings");

                    b.Navigation("Likes");

                    b.Navigation("Posts");

                    b.Navigation("SavePosts");

                    b.Navigation("Stories");
                });
#pragma warning restore 612, 618
        }
    }
}
