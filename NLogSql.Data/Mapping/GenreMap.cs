using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using NLogSql.Domain;

namespace NLogSql.Data.Mapping
{
    public class GenreMap : EntityTypeConfiguration<Genre>
    {
        public GenreMap()
        {
            // Primary Key
            this.HasKey(t => t.GenreId);

            // Properties
            this.Property(t => t.GenreId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .HasMaxLength(120);

            // Table & Column Mappings
            this.ToTable("Genre");
            this.Property(t => t.GenreId).HasColumnName("GenreId");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}
