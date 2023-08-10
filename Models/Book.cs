using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebUygulamaProje.Models
{
	public class Book
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[DisplayName("Kitap Adı")]
		public string BookName { get; set; }

		[DisplayName("Kitap Hakkında Açıklama")]
		public string Description { get; set; }

		[Required]
		[DisplayName("Yazar Adı")]
		public string Writer { get; set; }

		[Required]
		[DisplayName("Kitap Fiyatı")]
		[Range(20,10000)]
		public double Price { get; set; }

		// Foreign Key
		[ValidateNever]
		public int BookTypeId { get; set; }
		[ForeignKey("BookTypeId")]

		[ValidateNever]
		public BookType BookType { get; set; }

		[ValidateNever]
		public string İmageUrl { get; set; }
	}
}
