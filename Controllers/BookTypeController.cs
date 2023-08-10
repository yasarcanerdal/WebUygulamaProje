using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebUygulamaProje.Context;
using WebUygulamaProje.Models;

namespace WebUygulamaProje.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)] // Kitap İşlemlerine sadece yönetici girer diğer aşamalardan sonra bu kod eklenmeli.
    public class BookTypeController : Controller
	{
		private readonly IBookTypeRepository _bookTypeRepository;

		public BookTypeController(IBookTypeRepository context)
		{
			_bookTypeRepository = context;
		}
		public IActionResult Index()
		{
			List<BookType> objBookTypeList = _bookTypeRepository.GetAll().ToList();
			return View(objBookTypeList);
		}

		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Add(BookType bookType)
		{
			if (ModelState.IsValid)
			{
				_bookTypeRepository.Add(bookType);
				_bookTypeRepository.Save(); // Bilgiler veritabanına eklenir.
				TempData["basarili"] = "Yeni Kitap Türü Oluşturuldu.";
				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult Update(int? id)
		{
			if(id == null || id== 0)
			{
				return NotFound();
			}
			BookType? bookTypeVt = _bookTypeRepository.Get(x => x.Id == id);
			if(bookTypeVt == null)
			{
				return NotFound();
			}
			return View(bookTypeVt);
		}

		[HttpPost]
		public IActionResult Update(BookType bookType)
		{
			if (ModelState.IsValid)
			{
				_bookTypeRepository.Update(bookType);
				_bookTypeRepository.Save(); // Bilgiler veritabanına eklenir.
				TempData["basarili"] = "Kitap Türü Güncellendi.";
				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			BookType? bookTypeVt = _bookTypeRepository.Get(x =>x.Id == id);
			if (bookTypeVt == null)
			{
				return NotFound();
			}
			return View(bookTypeVt);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(int? id)
		{
			BookType? bookType = _bookTypeRepository.Get(x => x.Id == id);
			if ( bookType == null)
			{
				return NotFound();
			}
			_bookTypeRepository.Delete(bookType);
			_bookTypeRepository.Save();
			TempData["basarili"] = "Kitap Türü Silindi.";
			return RedirectToAction("Index");
		}
	}
}
