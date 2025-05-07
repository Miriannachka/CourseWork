using forum.Model;
using forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace forum
{
    public class ReportGenerator
    {
        public static List<RecipePopularityData> GetRecipePopularityData()
        {
            using (var db = new Forum_with_cooking_recipesContext())
            {
                var data = db.Recipes.Select(r => new RecipePopularityData
                {
                    RecipeID = r.RecipeID,
                    Title = r.Title,
                    CategoryName = r.Categories.CategoryName,
                    TotalRatings = r.RecipeRatings.Count(),
                    AverageRating = r.RecipeRatings.Any() ? r.RecipeRatings.Average(rr => rr.RatingValue) : 0,
                    TotalComments = r.Comments.Count(),
                    PublicationDate = r.PublicationDate ?? DateTime.MinValue
                }).ToList();

                return data;
            }
        }

        public static List<UserActivityData> GetUserActivityData()
        {
            using (var db = new Forum_with_cooking_recipesContext())
            {
                // 1. Сначала получаем список пользователей без LastLogin
                var users = db.Users.Select(u => new UserActivityData
                {
                    UserID = u.UserID,
                    Username = u.Username,
                    RoleName = u.Roles.RoleName,  // Предполагается, что у вас есть навигационное свойство Role
                    TotalRecipes = u.Recipes.Count(),
                    TotalComments = u.Comments.Count(),
                    RegistrationDate = u.RegistrationDate ?? DateTime.MinValue
                }).ToList();

                // 2. Затем заполняем LastLogin для каждого пользователя
                foreach (var user in users)
                {
                    user.LastLogin = db.Users.FirstOrDefault(u => u.UserID == user.UserID)?.LastLogin;
                }

                return users;
            }
        }

        public static List<CategoryRecipeData> GetCategoryRecipeData()
        {
            using (var db = new Forum_with_cooking_recipesContext())
            {
                var data = db.Categories.Select(c => new CategoryRecipeData
                {
                    CategoryID = c.CategoryID,
                    CategoryName = c.CategoryName,
                    TotalRecipes = c.Recipes.Count(),
                    AverageRating = c.Recipes.Any() ? c.Recipes.Average(r => (double?)r.RecipeRatings.Average(rr => rr.RatingValue)) ?? 0 : 0, // Double? для корректной обработки null
                    MostPopularRecipe = c.Recipes.OrderByDescending(r => r.RecipeRatings.Average(rr => rr.RatingValue)).FirstOrDefault().Title ?? "Нет рецептов"
                }).ToList();

                return data;
            }
        }

        public static List<CommentAnalysisData> GetCommentAnalysisData()
        {
            using (var db = new Forum_with_cooking_recipesContext())
            {
                var data = db.Comments.Select(c => new CommentAnalysisData
                {
                    CommentID = c.CommentID,
                    RecipeID = c.RecipeID,
                    UserID = c.UserID,
                    Username = c.Users.Username,
                    CommentText = c.CommentText,
                    CommentDate = c.CommentDate ?? DateTime.MinValue
                }).ToList();

                return data;
            }
        }
    }
}
