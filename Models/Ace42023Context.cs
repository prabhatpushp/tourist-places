using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TravelPlaces.Models;

public partial class Ace42023Context : DbContext
{
    public Ace42023Context()
    {
    }

    public Ace42023Context(DbContextOptions<Ace42023Context> options)
        : base(options)
    {
    }

    public virtual DbSet<PlaceImage> PlaceImages { get; set; }

    public virtual DbSet<ServiceProvider> ServiceProviders { get; set; }

    public virtual DbSet<TouristPlace> TouristPlaces { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DEVSQL.corp.local;Database=ACE 4- 2023;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlaceImage>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__place_im__DC9AC955551DE3F3");

            entity.ToTable("place_images");

            entity.Property(e => e.ImageId).HasColumnName("image_id");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("image_url");
            entity.Property(e => e.PlaceId).HasColumnName("place_id");

            entity.HasOne(d => d.Place).WithMany(p => p.PlaceImages)
                .HasForeignKey(d => d.PlaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__place_ima__place__6F7F8B4B");
        });

        modelBuilder.Entity<ServiceProvider>(entity =>
        {
            entity.HasKey(e => e.ProviderId).HasName("PK__service___00E2131085C74D5E");

            entity.ToTable("service_providers");

            entity.Property(e => e.ProviderId).HasColumnName("provider_id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.ProviderRating)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("provider_rating");
            entity.Property(e => e.ProviderType)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("provider_type");
        });

        modelBuilder.Entity<TouristPlace>(entity =>
        {
            entity.HasKey(e => e.PlaceId).HasName("PK__tourist___BF2B684AD276B7B5");

            entity.ToTable("tourist_places");

            entity.Property(e => e.PlaceId).HasColumnName("place_id");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("country");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PlaceRating)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("place_rating");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("state");

            entity.HasMany(d => d.Providers).WithMany(p => p.Places)
                .UsingEntity<Dictionary<string, object>>(
                    "PlaceServiceProvider",
                    r => r.HasOne<ServiceProvider>().WithMany()
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__place_ser__provi__73501C2F"),
                    l => l.HasOne<TouristPlace>().WithMany()
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__place_ser__place__725BF7F6"),
                    j =>
                    {
                        j.HasKey("PlaceId", "ProviderId").HasName("PK__place_se__AF25497BC7B13366");
                        j.ToTable("place_service_providers");
                        j.IndexerProperty<int>("PlaceId").HasColumnName("place_id");
                        j.IndexerProperty<int>("ProviderId").HasColumnName("provider_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
