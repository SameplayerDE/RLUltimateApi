using Microsoft.EntityFrameworkCore;
using RLUltimateApi.Models;

namespace RLUltimateApi.Controllers
{
    public partial class UltimateContext : DbContext
    {

        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Entry> Entries { get; set; }
        public virtual DbSet<EntryType> EntryTypes { get; set; }
        public virtual DbSet<Measurement> Measurements { get; set; }

        public UltimateContext()
        {
        }

        public UltimateContext(DbContextOptions<UltimateContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("players");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<EntryType>(entity =>
            {
                entity.ToTable("entityTypes");

                entity.HasIndex(e => e.Identifier, "identifier")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Identifier)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("identifier");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<Entry>(entity =>
            {
                entity.HasKey(e => new { e.MeasurementId, e.TypeId })
                    .HasName("PRIMARY");

                entity.ToTable("entries");

                entity.HasIndex(e => e.MeasurementId, "measurement_id");
                entity.HasIndex(e => e.TypeId, "type_id");

                entity.Property(e => e.MeasurementId)
                    .HasColumnType("int(11)")
                    .HasColumnName("collection_id");

                entity.Property(e => e.TypeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("type_id");


                entity.Property(e => e.Value)
                    .HasColumnType("int(11)")
                    .HasColumnName("value")
                    .HasDefaultValueSql("'0'");

                entity.HasOne(d => d.Measurement)
                    .WithMany(p => p.Entries)
                    .HasForeignKey(d => d.MeasurementId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("entries_midfk_1");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Entries)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("entries_tidfl_1");
            });

            modelBuilder.Entity<Measurement>(entity =>
            {
                entity.ToTable("measurement");

                entity.HasIndex(e => e.PlayerId, "player_id");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.PlayerId)
                    .HasColumnType("int(11)")
                    .HasColumnName("player_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP()");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
