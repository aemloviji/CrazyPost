﻿// <auto-generated />
using CrazyPost.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace CrazyPost.Migrations
{
    [DbContext(typeof(Contexts.ApiDbContext))]
    [Migration("20180418173708_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CrazyPost.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("InsertDate");

                    b.Property<int>("PostId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("CrazyPost.Models.Post", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("InsertDate");

                    b.Property<string>("Text");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("ID");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("CrazyPost.Models.Comment", b =>
                {
                    b.HasOne("CrazyPost.Models.Post", "Post")
                        .WithMany("Comment")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
