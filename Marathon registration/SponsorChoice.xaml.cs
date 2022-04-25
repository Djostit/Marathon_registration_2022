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
    /// Логика взаимодействия для SponsorChoice.xaml
    /// </summary>
    public partial class SponsorChoice : Page
    {
        public SponsorChoice()
        {
            InitializeComponent();
            this.DataContext = new Validation();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SponsorName.Text = "";
            Ammount.Text = "0";
        }

        private void Ammount_TextInput(object sender, TextCompositionEventArgs e)
        {
            if (Ammount.Text.Contains("0"))
            {
                Ammount.Text = "";
            }
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (SponsorName.Text.Length != 0 && int.Parse(Ammount.Text) > 0 && int.Parse(Ammount.Text) <= 50)
            {
                Sponsor.SponsorName = SponsorName.Text;
                Sponsor.Ammount = int.Parse(Ammount.Text);
                this.NavigationService.Navigate(new SponsorPage());
                Data.Value = "Marathon Skills 2022 - Sponsor confirmation";
            }
        }

        private void SponsorName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Data.Value = "Marathon Skills 2022";
        }
    }
}
