using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для AdminEditing.xaml
    /// </summary>
    public partial class AdminEditing : Page
    {
        string Email_temp;
        string Password_temp;
        string Name_temp;
        string Last_Name_temp;
        string Sex_temp;
        string Date_temp;
        string Contry_temp;
        string Photo_temp;
        DispatcherTimer timer = new();
        public AdminEditing(string email, string password, string name, string last_name, string sex, string date, string country, string photo)
        {
            InitializeComponent();

            Email_temp = email;
            Password_temp = password;
            Name_temp = name;
            Last_Name_temp = last_name;
            Sex_temp = sex;
            Date_temp = date;
            Contry_temp = country;
            Photo_temp = photo;

            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += timerTickText;
            timer.Start();
        }

        private void timerTickText(object sender, EventArgs e)
        {

            if (Email.Text.Length == 0)
            {
                Email.Text = Email_temp;
                Password.Text = Password_temp;
                NameText.Text = Name_temp;
                Last_Name.Text = Last_Name_temp;
                Sex.Text = Sex_temp;
                SuperTime.Text = Date_temp;
                Country.Text = Contry_temp;
                ImageLogo.Source = new BitmapImage(new Uri(Photo_temp, UriKind.RelativeOrAbsolute));
                TextImage.Text = System.IO.Path.GetFileName(Photo_temp);
            }
            else
            {
                timer.Stop();
            }
        }
        OpenFileDialog dialog = new();
        bool check = false;
        private void Choice_Click(object sender, RoutedEventArgs e)
        {
            dialog = new();
            dialog.Title = "Выбор логотипа";
            dialog.Filter = "Все форматы |*.jpg*;*.jpeg*;*.jpe*;*.png|PNG (*.png*)|*.png|JPEG (*.jpg*,*.jpeg*,*.jpe*)|*.jpg*;*.jpeg*;*.jpe";
            if (dialog.ShowDialog() == true)
            {
                ImageLogo.Source = new BitmapImage(new Uri(dialog.FileName, UriKind.Absolute));
                TextImage.Text = System.IO.Path.GetFileName(dialog.FileName);
                check = true;
            }
        }
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            var jsonRunner = File.ReadAllText(System.IO.Path.GetFullPath("Resources/runners.json").Replace(@"\bin\Debug\", @"\"));
            List<Runners> list = JsonConvert.DeserializeObject<List<Runners>>(jsonRunner);
            if (check)
            {
                try
                {
                    File.Copy(dialog.FileName, $"{System.IO.Path.GetFullPath("User photos/").Replace(@"\bin\Debug\", @"\")}{System.IO.Path.GetFileName(dialog.FileName)}");
                }
                catch (IOException) { }
                ImageLogo.Source = new BitmapImage(new Uri($"{System.IO.Path.GetFullPath("User photos/").Replace(@"\bin\Debug\", @"\")}{System.IO.Path.GetFileName(dialog.FileName)}", UriKind.RelativeOrAbsolute));
            }
            foreach (var item in list)
            {
                if (item.Email.Contains(Email_temp))
                {
                    item.Email = Email.Text;
                    item.Password = Password.Text;
                    item.Name = NameText.Text;
                    item.Last_Name = Last_Name.Text;
                    item.Sex = Sex.Text;
                    item.Birth_Date = SuperTime.Text;
                    item.Country = Country.Text;
                    item.Photo = ImageLogo.Source.ToString();
                    Debug.WriteLine(item.Photo);
                    break;
                }
            }
            this.NavigationService.Navigate(new AdminPage());
            var convertedJson = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText(System.IO.Path.GetFullPath("Resources/runners.json").Replace(@"\bin\Debug\", @"\"), convertedJson);
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Email.Text = Email_temp;
            Password.Text = Password_temp;
            NameText.Text = Name_temp;
            Last_Name.Text = Last_Name_temp;
            Sex.Text = Sex_temp;
            SuperTime.Text = Date_temp;
            Country.Text = Contry_temp;
            ImageLogo.Source = new BitmapImage(new Uri(Photo_temp, UriKind.RelativeOrAbsolute));
            TextImage.Text = System.IO.Path.GetFileName(Photo_temp);
        }
        
    }
}
