using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using NLogSql.Domain;

namespace NLogSql.Data.Mapping
{
    public class PlaylistMap : EntityTypeConfiguration<Playlist>
    {
        public PlaylistMap()
        {
            // Primary Key
            this.HasKey(t => t.PlaylistId);

            // Properties
            this.Property(t => t.PlaylistId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .HasMaxLength(120);

            // Table & Column Mappings
            this.ToTable("Playlist");
            this.Property(t => t.PlaylistId).HasColumnName("PlaylistId");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}
