﻿// <auto-generated />
using System;
using CCAMPServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CCAMPServer.Data.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    partial class ApplicationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CCAMPServerModel.Models.Advertisement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CampaignId");

                    b.Property<Guid>("Guid");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("StorageURLMediaPath")
                        .IsRequired();

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CampaignId");

                    b.ToTable("Advertisement");
                });

            modelBuilder.Entity("CCAMPServerModel.Models.Campaign", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate");

                    b.Property<Guid>("Guid");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("SponsorId");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("SponsorId");

                    b.ToTable("Campaign");
                });

            modelBuilder.Entity("CCAMPServerModel.Models.Channel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ContentCreatorId");

                    b.Property<DateTime>("CreationDate");

                    b.Property<Guid>("Guid");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ContentCreatorId");

                    b.ToTable("Channel");
                });

            modelBuilder.Entity("CCAMPServerModel.Models.Content", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ChannelId");

                    b.Property<Guid>("Guid");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<string>("URLMediaPath")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ChannelId");

                    b.ToTable("Content");
                });

            modelBuilder.Entity("CCAMPServerModel.Models.ContentCreator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuthToken")
                        .IsRequired();

                    b.Property<string>("CompanyName");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<Guid>("Guid");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Status");

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("ContentCreator");
                });

            modelBuilder.Entity("CCAMPServerModel.Models.Deal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CampaignId");

                    b.Property<int?>("ChannelId");

                    b.Property<Guid>("Guid");

                    b.HasKey("Id");

                    b.HasIndex("CampaignId");

                    b.HasIndex("ChannelId");

                    b.ToTable("Deal");
                });

            modelBuilder.Entity("CCAMPServerModel.Models.Sponsor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuthToken")
                        .IsRequired();

                    b.Property<string>("CompanyName")
                        .IsRequired();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<Guid>("Guid");

                    b.Property<int>("Status");

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Sponsor");
                });

            modelBuilder.Entity("CCAMPServerModel.Models.SupportUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuthToken")
                        .IsRequired();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<Guid>("Guid");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Status");

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("SupportUser");
                });

            modelBuilder.Entity("CCAMPServerModel.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AdvertisementId");

                    b.Property<int?>("ContentId");

                    b.Property<DateTime>("CreationDate");

                    b.Property<Guid>("Guid");

                    b.HasKey("Id");

                    b.HasIndex("AdvertisementId");

                    b.HasIndex("ContentId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("CCAMPServerModel.Models.Advertisement", b =>
                {
                    b.HasOne("CCAMPServerModel.Models.Campaign", "Campaign")
                        .WithMany("AdvertisementList")
                        .HasForeignKey("CampaignId");
                });

            modelBuilder.Entity("CCAMPServerModel.Models.Campaign", b =>
                {
                    b.HasOne("CCAMPServerModel.Models.Sponsor", "Sponsor")
                        .WithMany("CampaigntList")
                        .HasForeignKey("SponsorId");
                });

            modelBuilder.Entity("CCAMPServerModel.Models.Channel", b =>
                {
                    b.HasOne("CCAMPServerModel.Models.ContentCreator", "ContentCreator")
                        .WithMany("ChannelList")
                        .HasForeignKey("ContentCreatorId");
                });

            modelBuilder.Entity("CCAMPServerModel.Models.Content", b =>
                {
                    b.HasOne("CCAMPServerModel.Models.Channel", "Channel")
                        .WithMany("ContentList")
                        .HasForeignKey("ChannelId");
                });

            modelBuilder.Entity("CCAMPServerModel.Models.Deal", b =>
                {
                    b.HasOne("CCAMPServerModel.Models.Campaign", "Campaign")
                        .WithMany("DealList")
                        .HasForeignKey("CampaignId");

                    b.HasOne("CCAMPServerModel.Models.Channel", "Channel")
                        .WithMany("DealList")
                        .HasForeignKey("ChannelId");
                });

            modelBuilder.Entity("CCAMPServerModel.Models.Transaction", b =>
                {
                    b.HasOne("CCAMPServerModel.Models.Advertisement", "Advertisement")
                        .WithMany("TransactionList")
                        .HasForeignKey("AdvertisementId");

                    b.HasOne("CCAMPServerModel.Models.Content", "Content")
                        .WithMany("TransactionList")
                        .HasForeignKey("ContentId");
                });
#pragma warning restore 612, 618
        }
    }
}
