﻿// <auto-generated />
using System;
using KanbanApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KanbanApp.Migrations
{
    [DbContext(typeof(KanbanAppContext))]
    partial class KanbanAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.11");

            modelBuilder.Entity("KanbanApp.Models.Board", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CreatorID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CreatorUserID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("CreatorUserID");

                    b.ToTable("Board");
                });

            modelBuilder.Entity("KanbanApp.Models.Column", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BoardID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("BoardID");

                    b.ToTable("Column");
                });

            modelBuilder.Entity("KanbanApp.Models.Issue", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("CreatorID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DeadlineDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PerformerID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Priority")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("CreatorID");

                    b.HasIndex("PerformerID");

                    b.ToTable("Issue");
                });

            modelBuilder.Entity("KanbanApp.Models.IssueColumn", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("AssignDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("ColumnID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IssueID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("ColumnID");

                    b.HasIndex("IssueID");

                    b.ToTable("IssueColumn");
                });

            modelBuilder.Entity("KanbanApp.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("HashPass")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("KanbanApp.Models.UserBoard", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BoardID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserRole")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("BoardID");

                    b.HasIndex("UserID");

                    b.ToTable("UserBoard");
                });

            modelBuilder.Entity("KanbanApp.Models.Board", b =>
                {
                    b.HasOne("KanbanApp.Models.User", "CreatorUser")
                        .WithMany()
                        .HasForeignKey("CreatorUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatorUser");
                });

            modelBuilder.Entity("KanbanApp.Models.Column", b =>
                {
                    b.HasOne("KanbanApp.Models.Board", "Board")
                        .WithMany("Columns")
                        .HasForeignKey("BoardID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Board");
                });

            modelBuilder.Entity("KanbanApp.Models.Issue", b =>
                {
                    b.HasOne("KanbanApp.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KanbanApp.Models.User", "Performer")
                        .WithMany()
                        .HasForeignKey("PerformerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("Performer");
                });

            modelBuilder.Entity("KanbanApp.Models.IssueColumn", b =>
                {
                    b.HasOne("KanbanApp.Models.Column", "Column")
                        .WithMany("IssueColumns")
                        .HasForeignKey("ColumnID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KanbanApp.Models.Issue", "Issue")
                        .WithMany("IssueColumns")
                        .HasForeignKey("IssueID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Column");

                    b.Navigation("Issue");
                });

            modelBuilder.Entity("KanbanApp.Models.UserBoard", b =>
                {
                    b.HasOne("KanbanApp.Models.Board", "Board")
                        .WithMany("UserBoards")
                        .HasForeignKey("BoardID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KanbanApp.Models.User", "User")
                        .WithMany("UserBoards")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Board");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KanbanApp.Models.Board", b =>
                {
                    b.Navigation("Columns");

                    b.Navigation("UserBoards");
                });

            modelBuilder.Entity("KanbanApp.Models.Column", b =>
                {
                    b.Navigation("IssueColumns");
                });

            modelBuilder.Entity("KanbanApp.Models.Issue", b =>
                {
                    b.Navigation("IssueColumns");
                });

            modelBuilder.Entity("KanbanApp.Models.User", b =>
                {
                    b.Navigation("UserBoards");
                });
#pragma warning restore 612, 618
        }
    }
}
