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
                    break;
            }
        }
    }
}
