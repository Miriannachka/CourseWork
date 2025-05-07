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
    public partial class CategoriesSidebar : UserControl
    {
        public CategoriesSidebar()
        {
            InitializeComponent();
        }

        // Событие, которое будет вызываться при выборе категории
        public event EventHandler<string> CategoriesSelected;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string categoryName = button.Content.ToString();
            CategoriesSelected?.Invoke(this, categoryName); // Вызываем событие
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}