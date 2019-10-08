﻿// <auto-generated />
using System;
using API.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace api.Migrations
{
    [DbContext(typeof(LineUpContext))]
    partial class LineUpContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<int?>("UserId");

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

                    b.Property<int?>("ChatId");

                    b.Property<int>("ClientId");

                    b.Property<int>("MerchantId");

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

            modelBuilder.Entity("API.Core.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileName");

                    b.Property<bool>("IsMain");

                    b.Property<int>("SpaceId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("SpaceId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("API.Core.Models.Space", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Description");

                    b.Property<string>("Location");

                    b.Property<string>("Name");

                    b.Property<double>("Price");

                    b.Property<string>("Size");

                    b.Property<int?>("TypeId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

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
                        .IsRequired()
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
                        .HasForeignKey("UserId");
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
                    b.HasOne("API.Core.Models.SpaceType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");

                    b.HasOne("API.Core.Models.User")
                        .WithMany("Spaces")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
