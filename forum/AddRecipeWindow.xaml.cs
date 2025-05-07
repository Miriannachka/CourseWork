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
using Microsoft.Win32;

namespace forum
{
    public partial class AddRecipeWindow : Window
    {
        public AddRecipeWindow()
        {
            InitializeComponent();
            LoadCategories();
        }

        private void LoadCategories()
        {
            using (var db = new Forum_with_cooking_recipesContext())
            {
                // Загружаем категории из базы данных
                var categories = db.Categories.ToList();

                // Устанавливаем категории в ComboBox
                CategoryComboBox.ItemsSource = categories;
                CategoryComboBox.DisplayMemberPath = "CategoryName";
                CategoryComboBox.SelectedValuePath = "CategoryID";
            }
        }

        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
        {
            //Не нужно открывать диалоговое окно выбора файла
            //Просто предоставляем возможность ввода URL вручную
        }

        private void SaveRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new Forum_with_cooking_recipesContext())
            {
                // Проверяем, выбрана ли категория
                if (CategoryComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, выберите категорию.");
                    return;
                }

                // Получаем ID выбранной категории
                int selectedCategoryId = (int)CategoryComboBox.SelectedValue;

                // Получаем ID текущего пользователя (замените на ваш способ получения UserID)
                int currentUserId = 1; //SessionManager.CurrentUser.UserID; // Замените на ваш код

                Recipes newRecipe = new Recipes
                {
                    Title = TitleTextBox.Text,
                    Ingredients = IngredientsTextBox.Text,
                    Instructions = InstructionsTextBox.Text,
                    ImagePath = ImagePathTextBox.Text,
                    CategoryID = selectedCategoryId,
                    UserID = currentUserId // Устанавливаем UserID текущего пользователя
                };

                db.Recipes.Add(newRecipe);
                db.SaveChanges();

                MessageBox.Show("Рецепт успешно добавлен!");
                this.Close();
            }
        }

        private void CategoryComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //Обработчик события, возникающего при выборе категории в ComboBox
            //Здесь можно добавить логику, которая должна выполняться при изменении выбранной категории
        }
    }
}
