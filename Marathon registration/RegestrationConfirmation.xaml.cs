using Microsoft.Win32;
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
using Word = Microsoft.Office.Interop.Word;

namespace Marathon_registration
{
    /// <summary>
    /// Логика взаимодействия для RegestrationConfirmation.xaml
    /// </summary>

    public partial class RegestrationConfirmation : Page
    {
        int index;
        public RegestrationConfirmation(int nubmer)
        {
            InitializeComponent();
            index = nubmer;
            Data.Value = "Marathon Skills 2022 - Registration confirmation";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new();
            sfd.Title = "Сохранение отчёта";
            sfd.Filter = "Все форматы |*.docx|DOC (*.docx)|*.docx";
            if (sfd.ShowDialog() == true)
            {
                var wordApp = new Word.Application();
                wordApp.Visible = false;

                var wordDocument = wordApp.Documents.Open(System.IO.Path.GetFullPath("Resources/pattern.docx").Replace(@"\bin\Debug\", @"\"));

                var jsonRunner = File.ReadAllText(System.IO.Path.GetFullPath("Resources/runners.json").Replace(@"\bin\Debug\", @"\"));
                List<Runners> list = JsonConvert.DeserializeObject<List<Runners>>(jsonRunner);

                ReplaceWord("{email}", list[index].Email, wordDocument);
                ReplaceWord("{date_1}", $"{DateTime.Now:dddd d MMMM yyy}", wordDocument);
                DateTime date = DateTime.Now;
                date = date.AddDays(3);
                ReplaceWord("{date_2}", $"{date:dddd d MMMM yyy}", wordDocument);
                ReplaceWord("{sum}", $"{list[index].Birth_Date_Year}0", wordDocument);
                ReplaceWord("{fio}", $"{list[index].Name} {list[index].Last_Name}", wordDocument);
                ReplaceWord("{email}", list[index].Email, wordDocument);

                wordDocument.SaveAs(sfd.FileName);
                wordApp.Application.Quit();
            }
            this.NavigationService.Navigate(new MainPage());
        }
        private void ReplaceWord(string one, string two, Word.Document WordDoc)
        {
            var range = WordDoc.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: one, ReplaceWith: two);
        }
    }
}
