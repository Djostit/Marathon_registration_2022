using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace Marathon_registration
{
    /// <summary>
    /// Логика взаимодействия для UserEditing.xaml
    /// </summary>
    public partial class UserEditing : Page
    {
        int index;
        DispatcherTimer timer = new();
        public UserEditing(int number)
        {
            InitializeComponent();
            index = number;
            this.DataContext = new Validation();
            

            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += TimerTickText;
            timer.Start();
        }
        List<Runners> list;
        private void TimerTickText(object sender, EventArgs e)
        {
            if (Password.Text.Length == 0)
            {
                var jsonRunner = File.ReadAllText(System.IO.Path.GetFullPath("Resources/runners.json").Replace(@"\bin\Debug\", @"\"));
                list = JsonConvert.DeserializeObject<List<Runners>>(jsonRunner);

                Login.Text = list[index].Email;
                Password.Text = list[index].Password;
                Retry_Pass.Text = list[index].Password;
                Name_runner.Text = list[index].Name;
                Last_Name.Text = list[index].Last_Name;
                Sex.Text = list[index].Sex;
                SuperTime.Text = list[index].Birth_Date;
                Country.Text = list[index].Country;
                if (!list[index].Photo.Contains("pack://application:,,,/Marathon registration;component/Resources/Photo.png"))
                {
                    ImageLogo.Source = new BitmapImage(new Uri($"{System.IO.Path.GetFullPath("User photos/").Replace(@"\bin\Debug\", @"\")}{System.IO.Path.GetFileName(list[index].Photo)}", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    ImageLogo.Source = new BitmapImage(new Uri(list[index].Photo, UriKind.RelativeOrAbsolute));
                }
                TextImage.Text = System.IO.Path.GetFileName(list[index].Photo);
            }
            else
            {
                timer.Stop();
                ButtonLogin.IsEnabled = false;
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Login.Text = list[index].Email;
            Password.Text = list[index].Password;
            Retry_Pass.Text = list[index].Password;
            Name_runner.Text = list[index].Name;
            Last_Name.Text = list[index].Last_Name;
            Sex.Text = list[index].Sex;
            SuperTime.Text = list[index].Birth_Date;
            Country.Text = list[index].Country;
            ImageLogo.Source = new BitmapImage(new Uri(list[index].Photo, UriKind.RelativeOrAbsolute));
            TextImage.Text = System.IO.Path.GetFileName(list[index].Photo);
        }
    }
}
