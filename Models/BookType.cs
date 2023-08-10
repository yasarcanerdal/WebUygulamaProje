using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebUygulamaProje.Models
{
	public class BookType
	{
		[Key]
		public int Id { get; set; }


		[Required(ErrorMessage = "Kitap Türü Adı Boş Bırakılamaz.")]
		[MaxLength(30)]
		[DisplayName("Kitap Türü Adını Giriniz.")]
		public string Name { get; set; }	
	}
}
