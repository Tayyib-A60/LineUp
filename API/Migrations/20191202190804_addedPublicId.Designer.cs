﻿// <auto-generated />
using System;
using API.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace api.Migrations
{
    [DbContext(typeof(LineUpContext))]
    [Migration("20191202190804_addedPublicId")]
    partial class addedPublicId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("API.Core.Models.Amenity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<double>("Price");

                    b.Property<int?>("SpaceId");

                    b.HasKey("Id");

                    b.HasIndex("SpaceId");

                    b.ToTable("Amenities");
                });

            modelBuilder.Entity("API.Core.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookedById");

                    b.Property<DateTime>("BookingTime");

                    b.Property<int?>("ChatId");

                    b.Property<int?>("SpaceBookedId");

                    b.Property<int>("Status");

                    b.Property<double>("TotalPrice");

                    b.Property<int>("UserId");

                    b.Property<DateTime>("UsingFrom");

                    b.Property<DateTime>("UsingTill");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("SpaceBookedId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("API.Core.Models.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("Chat");
                });

            modelBuilder.Entity("API.Core.Models.ChatMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("By");

                    b.Property<int?>("ChatId");

                    b.Property<string>("Message");

                    b.Property<DateTime>("Time");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.ToTable("ChatMessage");
                });

            modelBuilder.Entity("API.Core.Models.Enquiry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Message");

                    b.Property<int>("SpaceId");

                    b.Property<DateTime>("Time");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Enquiries");
                });

            modelBuilder.Entity("API.Core.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Lat");

                    b.Property<string>("Long");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("API.Core.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("FileName");

                    b.Property<bool>("IsMain");

                    b.Property<string>("PublicId");

                    b.Property<int>("SpaceId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("SpaceId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("API.Core.Models.Pricing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Discount");

                    b.Property<double>("Price");

                    b.HasKey("Id");

                    b.ToTable("Pricing");
                });

            modelBuilder.Entity("API.Core.Models.Space", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Description");

                    b.Property<int?>("LocationId");

                    b.Property<string>("Name");

                    b.Property<int?>("PricePDId");

                    b.Property<int?>("PricePHId");

                    b.Property<int?>("PricePWId");

                    b.Property<string>("Size");

                    b.Property<int?>("TypeId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("PricePDId");

                    b.HasIndex("PricePHId");

                    b.HasIndex("PricePWId");

                    b.HasIndex("TypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Spaces");
                });

            modelBuilder.Entity("API.Core.Models.SpaceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("SpaceTypes");
                });

            modelBuilder.Entity("API.Core.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContactNo")
                        .HasMaxLength(30);

                    b.Property<DateTime>("DateRegistered");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("EmailVerified");

                    b.Property<bool>("Enabled");

                    b.Property<string>("Facebook");

                    b.Property<string>("Instagram");

                    b.Property<string>("LinkedIn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("OtherContactNo")
                        .HasMaxLength(30);

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired();

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired();

                    b.Property<int>("Role");

                    b.Property<string>("Twitter");

                    b.Property<bool>("VerifiedAsMerchant");

                    b.Property<string>("Whatsapp");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("API.Core.Models.Amenity", b =>
                {
                    b.HasOne("API.Core.Models.Space")
                        .WithMany("Amenities")
                        .HasForeignKey("SpaceId");
                });

            modelBuilder.Entity("API.Core.Models.Booking", b =>
                {
                    b.HasOne("API.Core.Models.Chat", "Chat")
                        .WithMany()
                        .HasForeignKey("ChatId");

                    b.HasOne("API.Core.Models.Space", "SpaceBooked")
                        .WithMany()
                        .HasForeignKey("SpaceBookedId");

                    b.HasOne("API.Core.Models.User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("API.Core.Models.ChatMessage", b =>
                {
                    b.HasOne("API.Core.Models.Chat")
                        .WithMany("ChatMessages")
                        .HasForeignKey("ChatId");
                });

            modelBuilder.Entity("API.Core.Models.Enquiry", b =>
                {
                    b.HasOne("API.Core.Models.User")
                        .WithMany("Enquiries")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("API.Core.Models.Photo", b =>
                {
                    b.HasOne("API.Core.Models.Space")
                        .WithMany("Photos")
                        .HasForeignKey("SpaceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("API.Core.Models.User")
                        .WithOne("Photo")
                        .HasForeignKey("API.Core.Models.Photo", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("API.Core.Models.Space", b =>
                {
                    b.HasOne("API.Core.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.HasOne("API.Core.Models.Pricing", "PricePD")
                        .WithMany()
                        .HasForeignKey("PricePDId");

                    b.HasOne("API.Core.Models.Pricing", "PricePH")
                        .WithMany()
                        .HasForeignKey("PricePHId");

                    b.HasOne("API.Core.Models.Pricing", "PricePW")
                        .WithMany()
                        .HasForeignKey("PricePWId");

                    b.HasOne("API.Core.Models.SpaceType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");

                    b.HasOne("API.Core.Models.User")
                        .WithMany("Spaces")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}