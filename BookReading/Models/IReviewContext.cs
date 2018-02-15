using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReading.Models
{
    public interface IReviewContext
    {
        int IncrementAndGetLikes(int reviewId);
        string EditText(int reviewId, string text);

        List<Review> GetAll();
        List<Review> GetTop20(int id);
        void Report(int reviewId, string reason);
    }
}
