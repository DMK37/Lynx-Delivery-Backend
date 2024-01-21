﻿// <auto-generated />
using System;
using CourierAppBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CourierAppBackend.Migrations
{
    [DbContext(typeof(CourierAppContext))]
    [Migration("20240121121100_NewFieldInTemporaryOffers")]
    partial class NewFieldInTemporaryOffers
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CourierAppBackend.Models.Database.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ApartmentNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HouseNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("CourierAppBackend.Models.Database.Inquiry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CourierCompanyName")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateOfInquiring")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeliveringCompany")
                        .HasColumnType("text");

                    b.Property<bool>("DeliveryAtWeekend")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("DeliveryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("DestinationAddressId")
                        .HasColumnType("integer");

                    b.Property<bool>("HighPriority")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsCompany")
                        .HasColumnType("boolean");

                    b.Property<int?>("OfferID")
                        .HasColumnType("integer");

                    b.Property<DateTime>("PickupDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("SourceAddressId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DestinationAddressId");

                    b.HasIndex("OfferID")
                        .IsUnique();

                    b.HasIndex("SourceAddressId");

                    b.ToTable("Inquiries");
                });

            modelBuilder.Entity("CourierAppBackend.Models.Database.Offer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("OrderID")
                        .HasColumnType("integer");

                    b.Property<string>("ReasonOfRejection")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("CourierAppBackend.Models.Database.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<string>("CourierName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("OfferID")
                        .HasColumnType("integer");

                    b.Property<int>("OfferId")
                        .HasColumnType("integer");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OfferId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("CourierAppBackend.Models.Database.PriceItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("TemporaryOfferId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TemporaryOfferId");

                    b.ToTable("PriceItems");
                });

            modelBuilder.Entity("CourierAppBackend.Models.Database.TemporaryOffer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ExpiringAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("InquiryID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("InquiryId")
                        .HasColumnType("integer");

                    b.Property<string>("OfferID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OfferRequestId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("InquiryId");

                    b.ToTable("TemporaryOffers");
                });

            modelBuilder.Entity("CourierAppBackend.Models.Database.UserInfo", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<int>("AddressId")
                        .HasColumnType("integer");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("DefaultSourceAddressId")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.HasIndex("AddressId");

                    b.HasIndex("DefaultSourceAddressId");

                    b.ToTable("UsersInfos");
                });

            modelBuilder.Entity("CourierAppBackend.Models.Database.Inquiry", b =>
                {
                    b.HasOne("CourierAppBackend.Models.Database.Address", "DestinationAddress")
                        .WithMany()
                        .HasForeignKey("DestinationAddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CourierAppBackend.Models.Database.Offer", null)
                        .WithOne("Inquiry")
                        .HasForeignKey("CourierAppBackend.Models.Database.Inquiry", "OfferID");

                    b.HasOne("CourierAppBackend.Models.Database.Address", "SourceAddress")
                        .WithMany()
                        .HasForeignKey("SourceAddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("CourierAppBackend.Models.Database.Package", "Package", b1 =>
                        {
                            b1.Property<int>("InquiryId")
                                .HasColumnType("integer");

                            b1.Property<int>("Height")
                                .HasColumnType("integer");

                            b1.Property<int>("Length")
                                .HasColumnType("integer");

                            b1.Property<float>("Weight")
                                .HasColumnType("real");

                            b1.Property<int>("Width")
                                .HasColumnType("integer");

                            b1.HasKey("InquiryId");

                            b1.ToTable("Inquiries");

                            b1.WithOwner()
                                .HasForeignKey("InquiryId");
                        });

                    b.Navigation("DestinationAddress");

                    b.Navigation("Package")
                        .IsRequired();

                    b.Navigation("SourceAddress");
                });

            modelBuilder.Entity("CourierAppBackend.Models.Database.Offer", b =>
                {
                    b.OwnsOne("CourierAppBackend.Models.Database.CustomerInfo", "CustomerInfo", b1 =>
                        {
                            b1.Property<int>("OfferId")
                                .HasColumnType("integer");

                            b1.Property<int>("AddressId")
                                .HasColumnType("integer");

                            b1.Property<string>("CompanyName")
                                .HasColumnType("text");

                            b1.Property<string>("Email")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("OfferId");

                            b1.HasIndex("AddressId");

                            b1.ToTable("Offers");

                            b1.HasOne("CourierAppBackend.Models.Database.Address", "Address")
                                .WithMany()
                                .HasForeignKey("AddressId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("OfferId");

                            b1.Navigation("Address");
                        });

                    b.OwnsOne("CourierAppBackend.Models.Database.Price", "Price", b1 =>
                        {
                            b1.Property<int>("OfferId")
                                .HasColumnType("integer");

                            b1.Property<decimal>("BaseDeliveryPrice")
                                .HasColumnType("numeric");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<decimal>("DeliveryAtWeekendFee")
                                .HasColumnType("numeric");

                            b1.Property<decimal>("FullPrice")
                                .HasColumnType("numeric");

                            b1.Property<decimal>("PriorityFee")
                                .HasColumnType("numeric");

                            b1.Property<decimal>("SizeFee")
                                .HasColumnType("numeric");

                            b1.Property<decimal>("WeightFee")
                                .HasColumnType("numeric");

                            b1.HasKey("OfferId");

                            b1.ToTable("Offers");

                            b1.WithOwner()
                                .HasForeignKey("OfferId");
                        });

                    b.Navigation("CustomerInfo");

                    b.Navigation("Price")
                        .IsRequired();
                });

            modelBuilder.Entity("CourierAppBackend.Models.Database.Order", b =>
                {
                    b.HasOne("CourierAppBackend.Models.Database.Offer", "Offer")
                        .WithMany()
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Offer");
                });

            modelBuilder.Entity("CourierAppBackend.Models.Database.PriceItem", b =>
                {
                    b.HasOne("CourierAppBackend.Models.Database.TemporaryOffer", null)
                        .WithMany("PriceItems")
                        .HasForeignKey("TemporaryOfferId");
                });

            modelBuilder.Entity("CourierAppBackend.Models.Database.TemporaryOffer", b =>
                {
                    b.HasOne("CourierAppBackend.Models.Database.Inquiry", "Inquiry")
                        .WithMany()
                        .HasForeignKey("InquiryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inquiry");
                });

            modelBuilder.Entity("CourierAppBackend.Models.Database.UserInfo", b =>
                {
                    b.HasOne("CourierAppBackend.Models.Database.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CourierAppBackend.Models.Database.Address", "DefaultSourceAddress")
                        .WithMany()
                        .HasForeignKey("DefaultSourceAddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("DefaultSourceAddress");
                });

            modelBuilder.Entity("CourierAppBackend.Models.Database.Offer", b =>
                {
                    b.Navigation("Inquiry")
                        .IsRequired();
                });

            modelBuilder.Entity("CourierAppBackend.Models.Database.TemporaryOffer", b =>
                {
                    b.Navigation("PriceItems");
                });
#pragma warning restore 612, 618
        }
    }
}
