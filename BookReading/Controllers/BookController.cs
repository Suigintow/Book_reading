using System;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web.Mvc;
using BookReading.Models;

namespace BookReading.Controllers
{
    public class BookController : Controller
    {
        private const int _PAGE_SIZE = 10;
        private IBookContext _bookContext;

        // public BookController() : this(BookContext.Instance)
        public BookController() : this(new DbBookContext())
        {
        }

        public BookController(IBookContext bookContext)
        {
            if (bookContext == null)
                throw new ArgumentNullException();
            _bookContext = bookContext;
        }

        //
        // GET: /Book/

        public ActionResult List(int currentPage = 1)
        {
            if (currentPage < 1)
            {
                currentPage = 1;
            }
            var paginatorNum = currentPage - 2;
            if (paginatorNum < 1)
            {
                paginatorNum = 1;
            }
            var indexListView = new IndexListView()
            {
                books = _bookContext.GetRange((currentPage - 1) * _PAGE_SIZE, _PAGE_SIZE),
                currentPage = paginatorNum,
                totalPage = (int)(_bookContext.Count()/_PAGE_SIZE + 0.5)
            };
            return View(indexListView);
        }    
		
		public ActionResult Details(int id = 0)
		{
			ViewBag.Title = "Подробнее о книге";
			var book = _bookContext.GetBook(id);
			
			if (book == null)
				return HttpNotFound();

            return View(book);
        }
		
	    public ActionResult Edit(int id = 0)
	    {
			var book = _bookContext.GetBook(id);

		    if (book == null)
			    return HttpNotFound();

		    return View(book);
	    }

		[HttpPost]
	    public ActionResult Edit(Book editBook)
		{
			Book first = _bookContext.GetBook(editBook.Id);

            if (first == null)
				return HttpNotFound();
			
			_bookContext.Update(editBook);

			return View(editBook);
		}

        public ActionResult CreateBook()
        {
            var book = new Book();
            return View(book);
        }

        [HttpPost]
        public ActionResult CreateBook(Book book)
        {
            if (ModelState.IsValid && !string.IsNullOrEmpty(book.Title))
            {
                _bookContext.AddBook(book);
            }
            else
            {
                ModelState.AddModelError("Create", "Something wrong!");
            }
            return RedirectToAction("List", "Book");
        }
    }
}
