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
    public partial class RecipePopularityReportWindow : Window
    {
        public RecipePopularityReportWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var data = ReportGenerator.GetRecipePopularityData();
            RecipePopularityDataGrid.ItemsSource = data;
        }
    }
}
