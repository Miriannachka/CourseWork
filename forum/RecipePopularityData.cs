using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forum.Model
{
    public class RecipePopularityData
    {
        public int RecipeID { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public int TotalRatings { get; set; }
        public double AverageRating { get; set; }
        public int TotalComments { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
