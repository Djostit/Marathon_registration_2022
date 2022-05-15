using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace Marathon_registration
{
    internal class Validation : IDataErrorInfo
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Retry_Password { get; set; }
        public string Name { get; set; }
        public string Last_Name { get; set; }
        public string Sex { get; set; }
        public string Country { get; set; }
        public DateTime SuperTime { get; set; } = DateTime.Now;
        public string SponsorName { get; set; }
        public int Ammount { get; set; }
        string[] email_list = { "@mail.ru", "@yandex.ru", "@gmail.com", "@bk.ru", "@outlook.com" };
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Email":
                        if (Email == null)
                        {
                            error = "Обязательное поле";
                        }
                        else
                        {
                            if (Email.Length == 0 || !Email.Contains('@') || Email.Split('@')[0].Length < 2 || !email_list.Contains('@' + Email.Split('@')[1]))
                            {
                                error = "Некорректная почта";
                            }
                            else
                            {
                                var jsonRunner = File.ReadAllText(System.IO.Path.GetFullPath("Resources/runners.json").Replace(@"\bin\Debug\", @"\"));
                                List<Runners> list = JsonConvert.DeserializeObject<List<Runners>>(jsonRunner);
                                foreach (var item in list)
                                {
                                    if (item.Email.Contains(Email))
                                    {
                                        error = "Почта уже существует";
                                        break;
                                    }
                                }
                            }
                        }
                     break;
                    case "Password":
                        if (Password == null)
                        {
                            error = "Обязательное поле";
                        }
                        else
                        {
                            if (Password.Length == 0)
                            {
                                error = "Обязательное поле";
                            }
                            else if (Password.Length < 6)
                            {
                                error = "Некорректный пароль";
                            }
                            int a = 0;
                            int b = 0;
                            int c = 0;
                            for (int i = 0; i < Password.Length; i++)
                            {
                                if (Char.IsDigit(Password[i]))
                                {
                                    a++;
                                }
                                if (Char.IsUpper(Password[i]))
                                {
                                    b++;
                                }
                                if (Char.IsSymbol(Password[i]) || Char.IsPunctuation(Password[i]))
                                {
                                    c++;
                                }
                            }
                            if (a == 0 || b == 0 || c == 0)
                            {
                                error = "Некорректный пароль";
                            }
                        }
                        break;
                    case "Retry_Password":
                        if (Retry_Password == null)
                        {
                            error = "Пароли не совпадают";
                        }
                        else if (Retry_Password != Password)
                        {
                            error = "Пароли не совпадают";
                        }
                        break;
                    case "Name":
                        if (Name == null)
                        {
                            error = "Обязательное поле";
                        }
                        else if (Name.Length == 0)
                        {
                            error = "Обязательное поле";
                        }
                        break;
                    case "Last_Name":
                        if (Last_Name == null)
                        {
                            error = "Обязательное поле";
                        }
                        else if (Last_Name.Length == 0)
                        {
                            error = "Обязательное поле";
                        }
                        break;
                    case "Sex":
                        if (Sex == null)
                        {
                            error = "Обязательное поле";
                        }
                        else if (Sex.Length == 0)
                        {
                            error = "Обязательное поле";
                        }
                        break;
                    case "Country":
                        if (Country == null)
                        {
                            error = "Обязательное поле";
                        }
                        else if (Country.Length == 0)
                        {
                            error = "Обязательное поле";
                        }
                        break;
                    case "SuperTime":
                        if (SuperTime == DateTime.MinValue)
                        {
                            error = "Обязательное поле";
                        }
                        else
                        {
                            if (SuperTime == DateTime.MinValue)
                            {
                                error = "Обязательное поле";
                            }
                            int age = DateTime.Now.Year - SuperTime.Year;
                            if (DateTime.Now.Month < SuperTime.Month || DateTime.Now.Month == SuperTime.Month && DateTime.Now.Day < SuperTime.Day)
                            {
                                age--;
                            }
                            if (age < 10)
                            {
                                error = "Вам меньше 10 лет";
                            }
                            else if (age > 85)
                            {
                                error = "Вам больше 85 лет";
                            }
                        }
                        break;
                    case "SponsorName":
                        if (SponsorName == null)
                        {
                            error = "Обязательное поле";
                        }
                        else if (SponsorName.Length == 0)
                        {
                            error = "Обязательное поле";
                        }
                        break;
                    case "Ammount":
                        try
                        {
                            if (Ammount < 1)
                            {
                                error = "Минимальное пожертвование 1";
                            }
                            if (Ammount > 50)
                            {
                                error = "Максимальное пожертвование 50";
                            }
                        }
                        catch { error = "Обязательное поле"; }
                        break;
                }
                return error;
            }
        }
        public string Error
        {
            get { throw new NotImplementedException(); }
        }
    }
}
