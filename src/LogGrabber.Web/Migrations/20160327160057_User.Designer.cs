using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using LogGrabber.DAL;

namespace LogGrabber.Web.Migrations
{
    [DbContext(typeof(LogGrabberDbContext))]
    [Migration("20160327160057_User")]
    partial class User
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LogGrabber.Application", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "Application");
                });

            modelBuilder.Entity("LogGrabber.Error", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CallStack")
                        .IsRequired();

                    b.Property<string>("Message")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "Error");
                });

            modelBuilder.Entity("LogGrabber.LogItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ApplicationId");

                    b.Property<int?>("ErrorId");

                    b.Property<string>("Message")
                        .IsRequired();

                    b.Property<DateTime>("Occured");

                    b.Property<int>("Status");

                    b.Property<long?>("UserId");

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "LogItem");
                });

            modelBuilder.Entity("LogGrabber.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 32);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 32);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();
                });

            modelBuilder.Entity("LogGrabber.LogItem", b =>
                {
                    b.HasOne("LogGrabber.Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId");

                    b.HasOne("LogGrabber.Error")
                        .WithMany()
                        .HasForeignKey("ErrorId");

                    b.HasOne("LogGrabber.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
        }
    }
}
