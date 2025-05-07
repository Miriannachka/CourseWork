using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace forum
{
    public partial class RecipeDetailWindow : Window
    {
        private int _recipeId;

        public RecipeDetailWindow(int recipeId)
        {
            InitializeComponent();
            _recipeId = recipeId;
            LoadRecipe();
        }

        private void LoadRecipe()
        {
            using (var db = new Forum_with_cooking_recipesContext())
            {
                var recipe = db.Recipes.FirstOrDefault(r => r.RecipeID == _recipeId);

                if (recipe != null)
                {
                    // Устанавливаем DataContext на один объект Recipe
                    DataContext = recipe;
                }
                else
                {
                    MessageBox.Show("Рецепт не найден.");
                    Close();
                }
            }
        }
    }
}
