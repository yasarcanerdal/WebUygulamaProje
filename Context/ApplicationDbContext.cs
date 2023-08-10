using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebUygulamaProje.Models;

// Veritabanında tablo oluşturulması için ilgili model sınıflarımızı buraya eklemeliyiz.

namespace WebUygulamaProje.Context
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options) { }

		public DbSet<BookType> BookTypes { get; set; }	
		public DbSet<Book> Books { get; set; }
		public DbSet<Hire> Leases { get; set; }

		public DbSet<ApplicationUser> ApplicationUsers { get; set; }

	}
}
