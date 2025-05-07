using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using forum; 

namespace forum
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
            //Заполняем ComboBox ролями
            using (var db = new Forum_with_cooking_recipesContext())
            {
                var roles = db.Roles.ToList();
                RoleComboBox.ItemsSource = roles;
                RoleComboBox.DisplayMemberPath = "RoleName"; // Отображаемое поле
                RoleComboBox.SelectedValuePath = "RoleID";   // Значение, которое будет выбрано
                RoleComboBox.SelectedIndex = 1; //Выбираем роль по умолчанию
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Валидация полей
            if (string.IsNullOrEmpty(UsernameTextBox.Text) || string.IsNullOrEmpty(EmailTextBox.Text) ||
                string.IsNullOrEmpty(PasswordBox.Password) || string.IsNullOrEmpty(ConfirmPasswordBox.Password) ||
                string.IsNullOrEmpty(FirstNameTextBox.Text) || string.IsNullOrEmpty(LastNameTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля");
                return;
            }

            if (PasswordBox.Password != ConfirmPasswordBox.Password)
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }

            // Используйте правильное имя вашего класса контекста базы данных
            using (var db = new Forum_with_cooking_recipesContext())
            {
                // Проверка, существует ли уже пользователь с таким именем
                var existingUser = db.Users.FirstOrDefault(u => u.Username == UsernameTextBox.Text);
                if (existingUser != null)
                {
                    MessageBox.Show("Пользователь с таким именем уже существует");
                    return;
                }

                // Валидация пароля
                StringBuilder errors = new StringBuilder();
                bool isEnglish = true;
                bool hasNumber = false;

                for (int i = 0; i < PasswordBox.Password.Length; i++)
                {
                    if (PasswordBox.Password[i] >= 'А' && PasswordBox.Password[i] <= 'я')
                    {
                        isEnglish = false;
                    }
                    if (PasswordBox.Password[i] >= '0' && PasswordBox.Password[i] <= '9')
                    {
                        hasNumber = true;
                    }
                }


                if (PasswordBox.Password.Length < 6)
                    errors.AppendLine("Пароль должен быть больше 6 символов");
                if (!isEnglish)
                    errors.AppendLine("Пароль должен быть на английском языке");
                if (!hasNumber)
                    errors.AppendLine("Пароль должен содержать хотя бы одну цифру");
                if (!IsValidEmail(EmailTextBox.Text))
                    errors.AppendLine("Введите корректный email");

                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString());
                    return;
                }

                // Хеширование пароля
                string passwordHash = GetHash(PasswordBox.Password);

                // Определяем RoleID на основе выбора пользователя
                int selectedRole = (RoleComboBox.SelectedItem.ToString() == "Администратор") ? (int)UserRole.Administrator : (int)UserRole.User;

                // Создание нового пользователя
                var newUser = new Users
                {
                    Username = UsernameTextBox.Text,
                    Email = EmailTextBox.Text,
                    PasswordHash = passwordHash,
                    FirstName = FirstNameTextBox.Text,
                    LastName = LastNameTextBox.Text,
                    RegistrationDate = DateTime.Now,
                    RoleID = selectedRole // Роль пользователя по умолчанию (обычный пользователь)

                    // RoleID = selectedRole //Заменить на выбранное значение
                };

                // Добавление пользователя в базу данных
                db.Users.Add(newUser);
                db.SaveChanges();

                MessageBox.Show("Регистрация прошла успешно!");

                // Переход к окну входа
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
        }

        // Функция проверки валидности email
        private bool IsValidEmail(string email)
        {
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
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