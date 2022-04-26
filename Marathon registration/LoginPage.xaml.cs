using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        List<Runners> list;
        public LoginPage()
        {
            InitializeComponent();
            this.DataContext = new Validation();
            var jsonRunner = File.ReadAllText("runners.json");
            list = JsonConvert.DeserializeObject<List<Runners>>(jsonRunner);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Data.Value = "Marathon Skills 2022";
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());
            Data.Value = "Marathon Skills 2022";
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text == "admin@bk.ru" && Password.Text == "admin")
            {
                this.NavigationService.Navigate(new AdminPage());
                return;
            }
            foreach (var item in list)
            {
                if (item.Email == Login.Text && item.Password == Password.Text)
                {
                    MessageBox.Show("1");
                }
            }
        }
    }
}
