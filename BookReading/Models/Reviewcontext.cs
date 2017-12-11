using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookReading.Models
{
    public class ReviewContext : IReviewContext
    {
        private static readonly ReviewContext _instance = new ReviewContext();

        public static IReviewContext Instance
        {
            get { return _instance; }
        }

        private readonly List<Review> _reviews = new List<Review>();

        public List<Review> GetAll()
        {
            return _reviews;
        }

        public void Report(int reviewId, string reason)
        {
            var review = _reviews.FirstOrDefault(x => x.Id == reviewId);
            if (review == null)
            {
                return;
            }

            review.ReportReason = reason;
        }

        public int IncrementAndGetLikes(int reviewId)
        {
            var review = _reviews.FirstOrDefault(x => x.Id == reviewId);
            if (review == null)
            {
                return -1;
            }
            
            return ++review.LikeCount;
        }
    }
}