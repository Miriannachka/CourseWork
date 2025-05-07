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
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;
using OxyPlot.Wpf;

namespace forum
{
    public partial class UserActivityReportWindow : Window
    {
        private const int BarWidth = 20; // Ширина столбца
        private const int BarSpacing = 10; // Расстояние между столбцами
        private const int MarginBottom = 20; // Отступ снизу для меток

        public UserActivityReportWindow()
        {
            InitializeComponent();
            Loaded += UserActivityReportWindow_Loaded; // Подписываемся на событие Loaded
        }

        private void UserActivityReportWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData(); // Загружаем данные после загрузки окна
        }

        private void LoadData()
        {
            var data = ReportGenerator.GetUserActivityData();

            // Определяем максимальное значение для масштабирования графика
            int maxValue = data.Max(u => Math.Max(u.TotalRecipes, u.TotalComments));

            // Рассчитываем высоту Canvas
            double canvasHeight = ChartCanvas.ActualHeight - MarginBottom;
            double canvasWidth = ChartCanvas.ActualWidth;

            // Рассчитываем масштабный коэффициент
            double scaleFactor = canvasHeight / maxValue;

            // Отрисовываем столбцы для каждого пользователя
            for (int i = 0; i < data.Count; i++)
            {
                var user = data[i];

                // Рассчитываем позицию столбца
                double x = i * (BarWidth + BarSpacing);

                // Рассчитываем высоту столбцов
                double recipeBarHeight = user.TotalRecipes * scaleFactor;
                double commentBarHeight = user.TotalComments * scaleFactor;

                // Создаем столбцы для рецептов
                Rectangle recipeBar = CreateBar(x, canvasHeight - recipeBarHeight, BarWidth, recipeBarHeight, Brushes.Blue);

                // Создаем столбцы для комментариев
                Rectangle commentBar = CreateBar(x + BarWidth / 2, canvasHeight - commentBarHeight, BarWidth / 2, commentBarHeight, Brushes.Green);

                // Создаем метку для имени пользователя
                TextBlock label = CreateLabel(user.Username, x, canvasHeight + 5);

                // Добавляем элементы на Canvas
                ChartCanvas.Children.Add(recipeBar);
                ChartCanvas.Children.Add(commentBar);
                ChartCanvas.Children.Add(label);
            }
        }

        // Вспомогательный метод для создания столбца
        private Rectangle CreateBar(double x, double y, double width, double height, Brush fill)
        {
            Rectangle bar = new Rectangle
            {
                Width = width,
                Height = height,
                Fill = fill,
                Stroke = Brushes.Black
            };
            Canvas.SetLeft(bar, x);
            Canvas.SetTop(bar, y);
            return bar;
        }

        // Вспомогательный метод для создания метки
        private TextBlock CreateLabel(string text, double x, double y)
        {
            TextBlock label = new TextBlock
            {
                Text = text,
                RenderTransform = new RotateTransform(-45),  // Поворот текста
                Foreground = Brushes.Black
            };
            Canvas.SetLeft(label, x);
            Canvas.SetTop(label, y);
            return label;
        }
    }
}