﻿using System;
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
    public partial class CommentAnalysisReportWindow : Window
    {
        public CommentAnalysisReportWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var data = ReportGenerator.GetCommentAnalysisData();
            CommentAnalysisDataGrid.ItemsSource = data;
        }
    }
}
