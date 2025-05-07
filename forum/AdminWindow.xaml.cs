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
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void ShowRecipePopularityReport_Click(object sender, RoutedEventArgs e)
        {
            RecipePopularityReportWindow reportWindow = new RecipePopularityReportWindow();
            reportWindow.Show();
        }

        private void ShowUserActivityReport_Click(object sender, RoutedEventArgs e)
        {
            UserActivityReportWindow reportWindow = new UserActivityReportWindow();
            reportWindow.Show();
        }

        private void ShowCategoryRecipeReport_Click(object sender, RoutedEventArgs e)
        {
            CategoryRecipeReportWindow reportWindow = new CategoryRecipeReportWindow();
            reportWindow.Show();
        }

        private void ShowCommentAnalysisReport_Click(object sender, RoutedEventArgs e)
        {
            CommentAnalysisReportWindow reportWindow = new CommentAnalysisReportWindow();
            reportWindow.Show();
        }
    }
}
