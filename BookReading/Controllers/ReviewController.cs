using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookReading.Models;

namespace BookReading.Controllers
{
    public class ReviewController : Controller
    {
        private IBookContext _bookContext;
        private IReviewContext _reviewContext;

        public ReviewController() : this(new DbBookContext(), new DbReviewContext())
        {
        }

        public ReviewController(IBookContext bookContext, IReviewContext reviewContext)
        {
            if (bookContext == null)
                throw new ArgumentNullException();
            if (reviewContext == null)
                throw new ArgumentNullException();
            _bookContext = bookContext;
            _reviewContext = reviewContext;
        }
        //
        // GET: /Review/

        public ActionResult Create(int bookId)
        {
	        if (_bookContext.GetBook(bookId) == null)
		        return HttpNotFound();

            var review = new Review(){BookId = bookId};
            
			return View(review);
        }

		[HttpPost]
	    public ActionResult Create(Review review)
	    {
		    if (ModelState.IsValid)
		    {
		        _bookContext.AddReview(review);
		    }
		    else
		    {
			    ModelState.AddModelError("Create", "Something wrong!");
		    }
		    return RedirectToAction("Details", "Book", new { id = review.BookId });
	    }

        [HttpPost]
        public int IncrementAndGetLikes(int reviewId)
        {
            var likesCount = _reviewContext.IncrementAndGetLikes(reviewId);
            if (likesCount == -1)
            {
                Response.StatusCode = 404;
                return -1;
            }

            return likesCount;
        }

        [HttpPost]
        public void ReportOffensiveReview(int reviewId, string reason)
        {
            _reviewContext.Report(reviewId, reason);
        }
    }
}
