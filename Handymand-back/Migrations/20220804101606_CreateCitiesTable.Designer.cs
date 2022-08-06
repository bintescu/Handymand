﻿// <auto-generated />
using System;
using Handymand.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Handymand.Migrations
{
    [DbContext(typeof(HandymandContext))]
    [Migration("20220804101606_CreateCitiesTable")]
    partial class CreateCitiesTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Handymand.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Handymand.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rating")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdUser")
                        .IsUnique();

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Handymand.Models.Contract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ComplexityGrade")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExpectedDurationInHours")
                        .HasColumnType("int");

                    b.Property<int>("IdCreationUser")
                        .HasColumnType("int");

                    b.Property<int?>("IdRefferedUser")
                        .HasColumnType("int");

                    b.Property<decimal>("PaymentAmount")
                        .HasPrecision(12, 2)
                        .HasColumnType("decimal(12,2)");

                    b.HasKey("Id");

                    b.HasIndex("IdCreationUser");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("Handymand.Models.ContractsSkills", b =>
                {
                    b.Property<int>("IdContract")
                        .HasColumnType("int");

                    b.Property<int>("IdSkill")
                        .HasColumnType("int");

                    b.HasKey("IdContract", "IdSkill");

                    b.HasIndex("IdSkill");

                    b.ToTable("ContractsSkills");
                });

            modelBuilder.Entity("Handymand.Models.Freelancer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<string>("Overview")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdUser")
                        .IsUnique();

                    b.ToTable("Freelancers");
                });

            modelBuilder.Entity("Handymand.Models.FreelancersSkills", b =>
                {
                    b.Property<int>("IdFreelancer")
                        .HasColumnType("int");

                    b.Property<int>("IdSkill")
                        .HasColumnType("int");

                    b.Property<double>("Grade")
                        .HasColumnType("float");

                    b.HasKey("IdFreelancer", "IdSkill");

                    b.HasIndex("IdSkill");

                    b.ToTable("FreelancersSkills");
                });

            modelBuilder.Entity("Handymand.Models.JobOffer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreationUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("HighPriceRange")
                        .HasColumnType("float");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("LowPriceRange")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreationUserId");

                    b.ToTable("JobOffer");
                });

            modelBuilder.Entity("Handymand.Models.JobOffersSkills", b =>
                {
                    b.Property<int>("IdSkill")
                        .HasColumnType("int");

                    b.Property<int>("IdJobOffer")
                        .HasColumnType("int");

                    b.HasKey("IdSkill", "IdJobOffer");

                    b.HasIndex("IdJobOffer");

                    b.ToTable("JobOffersSkills");
                });

            modelBuilder.Entity("Handymand.Models.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreationUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime?>("DateModified")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasComputedColumnSql("GETDATE()");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ModificationUserId")
                        .HasColumnType("int");

                    b.Property<string>("SkillName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreationUserId");

                    b.HasIndex("ModificationUserId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("Handymand.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AboutMe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Amount")
                        .HasPrecision(12, 2)
                        .HasColumnType("decimal(12,2)");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Blocked")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WalletAddress")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Handymand.Models.Client", b =>
                {
                    b.HasOne("Handymand.Models.User", "User")
                        .WithOne("ClientAccount")
                        .HasForeignKey("Handymand.Models.Client", "IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Handymand.Models.Contract", b =>
                {
                    b.HasOne("Handymand.Models.User", "CreationUser")
                        .WithMany("CreatedContracts")
                        .HasForeignKey("IdCreationUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreationUser");
                });

            modelBuilder.Entity("Handymand.Models.ContractsSkills", b =>
                {
                    b.HasOne("Handymand.Models.Contract", "Contract")
                        .WithMany("ContractSkills")
                        .HasForeignKey("IdContract")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Handymand.Models.Skill", "Skill")
                        .WithMany("ContractSkills")
                        .HasForeignKey("IdSkill")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contract");

                    b.Navigation("Skill");
                });

            modelBuilder.Entity("Handymand.Models.Freelancer", b =>
                {
                    b.HasOne("Handymand.Models.User", "User")
                        .WithOne("FreelancerAccount")
                        .HasForeignKey("Handymand.Models.Freelancer", "IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Handymand.Models.FreelancersSkills", b =>
                {
                    b.HasOne("Handymand.Models.Freelancer", "Freelancer")
                        .WithMany("FreelancersSkills")
                        .HasForeignKey("IdFreelancer")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Handymand.Models.Skill", "Skill")
                        .WithMany("FreelancersSkills")
                        .HasForeignKey("IdSkill")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Freelancer");

                    b.Navigation("Skill");
                });

            modelBuilder.Entity("Handymand.Models.JobOffer", b =>
                {
                    b.HasOne("Handymand.Models.User", "CreationUser")
                        .WithMany("CreatedJobOffers")
                        .HasForeignKey("CreationUserId");

                    b.Navigation("CreationUser");
                });

            modelBuilder.Entity("Handymand.Models.JobOffersSkills", b =>
                {
                    b.HasOne("Handymand.Models.JobOffer", "JobOffer")
                        .WithMany("JobOffersSkills")
                        .HasForeignKey("IdJobOffer")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Handymand.Models.Skill", "Skill")
                        .WithMany("JobOffersSkills")
                        .HasForeignKey("IdSkill")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobOffer");

                    b.Navigation("Skill");
                });

            modelBuilder.Entity("Handymand.Models.Skill", b =>
                {
                    b.HasOne("Handymand.Models.User", "CreationUser")
                        .WithMany()
                        .HasForeignKey("CreationUserId");

                    b.HasOne("Handymand.Models.User", "ModificationUser")
                        .WithMany()
                        .HasForeignKey("ModificationUserId");

                    b.Navigation("CreationUser");

                    b.Navigation("ModificationUser");
                });

            modelBuilder.Entity("Handymand.Models.Contract", b =>
                {
                    b.Navigation("ContractSkills");
                });

            modelBuilder.Entity("Handymand.Models.Freelancer", b =>
                {
                    b.Navigation("FreelancersSkills");
                });

            modelBuilder.Entity("Handymand.Models.JobOffer", b =>
                {
                    b.Navigation("JobOffersSkills");
                });

            modelBuilder.Entity("Handymand.Models.Skill", b =>
                {
                    b.Navigation("ContractSkills");

                    b.Navigation("FreelancersSkills");

                    b.Navigation("JobOffersSkills");
                });

            modelBuilder.Entity("Handymand.Models.User", b =>
                {
                    b.Navigation("ClientAccount");

                    b.Navigation("CreatedContracts");

                    b.Navigation("CreatedJobOffers");

                    b.Navigation("FreelancerAccount");
                });
#pragma warning restore 612, 618
        }
    }
}
