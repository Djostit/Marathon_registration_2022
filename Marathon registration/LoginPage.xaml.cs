using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Marathon_registration
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        List<Runners> list;
        DispatcherTimer timer1;
        public LoginPage()
        {
            InitializeComponent();
            Data.Value = "Marathon Skills 2022 - Login";
            this.DataContext = new Validation();
            var jsonRunner = File.ReadAllText(System.IO.Path.GetFullPath("Resources/runners.json").Replace(@"\bin\Debug\", @"\"));
            list = JsonConvert.DeserializeObject<List<Runners>>(jsonRunner);
            Captcha.Visibility = Visibility.Collapsed;
            cAPTCHA.Visibility = Visibility.Collapsed;

            timer1 = new DispatcherTimer();
            timer1.Interval = TimeSpan.FromSeconds(10);
            timer1.Tick += TimerTickText;
        }

        private void TimerTickText(object sender, EventArgs e)
        {
            ButtonLogin.IsEnabled = true; 
            timer1.Stop();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());
        }
        private string TextCapth()
        {
            string allowchar = string.Empty;
            allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";
            allowchar += "1,2,3,4,5,6,7,8,9,0";
            string[] ar = allowchar.Split(',');
            string pwd = string.Empty;

            Random rnd = new();

            for (int i = 0; i <= 4; i++)
            {
                pwd += ar[(rnd.Next(0, ar.Length))];
            }
            if (pwd.Length != 4)
            {
                return pwd.Substring(0, 4);
            }
            else
            {
                return pwd;
            }
        }
        private void DrawCapth(string pwd)
        {
            Random rnd = new();
            System.Drawing.Rectangle rect = new(0, 0, 100, 30);

            // Создаём битмап с нужными размерами и форматом пикселей.
            Bitmap bmp1 = new(60, 20);
            var pen = new System.Drawing.Pen(System.Drawing.Color.White, 3);
            using (Graphics g = Graphics.FromImage(bmp1))
            using (Font font = new("Arial", 12))
            {
                // Заливаем фон нужным цветом.
                g.FillRectangle(System.Drawing.Brushes.White, rect);

                // Выводим текст.
                g.DrawString(pwd, font, System.Drawing.Brushes.Black, rect, StringFormat.GenericTypographic);
                g.DrawLine(pen, rnd.Next(30, 70), rnd.Next(10, 50), rnd.Next(5, 31), rnd.Next(0, 6));
                for (int i = 0; i < 100; i++)
                {
                    g.DrawRectangle(Pens.White, rnd.Next(0, 50), rnd.Next(0, 50), 1, 1); //белая точка
                }
            }
            Captcha.Source = Imaging.CreateBitmapSourceFromHBitmap(bmp1.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }
        string st;
        bool check = false;
        int attempt;
        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text == "admin" && Password.Text == "admin")
            {
                this.NavigationService.Navigate(new AdminPage());
                return;
            }
            if (Login.Text == "test" && Password.Text == "test")
            {
                this.NavigationService.Navigate(new Login_Choice(0));
                return;
            }
            Debug.WriteLine("1 " + st);
            if (attempt == 1)
            {
                ButtonLogin.IsEnabled = false;
                timer1.Start();
            }
            Debug.WriteLine($"кол-во: {attempt}");
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Email == Login.Text && list[i].Password == Password.Text)
                {
                    if (cAPTCHA.Text != st && Captcha.Visibility == Visibility.Visible)
                    {
                        attempt = 1;
                        break;
                    }
                    this.NavigationService.Navigate(new Login_Choice(i));
                    attempt = 0;
                    ButtonLogin.IsEnabled = true;
                    timer1.Stop();
                    check = false;
                    break;
                }
                else
                {
                    check = true;
                    attempt = 1;
                }
            }
            Debug.WriteLine($"кол-во: {attempt}");
            st = TextCapth();
            Debug.WriteLine("2 " + st);
            DrawCapth(st);
            if (check && Captcha.Visibility != Visibility.Visible && cAPTCHA.Visibility != Visibility.Visible)
            {
                Captcha.Visibility = Visibility.Visible;
                cAPTCHA.Visibility = Visibility.Visible;
            }
            else if(!check && Captcha.Visibility != Visibility.Collapsed && cAPTCHA.Visibility != Visibility.Collapsed)
            {
                Captcha.Visibility = Visibility.Collapsed;
                cAPTCHA.Visibility = Visibility.Collapsed;
                cAPTCHA.Clear();
            }
        }
    }
}
