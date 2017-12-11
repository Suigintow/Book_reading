using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace BookReading.Models
{
	public class BookContext : IBookContext
    {

        private static readonly BookContext _instance = new BookContext();

        public static IBookContext Instance
        {
            get { return _instance; }
        }

        private readonly List<Book> _books = new List<Book>();

	    public Book GetBook(int bookId)
	    {
		    var book = _books.SingleOrDefault(x => x.Id == bookId);
		    if (book == null)
			    return null;

			// clear to avoid duplicates, because we hold this book in _books
		    book.Reviews.Clear();

			book.Reviews.AddRange(ReviewContext.Instance.GetAll().Where(x => x.BookId == bookId));
		    return book;
	    }

	    public Book Update(Book newBookData)
	    {
		    if (newBookData == null) throw new ArgumentNullException();
		    var book = _books.Single(x => x.Id == newBookData.Id);

			book.Update(newBookData);
			return book;
	    }

        public List<Book> GetAll()
	    {
	        return _books;
	    }

		public void AddBook(Book newBook)
		{
			_bookId++;
			newBook.Id = _bookId;
			_books.Add(newBook);
		}

		public void AddReview(Review newReview)
		{
			_reviewId++;
			newReview.Id = _reviewId;
		    ReviewContext.Instance.GetAll().Add(newReview);
		}

        public List<Book> GetRange(int from, int to)
        {
            return _books.Skip(from).Take(to - from).ToList();
        }

        public int Count()
        {
            return _books.Count;
        }

        private static int _bookId;

		private static int _reviewId;

		private BookContext()
		{
			Book book = new Book()
			{
				Author = "King",
				Description = "It is scary story",
				Genre = "horror",
				Id = 0,
				Pages = 200,
				Title = "It"
			};			
			
			Book book1 = new Book()
			{
				Author = "King1",
				Description = "It is scary story2",
				Genre = "horror3",
				Id = 1,
				Pages = 800,
				Title = "It2"
			};

			_books.AddRange(new List<Book>(){book,book1});

			Review review = new Review()
			{
				Id = 1,
				Author = "Some",
				BookId = 0,
				Text = "Good choice!"
			};

		    ReviewContext.Instance.GetAll().Add(review);
			_bookId = 1;
			_reviewId = 1;
		}
	}
}