using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace projetfinalFJO.Appdata
{
    public partial class ActualisationContext : DbContext
    {
        public ActualisationContext()
        {
        }

        public ActualisationContext(DbContextOptions<ActualisationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Competences> Competences { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(" Server=localhost;Database=Actualisation ;User Id=sa;Password=sql");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Competences>(entity =>
            {
                entity.HasKey(e => e.CodeCompetence);

                entity.Property(e => e.CodeCompetence)
                    .HasMaxLength(15)
                    .ValueGeneratedNever();

                entity.Property(e => e.ContextRealisation).HasColumnType("text");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.NoProgramme).HasMaxLength(15);
            });
        }
    }
}
