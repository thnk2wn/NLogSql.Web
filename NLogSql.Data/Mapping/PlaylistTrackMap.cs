using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using NLogSql.Domain;

namespace NLogSql.Data.Mapping
{
    public class PlaylistTrackMap : EntityTypeConfiguration<PlaylistTrack>
    {
        public PlaylistTrackMap()
        {
            // Primary Key
            this.HasKey(t => new { t.PlaylistId, t.TrackId });

            // Properties
            this.Property(t => t.PlaylistId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.TrackId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("PlaylistTrack");
            this.Property(t => t.PlaylistId).HasColumnName("PlaylistId");
            this.Property(t => t.TrackId).HasColumnName("TrackId");
        }
    }
}
