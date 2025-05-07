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
using System.Xml.Linq;

namespace forum
{
    public partial class CommentsSection : UserControl
    {
        private int _recipeId;
        public Users CurrentUser { get; set; }

        public CommentsSection(int recipeId, Users currentUser)
        {
            InitializeComponent();
            _recipeId = recipeId;
            CurrentUser = currentUser;
            LoadComments();
            CheckUserRole();
        }

        private void LoadComments()
        {
            using (var db = new Forum_with_cooking_recipesContext())
            {
                var comments = db.Comments
                    .Include("Users")
                    .Where(c => c.RecipeID == _recipeId)
                    .ToList();
                CommentsListBox.ItemsSource = comments;
            }
        }

        private void CheckUserRole()
        {
            if (CurrentUser != null && CurrentUser.RoleID == (int)UserRole.Administrator)
            {
                DeleteCommentButton.Visibility = Visibility.Visible;
            }
            else
            {
                DeleteCommentButton.Visibility = Visibility.Collapsed;
            }
        }

        private void AddCommentButton_Click(object sender, RoutedEventArgs e)
        {
            string commentText = NewCommentTextBox.Text.Trim();

            if (!string.IsNullOrEmpty(commentText))
            {
                using (var db = new Forum_with_cooking_recipesContext())
                {
                    Comments newComment = new Comments
                    {
                        RecipeID = _recipeId,
                        UserID = CurrentUser.UserID,
                        CommentText = commentText,
                        CommentDate = DateTime.Now
                    };

                    db.Comments.Add(newComment);
                    db.SaveChanges();

                    NewCommentTextBox.Clear();
                    LoadComments(); // Обновляем список комментариев
                }
            }
        }

        private void DeleteCommentButton_Click(object sender, RoutedEventArgs e)
        {
            if (CommentsListBox.SelectedItem is Comments selectedComment)
            {
                using (var db = new Forum_with_cooking_recipesContext())
                {
                    //Проверяем, является ли пользователь администратором, или автором комментария
                    if (CurrentUser.RoleID == (int)UserRole.Administrator || CurrentUser.UserID == selectedComment.UserID)
                    {
                        var commentToRemove = db.Comments.Find(selectedComment.CommentID);
                        if (commentToRemove != null)
                        {
                            db.Comments.Remove(commentToRemove);
                            db.SaveChanges();
                            LoadComments(); // Обновляем список комментариев
                        }
                    }
                    else
                    {
                        MessageBox.Show("У вас нет прав для удаления этого комментария.");
                    }
                }
            }
        }
    }
}
