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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace forum
{
    public partial class RecipesView : UserControl
    {
        public RecipesView()
        {
            InitializeComponent();
            LoadRecipes("Все категории");
        }

        public static readonly DependencyProperty CurrentUserProperty =
        DependencyProperty.Register("CurrentUser", typeof(Users), typeof(RecipesView), new PropertyMetadata(null));

        public Users CurrentUser
        {
            get { return (Users)GetValue(CurrentUserProperty); }
            set { SetValue(CurrentUserProperty, value); }
        }

        public void LoadRecipes(string categoryName)
        {
            RecipesListBox.Items.Clear();

            using (var db = new Forum_with_cooking_recipesContext())
            {
                IQueryable<Recipes> recipesQuery = db.Recipes
                    .Include("Categories")
                    .Include("Users");

                if (categoryName != "Все категории")
                {
                    recipesQuery = recipesQuery.Where(r => r.Categories.CategoryName == categoryName);
                }

                List<Recipes> recipes = recipesQuery.ToList();

                foreach (Recipes recipe in recipes)
                {
                    ListBoxItem item = new ListBoxItem();
                    item.Content = recipe.Title;
                    RecipesListBox.Items.Add(item);
                }
            }
        }

        private void RecipesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RecipesListBox.SelectedItem is ListBoxItem selectedItem)
            {
                string recipeTitle = selectedItem.Content.ToString();
                using (var db = new Forum_with_cooking_recipesContext())
                {
                    Recipes selectedRecipe = db.Recipes
                        .Include("Categories") 
                        .Include("Users")
                        .FirstOrDefault(r => r.Title == recipeTitle);

                    if (selectedRecipe != null)
                    {
                        // Объявление и присвоение recipeId
                        int recipeId = selectedRecipe.RecipeID;

                        RecipeDetailWindow detailWindow = new RecipeDetailWindow(recipeId);
                        detailWindow.Show();
                    }
                    else
                    {
                        MessageBox.Show("Рецепт не найден.");
                    }
                }
            }
        }
    }
}
