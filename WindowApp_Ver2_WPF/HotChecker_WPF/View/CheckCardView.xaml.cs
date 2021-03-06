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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotChecker_WPF.View
{
    /// <summary>
    /// Interaction logic for CheckCardView.xaml
    /// </summary>
    public partial class CheckCardView : UserControl
    {
        public CheckCardView()
        {
            InitializeComponent();
            Loaded += CheckCardView_Loaded;
        }

        private void CheckCardView_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = App.cardViewModel;
        }
    }
}
