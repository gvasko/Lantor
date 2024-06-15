﻿// <auto-generated />
using Lantor.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Lantor.Data.Migrations
{
    [DbContext(typeof(LantorContext))]
    partial class LantorContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Lantor.DomainModel.Alphabet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Alphabets");
                });

            modelBuilder.Entity("Lantor.DomainModel.LanguageSample", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MultilingualSampleId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sample")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MultilingualSampleId");

                    b.ToTable("LanguageSamples");
                });

            modelBuilder.Entity("Lantor.DomainModel.LanguageVectorCache", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AlphabetId")
                        .HasColumnType("int");

                    b.Property<int>("LanguageSampleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AlphabetId");

                    b.HasIndex("LanguageSampleId");

                    b.ToTable("LanguageVectorCache");
                });

            modelBuilder.Entity("Lantor.DomainModel.MultilingualSample", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MultilingualSamples");
                });

            modelBuilder.Entity("Lantor.DomainModel.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExternalId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Lantor.DomainModel.Alphabet", b =>
                {
                    b.OwnsMany("Lantor.DomainModel.LetterVector", "LetterVectors", b1 =>
                        {
                            b1.Property<int>("AlphabetId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<string>("Letter")
                                .IsRequired()
                                .HasColumnType("nvarchar(1)");

                            b1.HasKey("AlphabetId", "Id");

                            b1.ToTable("LetterVector");

                            b1.WithOwner()
                                .HasForeignKey("AlphabetId");

                            b1.OwnsOne("Lantor.DomainModel.HiDimBipolarVector", "Vector", b2 =>
                                {
                                    b2.Property<int>("LetterVectorAlphabetId")
                                        .HasColumnType("int");

                                    b2.Property<int>("LetterVectorId")
                                        .HasColumnType("int");

                                    b2.Property<byte[]>("Data")
                                        .IsRequired()
                                        .HasColumnType("varbinary(max)");

                                    b2.HasKey("LetterVectorAlphabetId", "LetterVectorId");

                                    b2.ToTable("LetterVector");

                                    b2.WithOwner()
                                        .HasForeignKey("LetterVectorAlphabetId", "LetterVectorId");
                                });

                            b1.Navigation("Vector")
                                .IsRequired();
                        });

                    b.Navigation("LetterVectors");
                });

            modelBuilder.Entity("Lantor.DomainModel.LanguageSample", b =>
                {
                    b.HasOne("Lantor.DomainModel.MultilingualSample", null)
                        .WithMany("Languages")
                        .HasForeignKey("MultilingualSampleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Lantor.DomainModel.LanguageVectorCache", b =>
                {
                    b.HasOne("Lantor.DomainModel.Alphabet", "Alphabet")
                        .WithMany()
                        .HasForeignKey("AlphabetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lantor.DomainModel.LanguageSample", "LanguageSample")
                        .WithMany()
                        .HasForeignKey("LanguageSampleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Lantor.DomainModel.HiDimBipolarVector", "Vector", b1 =>
                        {
                            b1.Property<int>("LanguageVectorCacheId")
                                .HasColumnType("int");

                            b1.Property<byte[]>("Data")
                                .IsRequired()
                                .HasColumnType("varbinary(max)");

                            b1.HasKey("LanguageVectorCacheId");

                            b1.ToTable("LanguageVectorCache");

                            b1.WithOwner()
                                .HasForeignKey("LanguageVectorCacheId");
                        });

                    b.Navigation("Alphabet");

                    b.Navigation("LanguageSample");

                    b.Navigation("Vector")
                        .IsRequired();
                });

            modelBuilder.Entity("Lantor.DomainModel.MultilingualSample", b =>
                {
                    b.Navigation("Languages");
                });
#pragma warning restore 612, 618
        }
    }
}
