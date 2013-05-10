using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using NLogSql.Domain;

namespace NLogSql.Data.Mapping
{
    public class ArtistMap : EntityTypeConfiguration<Artist>
    {
        public ArtistMap()
        {
            // Primary Key
            this.HasKey(t => t.ArtistId);

            // Properties
            this.Property(t => t.ArtistId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .HasMaxLength(120);

            // Table & Column Mappings
            this.ToTable("Artist");
            this.Property(t => t.ArtistId).HasColumnName("ArtistId");
            this.Property(t => t.Name).HasColumnName("Name");

            HasMany(x => x.Albums);
        }
    }
}
