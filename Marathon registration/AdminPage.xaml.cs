using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        List<Runners> list;
        string jsonRunner;
        public AdminPage()
        {
            InitializeComponent();
            Data.Value = "Marathon Skills 2022 - Admin Panel";
            jsonRunner = File.ReadAllText("runners.json");
            list = JsonConvert.DeserializeObject<List<Runners>>(jsonRunner);

            Count_Runner.Text = $"Всего пользователей: {list.Count}";
            dataGrid.ItemsSource = list;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RegestrationPage());
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Search.Text.Length != 0)
            {
                List<Runners> list2 = JsonConvert.DeserializeObject<List<Runners>>(jsonRunner);
                foreach (var item in list)
                {
                    if (item.Name.ToLower().Contains(Search.Text.ToLower()) || item.Last_Name.ToLower().Contains(Search.Text.ToLower()) || item.Email.ToLower().Contains(Search.Text.ToLower()))
                    {
                        list2.Clear();
                        list2.Add(item);
                    }
                }
                Count_Runner.Text = $"Всего пользователей: {list2.Count}";
                dataGrid.ItemsSource = list2;
            }
            else if (Search.Text.Length == 0)
            {
                dataGrid.ItemsSource = list;
            }
        }

        private void SortBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (SortBox.SelectedIndex)
            {
                case 0:
                    dataGrid.Items.SortDescriptions.Clear();
                    dataGrid.Items.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Descending));
                    break;
                case 1:
                    dataGrid.Items.SortDescriptions.Clear();
                    dataGrid.Items.SortDescriptions.Add(new SortDescription("Last_Name", ListSortDirection.Descending));
                    break;
                case 2:
                    dataGrid.Items.SortDescriptions.Clear();
                    dataGrid.Items.SortDescriptions.Add(new SortDescription("Email", ListSortDirection.Descending));
                    break;
                case 3:
                    dataGrid.Items.SortDescriptions.Clear();
                    dataGrid.Items.SortDescriptions.Add(new SortDescription("Sex", ListSortDirection.Descending));
                    break;
                case 4:
                    dataGrid.Items.SortDescriptions.Clear();
                    break;
            }
        }
    }
}
