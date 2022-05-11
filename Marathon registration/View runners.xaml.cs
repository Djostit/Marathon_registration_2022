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
    /// Логика взаимодействия для View_runners.xaml
    /// </summary>
    public partial class View_runners : Page
    {
        List<Runners> list;
        string jsonRunner;
        public View_runners()
        {
            InitializeComponent();
            jsonRunner = File.ReadAllText(System.IO.Path.GetFullPath("Resources/runners.json").Replace(@"\bin\Debug\", @"\"));
            list = JsonConvert.DeserializeObject<List<Runners>>(jsonRunner);

            Count_Runner.Text = $"Всего пользователей: {list.Count}";
            listView.ItemsSource = list;
            
            
        }

        private void SortBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (SortBox.SelectedIndex)
            {
                case 0:
                    listView.Items.SortDescriptions.Clear();
                    listView.Items.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Descending));
                    break;
                case 1:
                    listView.Items.SortDescriptions.Clear();
                    listView.Items.SortDescriptions.Add(new SortDescription("Last_Name", ListSortDirection.Descending));
                    break;
                case 2:
                    listView.Items.SortDescriptions.Clear();
                    listView.Items.SortDescriptions.Add(new SortDescription("Sex", ListSortDirection.Descending));
                    break;
                case 3:
                    listView.Items.SortDescriptions.Clear();
                    listView.Items.SortDescriptions.Add(new SortDescription("Birth_Date_Year", ListSortDirection.Descending));
                    break;
                case 4:
                    listView.Items.SortDescriptions.Clear();
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Search.Text.Length != 0)
            {
                List<Runners> list2 = JsonConvert.DeserializeObject<List<Runners>>(jsonRunner);
                list2.Clear();
                foreach (var item in list)
                {
                    if (item.Name.ToLower().Contains(Search.Text.ToLower()) || item.Last_Name.ToLower().Contains(Search.Text.ToLower()) || item.Sex.ToLower().Contains(Search.Text.ToLower()) || item.Last_Name.ToLower().Contains(Search.Text.ToLower()) || item.Country.ToLower().Contains(Search.Text.ToLower()))
                    {   
                        list2.Add(item);
                    }
                }
                Count_Runner.Text = $"Всего пользователей: {list2.Count} из {list.Count}";
                listView.ItemsSource = list2;
            }
            else if (Search.Text.Length == 0)
            {
                var jsonRunner = File.ReadAllText(System.IO.Path.GetFullPath("Resources/runners.json").Replace(@"\bin\Debug\", @"\"));
                List<Runners> list = JsonConvert.DeserializeObject<List<Runners>>(jsonRunner);
                Count_Runner.Text = $"Всего пользователей: {list.Count}";
                listView.ItemsSource = list;
                listView.UpdateLayout();
            }
        }
    }
}
