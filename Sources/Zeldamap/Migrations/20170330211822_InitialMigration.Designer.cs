using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using zeldassistant.Data;
using zeldassistant.Models;

namespace zeldassistant.Migrations
{
    [DbContext(typeof(ZeldaDataContext))]
    [Migration("20170330211822_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("zeldassistant.Models.Marker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<Guid>("Guid");

                    b.Property<bool>("IsDeleted");

                    b.Property<decimal>("Lat");

                    b.Property<decimal>("Lng");

                    b.Property<string>("Name");

                    b.Property<int>("Type");

                    b.Property<decimal>("X");

                    b.Property<decimal>("Y");

                    b.HasKey("Id");

                    b.ToTable("Markers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Marker");
                });

            modelBuilder.Entity("zeldassistant.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsInitialized");

                    b.Property<string>("Login");

                    b.Property<string>("PasswordHash");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("zeldassistant.Models.UserMarker", b =>
                {
                    b.HasBaseType("zeldassistant.Models.Marker");

                    b.Property<DateTime>("DoneTime");

                    b.Property<bool>("IsDone");

                    b.Property<int>("UserId");

                    b.ToTable("UserMarker");

                    b.HasDiscriminator().HasValue("UserMarker");
                });
        }
    }
}
