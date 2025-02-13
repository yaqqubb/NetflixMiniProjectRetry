using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleNetflix
{
    public class Movie : BaseModel
    {
        public Movie(string title, Genre genre, int viewCount, string description, decimal runTime)
        {
            Title = title;
            Genre = genre;
            ViewCount = viewCount;
            Description = description;
            RunTime = runTime;
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public Genre Genre { get; set; }
        public int ViewCount { get; set; }
        public decimal RunTime { get; set; }
    }
}
