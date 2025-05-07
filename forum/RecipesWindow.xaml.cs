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
using System.Data.Entity.Migrations.Model;

namespace forum
{
    public partial class RecipesWindow : Window
    {
        public RecipesWindow(Users user)
        {
            InitializeComponent();

            // Получаем экземпляр приложения
            App app = (App)Application.Current;

            MessageBox.Show($"Добро пожаловать, {app.CurrentUser.FirstName}!");

            //Подписываемся на событие выбора категории в CategoriesSidebar
            CategoriesSidebar.CategoriesSelected += CategoriesSidebar_CategorySelected;

            //Передаем CurrentUser в RecipesView
            RecipesView.CurrentUser = app.CurrentUser; //Исправлено

            //Проверка роли и отображение элементов управления
            if (app.CurrentUser.RoleID == (int)UserRole.Administrator) //Исправлено
            {
                AddRecipeButton.Visibility = Visibility.Visible; //Кнопка добавления рецепта
            }
            else
            {
                AddRecipeButton.Visibility = Visibility.Collapsed; //Скрываем кнопку
            }
        }

        private void CategoriesSidebar_CategorySelected(object sender, string categoryName)
        {
            RecipesView.LoadRecipes(categoryName);
        }

        private void AddRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            // Открываем окно добавления рецепта
            AddRecipeWindow addRecipeWindow = new AddRecipeWindow();
            addRecipeWindow.Show();
        }

        private void DeleteRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранный рецепт
            if (RecipesListView.SelectedItem is Recipes selectedRecipe)
            {
                // Подтверждаем удаление
                MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить рецепт '{selectedRecipe.Title}'?",
                                                        "Подтверждение удаления",
                                                        MessageBoxButton.YesNo,
                                                        MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Удаляем рецепт из базы данных
                    using (var db = new Forum_with_cooking_recipesContext())
                    {
                        // Находим рецепт в базе данных
                        Recipes recipeToDelete = db.Recipes.Find(selectedRecipe.RecipeID);

                        if (recipeToDelete != null)
                        {
                            // Удаляем рецепт
                            db.Recipes.Remove(recipeToDelete);

                            // Сохраняем изменения
                            db.SaveChanges();

                            // Обновляем UI (например, перезагружаем список рецептов)
                            LoadRecipes(); // Замените на ваш метод загрузки рецептов
                        }
                        else
                        {
                            MessageBox.Show("Рецепт не найден.");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите рецепт для удаления.");
            }
        }

        private void LoadRecipes()
        {
            using (var db = new Forum_with_cooking_recipesContext())
            {
                // Загружаем рецепты из базы данных
                var recipes = db.Recipes
                    .Include("Categories")
                    .Include("Users")
                    .ToList();

                // Устанавливаем список рецептов в качестве ItemsSource для RecipesListView
                RecipesListView.ItemsSource = recipes;
            }
        }

        private void ShowAdminWindow_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, является ли текущий пользователь администратором
            App app = (App)Application.Current;
            if (app.CurrentUser.RoleID == (int)UserRole.Administrator)
            {
                AdminWindow adminWindow = new AdminWindow();
                adminWindow.Show();
            }
            else
            {
                MessageBox.Show("У вас нет прав для доступа к окну администрирования.");
            }
        }
    }
}