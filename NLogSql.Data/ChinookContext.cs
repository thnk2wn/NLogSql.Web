using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using NLogSql.Domain;

namespace NLogSql.Data
{
	public class ChinookContext : DbContext
	{
		static ChinookContext()
		{
			Database.SetInitializer<ChinookContext>(null);
		}

		public ChinookContext()
			: base("Name=ChinookContext")
		{
			TurnOffLazyLoading();
		}

		public DbSet<Album> Albums { get; set; }
		public DbSet<Artist> Artists { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Employee> Employees { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Invoice> Invoices { get; set; }
		public DbSet<InvoiceLine> InvoiceLines { get; set; }
		public DbSet<MediaType> MediaTypes { get; set; }
		public DbSet<Playlist> Playlists { get; set; }
		public DbSet<PlaylistTrack> PlaylistTracks { get; set; }
		public DbSet<Track> Tracks { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			//modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

			var types = Assembly.GetExecutingAssembly().GetTypes().Where(x =>
				x.BaseType != null && x.BaseType.IsGenericType &&
				x.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)).ToList();
			types.ForEach(x =>
			{
				dynamic c = Activator.CreateInstance(x);
				modelBuilder.Configurations.Add(c);
			});
		}

		public void TurnOffLazyLoading()
		{
			this.Configuration.LazyLoadingEnabled = false;
			this.Configuration.ProxyCreationEnabled = false;
		}

		public void TurnOnLazyLoading()
		{
			this.Configuration.LazyLoadingEnabled = true;
			this.Configuration.ProxyCreationEnabled = true;
		}
	}
}
