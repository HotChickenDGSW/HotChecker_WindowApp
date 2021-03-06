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
    /// CheckCardView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CheckingCardView : UserControl
    {
        public CheckingCardView()
        {
            InitializeComponent();
            Loaded += CheckingCardView_Loaded;
            App.cardViewModel.TextBoxFocusEventHandler += CardViewModel_TextBoxFocusEventHandler;
        }

        private void CardViewModel_TextBoxFocusEventHandler()
        {
            tbBarcode.Focus();
        }

        private void CheckingCardView_Loaded(object sender, RoutedEventArgs e)
        {
            tbBarcode.Focus();
            DataContext = App.cardViewModel; 
        }
    }
}
