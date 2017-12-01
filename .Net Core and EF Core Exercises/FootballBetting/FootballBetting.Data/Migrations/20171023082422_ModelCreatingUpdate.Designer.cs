﻿// <auto-generated />
using FootballBetting.Data.FottballBettingDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace FootballBetting.Data.Migrations
{
    [DbContext(typeof(FootballBettingDbContext))]
    [Migration("20171023082422_ModelCreatingUpdate")]
    partial class ModelCreatingUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FootballBetting.Data.Models.Bet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BetTime");

                    b.Property<decimal>("Money");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Bets");
                });

            modelBuilder.Entity("FootballBetting.Data.Models.BetGame", b =>
                {
                    b.Property<int>("BetId");

                    b.Property<int>("GameId");

                    b.Property<int>("ResultPredictionId");

                    b.HasKey("BetId", "GameId");

                    b.HasIndex("GameId");

                    b.HasIndex("ResultPredictionId");

                    b.ToTable("BetGames");
                });

            modelBuilder.Entity("FootballBetting.Data.Models.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("FootballBetting.Data.Models.Competition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CompetitionTypeId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CompetitionTypeId");

                    b.ToTable("Competitions");
                });

            modelBuilder.Entity("FootballBetting.Data.Models.CompetitionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("CompetitionTypes");
                });

            modelBuilder.Entity("FootballBetting.Data.Models.Continent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Continents");
                });

            modelBuilder.Entity("FootballBetting.Data.Models.Country", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(3);

                    b.Property<int>("ContinentId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ContinentId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("FootballBetting.Data.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AwayGoals");

                    b.Property<int>("AwayTeamId");

                    b.Property<decimal>("AwayTeamWinBetRate");

                    b.Property<int>("CompetitionId");

                    b.Property<DateTime>("Date");

                    b.Property<decimal>("DrawGameBetRate");

                    b.Property<int>("HomeGoals");

                    b.Property<int>("HomeTeamId");

                    b.Property<decimal>("HomeTeamWinBetRate");

                    b.Property<int>("RoundId");

                    b.HasKey("Id");

                    b.HasIndex("AwayTeamId");

                    b.HasIndex("CompetitionId");

                    b.HasIndex("HomeTeamId");

                    b.HasIndex("RoundId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("FootballBetting.Data.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsInjured");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PositionId");

                    b.Property<int>("SquadNumber");

                    b.Property<int>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("PositionId");

                    b.HasIndex("TeamId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("FootballBetting.Data.Models.PlayerStatistic", b =>
                {
                    b.Property<int>("GameId");

                    b.Property<int>("PlayerId");

                    b.Property<int>("Assists");

                    b.Property<int>("PlayedMinutesPerGame");

                    b.Property<int>("ScoredGoals");

                    b.HasKey("GameId", "PlayerId");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayerStatistics");
                });

            modelBuilder.Entity("FootballBetting.Data.Models.Position", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(2);

                    b.Property<string>("Description")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("FootballBetting.Data.Models.ResultPrediction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Prediction");

                    b.HasKey("Id");

                    b.ToTable("ResultPredictions");
                });

            modelBuilder.Entity("FootballBetting.Data.Models.Round", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Rounds");
                });

            modelBuilder.Entity("FootballBetting.Data.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Budget");

                    b.Property<string>("Initials")
                        .IsRequired()
                        .HasMaxLength(3);

                    b.Property<byte[]>("Logo")
                        .HasMaxLength(1024);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("PrimaryKitColorId");

                    b.Property<int>("SecondaryKitColorId");

                    b.Property<int>("TownId");

                    b.HasKey("Id");

                    b.HasIndex("PrimaryKitColorId");

                    b.HasIndex("SecondaryKitColorId");

                    b.HasIndex("TownId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("FootballBetting.Data.Models.Town", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CountryId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Towns");
                });

            modelBuilder.Entity("FootballBetting.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Balance");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FullName");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FootballBetting.Data.Models.BetGame", b =>
                {
                    b.HasOne("FootballBetting.Data.Models.Bet", "Bet")
                        .WithMany("BetGames")
                        .HasForeignKey("BetId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FootballBetting.Data.Models.Game", "Game")
                        .WithMany("BetGames")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FootballBetting.Data.Models.ResultPrediction", "ResultPrediction")
                        .WithMany("BetGames")
                        .HasForeignKey("ResultPredictionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FootballBetting.Data.Models.Competition", b =>
                {
                    b.HasOne("FootballBetting.Data.Models.CompetitionType", "Type")
                        .WithMany("Competitions")
                        .HasForeignKey("CompetitionTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FootballBetting.Data.Models.Country", b =>
                {
                    b.HasOne("FootballBetting.Data.Models.Continent", "Continent")
                        .WithMany("Countries")
                        .HasForeignKey("ContinentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FootballBetting.Data.Models.Game", b =>
                {
                    b.HasOne("FootballBetting.Data.Models.Team", "AwayTeam")
                        .WithMany("AwayTeams")
                        .HasForeignKey("AwayTeamId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FootballBetting.Data.Models.Competition", "Competition")
                        .WithMany("Games")
                        .HasForeignKey("CompetitionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FootballBetting.Data.Models.Team", "HomeTeam")
                        .WithMany("HomeTeams")
                        .HasForeignKey("HomeTeamId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FootballBetting.Data.Models.Round", "Round")
                        .WithMany("Games")
                        .HasForeignKey("RoundId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FootballBetting.Data.Models.Player", b =>
                {
                    b.HasOne("FootballBetting.Data.Models.Position", "Position")
                        .WithMany("Players")
                        .HasForeignKey("PositionId");

                    b.HasOne("FootballBetting.Data.Models.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FootballBetting.Data.Models.PlayerStatistic", b =>
                {
                    b.HasOne("FootballBetting.Data.Models.Game", "Game")
                        .WithMany("PlayerStatistics")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FootballBetting.Data.Models.Player", "Player")
                        .WithMany("PlayerStatistics")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FootballBetting.Data.Models.Team", b =>
                {
                    b.HasOne("FootballBetting.Data.Models.Color", "PrimaryKitColor")
                        .WithMany("PrimaryKits")
                        .HasForeignKey("PrimaryKitColorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FootballBetting.Data.Models.Color", "SecondaryKitColor")
                        .WithMany("SecondaryKits")
                        .HasForeignKey("SecondaryKitColorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FootballBetting.Data.Models.Town", "Town")
                        .WithMany("Teams")
                        .HasForeignKey("TownId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FootballBetting.Data.Models.Town", b =>
                {
                    b.HasOne("FootballBetting.Data.Models.Country", "Country")
                        .WithMany("Towns")
                        .HasForeignKey("CountryId");
                });
#pragma warning restore 612, 618
        }
    }
}
