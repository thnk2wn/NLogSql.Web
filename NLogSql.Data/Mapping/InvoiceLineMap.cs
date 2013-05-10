using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using NLogSql.Domain;

namespace NLogSql.Data.Mapping
{
    public class InvoiceLineMap : EntityTypeConfiguration<InvoiceLine>
    {
        public InvoiceLineMap()
        {
            // Primary Key
            this.HasKey(t => t.InvoiceLineId);

            // Properties
            this.Property(t => t.InvoiceLineId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("InvoiceLine");
            this.Property(t => t.InvoiceLineId).HasColumnName("InvoiceLineId");
            this.Property(t => t.InvoiceId).HasColumnName("InvoiceId");
            this.Property(t => t.TrackId).HasColumnName("TrackId");
            this.Property(t => t.UnitPrice).HasColumnName("UnitPrice");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
        }
    }
}
