using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using NLogSql.Domain;

namespace NLogSql.Data.Mapping
{
    public class AlbumMap : EntityTypeConfiguration<Album>
    {
        public AlbumMap()
        {
            // Primary Key
            this.HasKey(t => t.AlbumId);

            // Properties
            this.Property(t => t.AlbumId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(160);

            // Table & Column Mappings
            this.ToTable("Album");
            this.Property(t => t.AlbumId).HasColumnName("AlbumId");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.ArtistId).HasColumnName("ArtistId");
        }
    }
}
