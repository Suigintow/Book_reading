using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinqToDB;
using LinqToDB.Data;

namespace BookReading.Models
{
    public class DbReviewContext : IReviewContext
    {
        public int IncrementAndGetLikes(int reviewId)
        {
            using (var db = new Database())
            {
                var review =
                    db.Reviews.SingleOrDefault(x => x.Id == reviewId);

                if (review == null)
                    return -1;

                review.LikeCount += 1;

                db.Update(review);
                return review.LikeCount;
            }
        }

        public List<Review> GetAll()
        {
            using (var db = new Database())
            {
                var query = (from r in db.Reviews
                    select r);
                return query.ToList();
            }
        }

        public void Report(int reviewId, string reason)
        {
            using (var db = new Database())
            {
                var review =
                    db.Reviews.SingleOrDefault(x => x.Id == reviewId);

                if (review == null)
                    return;

                review.ReportReason = reason;

                db.Update(review);
                return;
            }
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