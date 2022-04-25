using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для RegestrationPage.xaml
    /// </summary>
    public partial class RegestrationPage : Page
    {
        public RegestrationPage()
        {
            InitializeComponent();
            this.DataContext = new Validation();
        }
        private void Choice_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new();
            dialog.Title = "Выбор логотипа";
            dialog.Filter = "Все форматы |*.jpg*;*.jpeg*;*.jpe*;*.png|PNG (*.png*)|*.png|JPEG (*.jpg*,*.jpeg*,*.jpe*)|*.jpg*;*.jpeg*;*.jpe";
            if (dialog.ShowDialog() == true)
            {
                ImageLogo.Source = new BitmapImage(new Uri(dialog.FileName, UriKind.Absolute));
                TextImage.Text = System.IO.Path.GetFileName(dialog.FileName);
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());
        }
        string[] email_list = { "@mail.ru", "@yandex.ru", "@gmail.com", "@bk.ru", "@outlook.com" };
        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text.Length == 0 || !Login.Text.Contains('@') || Login.Text.Split('@')[0].Length < 2 || !email_list.Contains('@' + Login.Text.Split('@')[1]))
            {
                return;
            }
            int a = 0;
            int b = 0;
            int c = 0;
            for (int i = 0; i < Password.Text.Length; i++)
            {
                if (Char.IsDigit(Password.Text[i]))
                {
                    a++;
                }
                if (Char.IsUpper(Password.Text[i]))
                {
                    b++;
                }
                if (Char.IsSymbol(Password.Text[i]) || Char.IsPunctuation(Password.Text[i]))
                {
                    c++;
                }
            }
            //13.04.2022
            if (SuperTime.Text.Length != 0)
            {
                int day = int.Parse(SuperTime.Text.Split('.')[0]);
                int month = int.Parse(SuperTime.Text.Split('.')[1]);
                int year = int.Parse(SuperTime.Text.Split('.')[2]);
                int age = DateTime.Now.Year - year;
                if (DateTime.Now.Month < month|| DateTime.Now.Month == month && DateTime.Now.Day < day)
                {
                    age--;
                }
                if (age < 10 || age > 85) { return; }
            }
            if (Name_runner.Text.Length == 0 || 
                Last_Name.Text.Length == 0 || 
                Retry_Pass.Text != Password.Text || 
                (a == 0 || b == 0 || c == 0) || 
                Password.Text.Length < 6 || 
                Sex.Text.Length == 0 || 
                Country.Text.Length == 0 ||
                SuperTime.Text.Length == 0) { return; }
            this.NavigationService.Navigate(new RegestrationConfirmation());
            Data.Value = "Marathon Skills 2022 - Regestration confirmation";
        }
    }
}
