using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using NLogSql.Domain;

namespace NLogSql.Data.Mapping
{
    public class EmployeeMap : EntityTypeConfiguration<Employee>
    {
        public EmployeeMap()
        {
            // Primary Key
            this.HasKey(t => t.EmployeeId);

            // Properties
            this.Property(t => t.EmployeeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Title)
                .HasMaxLength(30);

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
                .HasMaxLength(60);

            // Table & Column Mappings
            this.ToTable("Employee");
            this.Property(t => t.EmployeeId).HasColumnName("EmployeeId");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.ReportsTo).HasColumnName("ReportsTo");
            this.Property(t => t.BirthDate).HasColumnName("BirthDate");
            this.Property(t => t.HireDate).HasColumnName("HireDate");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.State).HasColumnName("State");
            this.Property(t => t.Country).HasColumnName("Country");
            this.Property(t => t.PostalCode).HasColumnName("PostalCode");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Fax).HasColumnName("Fax");
            this.Property(t => t.Email).HasColumnName("Email");
        }
    }
}
