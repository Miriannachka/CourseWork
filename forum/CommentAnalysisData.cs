using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forum.Models
{
    public class CommentAnalysisData
    {
        public int CommentID { get; set; }
        public int RecipeID { get; set; }
        public int UserID { get; set; }
        public string Username { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }
    }
}
