using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebUygulamaProje.Models
{
	public class Hire
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int CustomerId { get; set; }

		[ValidateNever]
		public int BookId { get; set; }
		[ForeignKey("BookId")]

		[ValidateNever]
		public Book Book { get; set; }
	}
}
