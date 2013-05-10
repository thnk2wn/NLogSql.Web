using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using NLogSql.Domain;

namespace NLogSql.Data.Mapping
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            // Primary Key
            this.HasKey(t => t.CustomerId);

            // Properties
            this.Property(t => t.CustomerId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Company)
                .HasMaxLength(80);

            this.Property(t => t.Address)
                .HasMaxLength(70);

            this.Property(t => t.City)
                .HasMaxLength(40);

            this.Property(t => t.State)
                .HasMaxLength(40);

            this.Property(t => t.Country)
                .HasMaxLength(40);

            this.Property(t => t.PostalCode)
                .HasMaxLength(10);

            this.Property(t => t.Phone)
                .HasMaxLength(24);

            this.Property(t => t.Fax)
                .HasMaxLength(24);

            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("Customer");
            this.Property(t => t.CustomerId).HasColumnName("CustomerId");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.Company).HasColumnName("Company");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.State).HasColumnName("State");
            this.Property(t => t.Country).HasColumnName("Country");
            this.Property(t => t.PostalCode).HasColumnName("PostalCode");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Fax).HasColumnName("Fax");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.SupportRepId).HasColumnName("SupportRepId");
        }
    }
}
