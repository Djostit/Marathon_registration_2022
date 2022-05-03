using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    static class Data
    {
        public static string Value { get; set; }
    }
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();

            frame.Navigate(new MainPage());

            RestoreButton.Visibility = Visibility.Collapsed;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timerTick;
            timer.Start();   

            Data.Value = "Marathon Skills 2022";

            DispatcherTimer timer1 = new DispatcherTimer();
            timer1.Interval = TimeSpan.FromMilliseconds(10);
            timer1.Tick += timerTickText;
            timer1.Start();
        }

        private void timerTickText(object sender, EventArgs e)
        {
            TitleText.Text = Data.Value;
        }

        private string getNoun(int number, string one, string two, string five)
        {
            int n = Math.Abs(number);
            n %= 100;
            if (n >= 5 && n <= 20)
            {
                return five;
            }
            n %= 10;
            if (n == 1)
            {
                return one;
            }
            if (n >= 2 && n <= 4)
            {
                return two;
            }
            return five;
        }
        private void timerTick(object sender, EventArgs e)
        {
            DateTime date = new DateTime(2022, 06, 01);
            TimeSpan TimeRemaining = date - DateTime.Now;
            if (TimeRemaining.Days < 0)
            {
                TextDatePanel.Text = "Уже наступило";
                timer.Stop();
            } else
            TextDatePanel.Text = $"{TimeRemaining.Days} {getNoun(TimeRemaining.Days, "день", "дня", "дней")} {TimeRemaining.Hours} {getNoun(TimeRemaining.Hours, "час", "часа", "часов")} {TimeRemaining.Minutes} {getNoun(TimeRemaining.Minutes, "минут", "минуты", "минут")} и {TimeRemaining.Seconds} {getNoun(TimeRemaining.Seconds, "секунд", "секунды", "секунд")} до начала марафона!";
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch { }
        }

        private void CloseCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void MinCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void MinCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaxCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void MaxCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }

        private void RestoreCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void RestoreCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            WindowState = WindowState.Normal;
        }

        private void MaxButton_Click(object sender, RoutedEventArgs e)
        {
            MaxButton.Visibility = Visibility.Collapsed;
            RestoreButton.Visibility = Visibility.Visible;
        }

        private void RestoreButton_Click(object sender, RoutedEventArgs e)
        {
            MaxButton.Visibility = Visibility.Visible;
            RestoreButton.Visibility = Visibility.Collapsed;
        }
    }
}
