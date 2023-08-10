using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebUygulamaProje.Context;
using WebUygulamaProje.Models;

namespace WebUygulamaProje.Controllers
{
	public class BookController : Controller
	{
		private readonly IBookRepository _bookRepository;
		private readonly IBookTypeRepository _bookTypeRepository;
		public readonly IWebHostEnvironment _webHostEnvironment;

		public BookController(IBookRepository context, IBookTypeRepository bookTypeRepository, IWebHostEnvironment webHostEnvironment)
		{
			_bookRepository = context;
			_bookTypeRepository = bookTypeRepository;
			_webHostEnvironment = webHostEnvironment;
		}

        [Authorize(Roles = "Admin,Customer")]
        public IActionResult Index()
		{

			//List<Book> objBookList = _bookRepository.GetAll().ToList();
			List<Book> objBookList = _bookRepository.GetAll(includeProps:"BookType").ToList();
			return View(objBookList);
		}


        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult AddUpdate(int? id)
		{
			IEnumerable<SelectListItem> BookTypeList = _bookTypeRepository.GetAll()
				.Select(x => new SelectListItem
				{
					Text = x.Name,
					Value = x.Id.ToString(),
				});
			ViewBag.BookTypeList = BookTypeList;

			if (id == null || id==0)
			{
				return View();
			}
			else
			{
				// Güncelleme
				Book? bookVt = _bookRepository.Get(x => x.Id == id);
				if (bookVt == null)
				{
					return NotFound();
				}
				return View(bookVt);
			}

			
		}
        [Authorize(Roles = UserRoles.Role_Admin)]
        [HttpPost]
		public IActionResult AddUpdate(Book book, IFormFile? file)
		{
			if (ModelState.IsValid)
			{
				string wwwRootPath = _webHostEnvironment.WebRootPath;
				string bookPath = Path.Combine(wwwRootPath, @"img");


				if (file != null)
				{
					using (var fileStream = new FileStream(Path.Combine(bookPath, file.FileName), FileMode.Create))
					{
						file.CopyTo(fileStream);
					}
					book.İmageUrl = @"\img\" + file.FileName;
				}

				if (book.Id == 0)
				{
					_bookRepository.Add(book);
					TempData["basarili"] = "Yeni Kitap Oluşturuldu.";
				}
				else
				{
					_bookRepository.Update(book);
					TempData["basarili"] = "Kitap Güncellendi.";
				}
				_bookRepository.Save(); // Bilgiler veritabanına eklenir.
				return RedirectToAction("Index");
			}
			return View();
		}

        //public IActionResult Update(int? id)
        //{
        //	if(id == null || id== 0)
        //	{
        //		return NotFound();
        //	}
        //	Book? bookVt = _bookRepository.Get(x => x.Id == id);
        //	if(bookVt == null)
        //	{
        //		return NotFound();
        //	}
        //	return View(bookVt);
        //}

        //[HttpPost]
        //public IActionResult Update(Book book)
        //{
        //	if (ModelState.IsValid)
        //	{
        //		_bookRepository.Update(book);
        //		_bookRepository.Save(); // Bilgiler veritabanına eklenir.
        //		TempData["basarili"] = "Kitap Güncellendi.";
        //		return RedirectToAction("Index");
        //	}
        //	return View();
        //}

        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Book? bookVt = _bookRepository.Get(x =>x.Id == id);
			if (bookVt == null)
			{
				return NotFound();
			}
			return View(bookVt);
		}

		[HttpPost, ActionName("Delete")]
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult DeletePost(int? id)
		{
			Book? book = _bookRepository.Get(x => x.Id == id);
			if ( book == null)
			{
				return NotFound();
			}
			_bookRepository.Delete(book);
			_bookRepository.Save();
			TempData["basarili"] = "Kitap Silindi.";
			return RedirectToAction("Index");
		}
	}
}
