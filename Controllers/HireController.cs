using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using WebUygulamaProje.Context;
using WebUygulamaProje.Models;

namespace WebUygulamaProje.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)] // Kitap İşlemlerine sadece yönetici girer diğer aşamalardan sonra bu kod eklenmeli.
    public class HireController : Controller
	{
		private readonly IHireRepository _hireRepository;
		private readonly IBookRepository _bookRepository;
		public readonly IWebHostEnvironment _webHostEnvironment;

		public HireController(IHireRepository context, IBookRepository bookRepository, IWebHostEnvironment webHostEnvironment)
		{
			_hireRepository = context;
			_bookRepository = bookRepository;
			_webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Index()
		{
			List<Hire> objHireList = _hireRepository.GetAll(includeProps:"Book").ToList();
			return View(objHireList);
		}

		[HttpGet]
		public IActionResult AddUpdate(int? id)
		{
			IEnumerable<SelectListItem> BookList = _bookRepository.GetAll()
				.Select(x => new SelectListItem
				{
					Text = x.BookName,
					Value = x.Id.ToString(),
				});
			ViewBag.BookList = BookList;

			if (id == null || id==0)
			{
				return View();
			}
			else
			{
				// Güncelleme
				Hire? hireVt = _hireRepository.Get(x => x.Id == id);
				if (hireVt == null)
				{
					return NotFound();
				}
				return View(hireVt);
			}

			
		}

		[HttpPost]
		public IActionResult AddUpdate(Hire hire)
		{
			if (ModelState.IsValid)
			{

				if (hire.Id == 0)
				{
					_hireRepository.Add(hire);
					TempData["basarili"] = "Yeni Kiralama İşlemi Oluşturuldu.";
				}
				else
				{
					_hireRepository.Update(hire);
					TempData["basarili"] = "Kiralama Kayıt Güncelleme Başarılı";
				}
				_hireRepository.Save(); // Bilgiler veritabanına eklenir.
				return RedirectToAction("Index");
			}
			return View();
		}

		// Get Actıon
		public IActionResult Delete(int? id)
		{

			IEnumerable<SelectListItem> BookList = _bookRepository.GetAll()
				.Select(x => new SelectListItem
				{
					Text = x.BookName,
					Value = x.Id.ToString(),
				});
			ViewBag.BookList = BookList;

			if (id == null || id == 0)
			{
				return NotFound();
			}
			Hire? hireVt = _hireRepository.Get(x =>x.Id == id);
			if (hireVt == null)
			{
				return NotFound();
			}
			return View(hireVt);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(int? id)
		{
			Hire? hire = _hireRepository.Get(x => x.Id == id);
			if ( hire == null)
			{
				return NotFound();
			}
			_hireRepository.Delete(hire);
			_hireRepository.Save();
			TempData["basarili"] = "Kitap Silindi.";
			return RedirectToAction("Index");
		}
	}
}
