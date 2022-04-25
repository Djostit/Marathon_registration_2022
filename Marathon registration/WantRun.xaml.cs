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

namespace Marathon_registration
{
    /// <summary>
    /// Логика взаимодействия для WantRun.xaml
    /// </summary>
    public partial class WantRun : Page
    {
        public WantRun()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new LoginPage());
            Data.Value = "Marathon Skills 2022 - Login";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Data.Value = "Marathon Skills 2022";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RegestrationPage());
            Data.Value = "Marathon Skills 2022 - Register as Runner";
        }
    }
}
