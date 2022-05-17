using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;


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
            jsonRunner = File.ReadAllText(System.IO.Path.GetFullPath("Resources/runners.json").Replace(@"\bin\Debug\", @"\"));
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
                list2.Clear();
                foreach (var item in list)
                {
                    if (item.Name.ToLower().Contains(Search.Text.ToLower()) || item.Last_Name.ToLower().Contains(Search.Text.ToLower()) || item.Email.ToLower().Contains(Search.Text.ToLower()))
                    {
                        list2.Add(item);
                    }
                }
                Count_Runner.Text = $"Всего пользователей: {list2.Count}";
                dataGrid.ItemsSource = list2;
            }
            else if (Search.Text.Length == 0)
            {
                var jsonRunner = File.ReadAllText(System.IO.Path.GetFullPath("Resources/runners.json").Replace(@"\bin\Debug\", @"\"));
                List<Runners> list = JsonConvert.DeserializeObject<List<Runners>>(jsonRunner);
                Count_Runner.Text = $"Всего пользователей: {list.Count}";
                dataGrid.ItemsSource = list;
                dataGrid.UpdateLayout();
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

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var jsonRunner = File.ReadAllText(System.IO.Path.GetFullPath("Resources/runners.json").Replace(@"\bin\Debug\", @"\"));
            List<Runners> list = JsonConvert.DeserializeObject<List<Runners>>(jsonRunner);
            this.NavigationService.Navigate(new AdminEditing(list[dataGrid.SelectedIndex].Email, list[dataGrid.SelectedIndex].Password, list[dataGrid.SelectedIndex].Name, list[dataGrid.SelectedIndex].Last_Name, list[dataGrid.SelectedIndex].Sex, list[dataGrid.SelectedIndex].Birth_Date, list[dataGrid.SelectedIndex].Country, list[dataGrid.SelectedIndex].Photo));
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new();
            sfd.Title = "Сохранение отчёта";
            sfd.Filter = "Все форматы |*.docx*;*.xlsx*;*.pdf|DOC (*.docx*)|*.docx|XLSX (*.xlsx*)|*.xlsx|PDF (*.pdf)|*.pdf";
            if (sfd.ShowDialog() == true)
            {
                if (System.IO.Path.GetExtension(sfd.FileName) == ".docx")
                {
                    int RowCount = dataGrid.Items.Count;
                    int ColumnCount = dataGrid.Columns.Count-1;
                    Object[,] DataArray = new object[RowCount, ColumnCount];
                    //Добавление строк и ячеек
                    int r = 0;
                    foreach (var row in list)
                    {

                        DataArray[r, 0] = row.Name;
                        DataArray[r, 1] = row.Last_Name;
                        DataArray[r, 2] = row.Email;
                        DataArray[r, 3] = row.Sex;

                        r++;
                    }
                    Word.Document oDoc = new();
                    //Ориентация листа
                    oDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;
                    dynamic oRange = oDoc.Content.Application.Selection.Range;
                    string oTemp = "";
                    for (r = 0; r <= RowCount - 1; r++)
                    {
                        for (int c = 0; c <= ColumnCount - 1; c++)
                        {
                            if (c != ColumnCount)
                                oTemp = oTemp + DataArray[r, c] + "\t";
                            else
                                oTemp = oTemp + DataArray[r, c];
                        }
                    }

                    //Формат таблицы
                    oRange.Text = oTemp;
                    object oMissing = Missing.Value;
                    object Separator = Word.WdTableFieldSeparator.wdSeparateByTabs;
                    object ApplyBorders = true;
                    object AutoFit = true;
                    object AutoFitBehavior = Word.WdAutoFitBehavior.wdAutoFitContent;
                    oRange.ConvertToTable(ref Separator, ref RowCount, ref ColumnCount,
                                          Type.Missing, Type.Missing, ref ApplyBorders,
                                          Type.Missing, Type.Missing, Type.Missing,
                                          Type.Missing, Type.Missing, Type.Missing,
                                          Type.Missing, ref AutoFit, ref AutoFitBehavior, Type.Missing);
                    oRange.Select();

                    oDoc.Application.Selection.Tables[1].Select();
                    oDoc.Application.Selection.Tables[1].Rows.AllowBreakAcrossPages = 0;
                    oDoc.Application.Selection.Tables[1].Rows.Alignment = Word.WdRowAlignment.wdAlignRowCenter;
                    oDoc.Application.Selection.Tables[1].Rows[1].Select();
                    oDoc.Application.Selection.InsertRowsAbove(1);
                    oDoc.Application.Selection.Tables[1].Rows[1].Select();

                    //Стиль заголовка таблицы
                    oDoc.Application.Selection.Tables[1].Rows[1].Range.Bold = 2;
                    oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Name = "Open Sans";
                    oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Size = 14;

                    //add header row manually
                    for (int c = 0; c <= ColumnCount - 1; c++)
                    {
                        oDoc.Application.Selection.Tables[1].Cell(1, c + 1).Range.Text = dataGrid.Columns[c].Header.ToString();
                    }

                    //Стили таблицы
                    oDoc.Application.Selection.Tables[1].Rows[1].Select();
                    oDoc.Application.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    oDoc.Application.Selection.Tables[1].Borders.Enable = 1;

                    //Текст шапки
                    foreach (Word.Section section in oDoc.Application.ActiveDocument.Sections)
                    {
                        Word.Range headerRange = section.Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                        headerRange.Fields.Add(headerRange, Word.WdFieldType.wdFieldPage);
                        headerRange.Text = "Список спортсменов";
                        headerRange.Font.Size = 16;
                        headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    }
                    oDoc.Application.Application.Selection.Tables[1].Rows[dataGrid.Items.Count+2].Delete();
                    oDoc.Application.Visible = true;

                }
                else if (System.IO.Path.GetExtension(sfd.FileName) == ".xlsx") // Полностью готовый вариант.
                {
                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    using (var package = new ExcelPackage())
                    {
                        ExcelPackage excel = new ExcelPackage();
                        // название листа
                        var workSheet = excel.Workbook.Worksheets.Add($"Отчет за {DateTime.Now:dddd d MMMM yyy}");

                        // установка свойств
                        // рабочего листа
                        workSheet.DefaultRowHeight = 12;

                        workSheet.Cells["A1:D1"].Merge = true;
                        workSheet.Cells["A1:D1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        workSheet.Cells["A1:D1"].Style.Font.Name = "Open Sans SemiBold";
                        workSheet.Cells["A1:D1"].Style.Font.Bold = true;
                        workSheet.Cells["A1:D1"].Value = "Список пользователей";

                        // Установка свойств
                        // первого ряда
                        workSheet.Row(2).Height = 20;
                        workSheet.Row(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        workSheet.Row(2).Style.Font.Name = "Open Sans SemiBold";
                        //workSheet.Row(2).Style.Fill.PatternType = ExcelFillStyle.DarkHorizontal;
                        //workSheet.Row(2).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                        workSheet.Row(2).Style.Font.Bold = true;

                        // Заголовок листа Excel
                        workSheet.Cells[2, 1].Value = "Имя";
                        workSheet.Cells[2, 2].Value = "Фамилия";
                        workSheet.Cells[2, 3].Value = "Эл.почта";
                        workSheet.Cells[2, 4].Value = "Пол";

                        // Вставка данных статьи в Excel
                        // лист с использованием для каждого цикла
                        // Поскольку у нас есть значения в первой строке
                        // начнем со второго ряда

                        int r = 3;
                        foreach (var item in list)
                        {
                            workSheet.Cells[r, 1].Value = item.Name;
                            workSheet.Cells[r, 2].Value = item.Last_Name;
                            workSheet.Cells[r, 3].Value = item.Email;
                            workSheet.Cells[r, 4].Value = item.Sex;
                            r++;
                        }

                        // По умолчанию ширина столбца не
                        // установить для автоподбора содержимого
                        // диапазона, поэтому мы используем
                        // Метод AutoFit () здесь.
                        workSheet.Column(1).AutoFit();
                        workSheet.Column(2).AutoFit();
                        workSheet.Column(3).AutoFit();
                        workSheet.Column(4).AutoFit();
                        for (int i = 3; i < list.Count+3; i++)
                        {
                            workSheet.Row(i).Style.Font.Name = "Open Sans Light";
                        }
                        workSheet.Cells["A1:D1"].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                        workSheet.Cells[$"D2:D{list.Count+2}"].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                        workSheet.Cells[$"A2:A{list.Count+2}"].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                        workSheet.Cells[$"A{list.Count+2}:D{list.Count+2}"].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                        workSheet.Cells[$"A{list.Count + 3}:D{list.Count + 3}"].Merge = true;
                        workSheet.Cells[$"A{list.Count + 3}:D{list.Count + 3}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        workSheet.Cells[$"A{list.Count + 3}:D{list.Count + 3}"].Style.Font.Name = "Open Sans SemiBold";
                        workSheet.Cells[$"A{list.Count + 3}:D{list.Count + 3}"].Style.Font.Bold = true;
                        workSheet.Cells[$"A{list.Count + 3}:D{list.Count + 3}"].Value = $"Дата формирования: {DateTime.Now:dddd d MMMM yyy}";

                        workSheet.Cells[$"A{list.Count + 4}:D{list.Count + 4}"].Merge = true;
                        workSheet.Cells[$"A{list.Count + 4}:D{list.Count + 4}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        workSheet.Cells[$"A{list.Count + 4}:D{list.Count + 4}"].Style.Font.Name = "Open Sans SemiBold";
                        workSheet.Cells[$"A{list.Count + 4}:D{list.Count + 4}"].Style.Font.Bold = true;
                        workSheet.Cells[$"A{list.Count + 4}:D{list.Count + 4}"].Value = $"Количество пользователей: {list.Count}";

                        // Создать файл Excel на физическом диске
                        FileStream objFileStrm = File.Create(sfd.FileName);
                        objFileStrm.Close();
                        // Записать содержимое в файл excel
                        File.WriteAllBytes(sfd.FileName, excel.GetAsByteArray());
                    }
                }
                else if (System.IO.Path.GetExtension(sfd.FileName) == ".pdf") // Полностью готовый вариант.
                {
                    //Объект документа пдф
                    Document doc = new();

                    //Создаем объект записи пдф-документа в файл
                    PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));

                    //Открываем документ
                    doc.Open();

                    //Определение шрифта необходимо для сохранения кириллического текста
                    //Иначе мы не увидим кириллический текст
                    //Если мы работаем только с англоязычными текстами, то шрифт можно не указывать
                    BaseFont baseFont_1 = BaseFont.CreateFont(System.IO.Path.GetFullPath("Font/SemiBold.ttf").Replace(@"\bin\Debug\", @"\"), BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                    Font font_1 = new(baseFont_1, Font.DEFAULTSIZE, Font.NORMAL);

                    BaseFont baseFont_2 = BaseFont.CreateFont(System.IO.Path.GetFullPath("Font/Light.ttf").Replace(@"\bin\Debug\", @"\"), BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                    Font font_2 = new(baseFont_2, Font.DEFAULTSIZE, Font.NORMAL);

                    PdfPTable table = new(dataGrid.Columns.Count - 1);
                    PdfPCell cell = new(new Phrase("Список пользователей", font_1));
                    cell.Colspan = dataGrid.Columns.Count - 1;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_CENTER;
                    cell.Border = 0;
                    table.AddCell(cell);
                    //Сначала добавляем заголовки таблицы
                    for (int j = 0; j < dataGrid.Columns.Count - 1; j++)
                    {
                        cell = new PdfPCell(new Phrase(new Phrase((string)dataGrid.Columns[j].Header, font_1)))
                        {
                            //Фоновый цвет (необязательно, просто сделаем по красивее)
                            BackgroundColor = BaseColor.LIGHT_GRAY,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_CENTER
                        };

                        table.AddCell(cell);
                    }
                    for (int j = 0; j < dataGrid.Items.Count; j++)
                    {
                        float[] anchoDeColumnas = new float[] { 15f, 15f, 20f, 10f };
                        table.SetWidths(anchoDeColumnas);
                        table.AddCell(new Phrase(list[j].Name, font_2));
                        table.AddCell(new Phrase(list[j].Last_Name, font_2));
                        table.AddCell(new Phrase(list[j].Email, font_2));
                        table.AddCell(new Phrase(list[j].Sex, font_2));

                    }
                    cell = new(new Phrase($"Дата формирования: {DateTime.Now:dddd d MMMM yyy}", font_1));
                    cell.Colspan = dataGrid.Columns.Count+1;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_CENTER;
                    cell.Border = 0;
                    table.AddCell(cell);
                    cell = new(new Phrase($"Количество пользователей: {list.Count}", font_1));
                    cell.Colspan = dataGrid.Columns.Count + 1;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.VerticalAlignment = Element.ALIGN_CENTER;
                    cell.Border = 0;
                    table.AddCell(cell);
                    //Добавляем таблицу в документ
                    doc.Add(table);
                    //Закрываем документ
                    doc.Close();
                }
            }
        }
    }
}
