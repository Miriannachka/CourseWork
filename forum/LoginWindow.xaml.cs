using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, введите имя пользователя и пароль");
                return;
            }

            using (var db = new Forum_with_cooking_recipesContext())
            {
                // 1. Сначала получаем пользователя по имени

                if (db.Users.FirstOrDefault(u => u.Username == username) == null)
                {
                    MessageBox.Show("Неверное имя пользователя или пароль");
                    return;
                }

                // 2. Проверяем пароль
                string passwordHash = GetHash(password);
                if (db.Users.FirstOrDefault(u => u.Username == username).PasswordHash != passwordHash)
                {
                    MessageBox.Show("Неверное имя пользователя или пароль");
                    return;
                }

                // 3. Авторизация прошла успешно
                MessageBox.Show("Авторизация прошла успешно!");

                //Получаем экземпляр приложения
                App app = (App)Application.Current;

                // Устанавливаем CurrentUser
                app.CurrentUser = db.Users.FirstOrDefault(u => u.Username == username);  // где user - это объект User после успешной авторизации

                // Открываем RecipesWindow
                // Передаём user
                new RecipesWindow(db.Users.FirstOrDefault(u => u.Username == username)).Show();
                this.Close();
            }
        }

        // Функция хеширования пароля (SHA256)
        public static string GetHash(string password)
        {
            using (var hash = SHA256.Create())
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
