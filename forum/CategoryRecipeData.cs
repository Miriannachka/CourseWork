using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forum.Models
{
    public class CategoryRecipeData
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int TotalRecipes { get; set; }
        public double AverageRating { get; set; }
        public string MostPopularRecipe { get; set; }
    }
}