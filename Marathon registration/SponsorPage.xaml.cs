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

namespace Marathon_registration
{
    /// <summary>
    /// Логика взаимодействия для SponsorPage.xaml
    /// </summary>
    class Sponsor
    {
        public static string SponsorName { get; set; }
        public static int Ammount { get; set; }
    }
    public partial class SponsorPage : Page
    {
        public SponsorPage()
        {
            InitializeComponent();
            NameSponsor.Text = Sponsor.SponsorName;
            Ammount.Text = Sponsor.Ammount.ToString() + "$";
            Data.Value = "Marathon Skills 2022 - Sponsor paid";

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());
        }
    }
}
