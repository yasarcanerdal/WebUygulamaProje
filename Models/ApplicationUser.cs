using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebUygulamaProje.Models
{
	public class ApplicationUser : IdentityUser
	{
		[Required]
		public int CustomerNo { get; set; }
		public string Address { get; set; }
		public string? Faculty { get; set; }
		public string? Section { get; set; }

	}
}
