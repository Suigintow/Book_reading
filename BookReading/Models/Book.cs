using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Razor;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;

namespace BookReading.Models
{
    [LinqToDB.Mapping.Table]
	public class Book
    { 

        [PrimaryKey, Identity]
	    public int Id { get; set; }

		[Display(Name = "Название"), LinqToDB.Mapping.Column]
		public string Title { get; set; }

        [LinqToDB.Mapping.Column]
		public string Description { get; set; }

	    [LinqToDB.Mapping.Column]
        public string Author { get; set; }

	    [LinqToDB.Mapping.Column]
        public int Pages { get; set; }

	    [LinqToDB.Mapping.Column]
        public string Genre { get; set; }

		public void Update(Book newBookData)
		{
			Title = newBookData.Title;
			Description = newBookData.Description;
			Author = newBookData.Author;
			Pages = newBookData.Pages;
			Genre = newBookData.Genre;
		}

        public List<Review> Reviews
        {
	        get {
                using (var db = new Database())
                    return (from r in db.Reviews
                    where r.BookId == Id && r.ReportReason == null 
                    select r).Take(10).ToList();
            }
	        set { new NotImplementedException(); }
        }

	    private class Database : DataConnection
	    {
	        public Database() : base("Main")
	        {

	        }

	        public ITable<Review> Reviews { get { return GetTable<Review>(); } }
	    }
    }
}