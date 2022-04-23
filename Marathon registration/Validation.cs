using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

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
                            try
                            {
                                if (Email.Split('@')[1] != "mail.ru" || Email.Split('@')[0].Length == 0)
                                {
                                    error = "Некорректная почта";
                                }
                            }
                            catch {
                                if (Email.Length == 0)
                                {
                                    error = "Обязательное поле";
                                }
                                else
                                {
                                    error = "Некорректная почта";
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
                        }
                        break;
                    case "Retry_Password":
                        if (Retry_Password != Password)
                        {
                            error = "Пахнет наебом";
                        }
                        break;
                    case "Name":
                        if (Name == null)
                        {
                            error = "Обязательное поле";
                        }
                        else
                        {
                            if (Name.Length == 0)
                            {
                                error = "Обязательное поле";
                            }
                        }
                        break;
                    case "Last_Name":
                        if (Last_Name == null)
                        {
                            error = "Обязательное поле";
                        }
                        else
                        {
                            if (Last_Name.Length == 0)
                            {
                                error = "Обязательное поле";
                            }
                        }
                        break;
                    case "Sex":
                        if (Sex == null)
                        {
                            error = "Обязательное поле";
                        }
                        else
                        {
                            if (Sex.Length == 0)
                            {
                                error = "Обязательное поле";
                            }
                        }
                        break;
                    case "Country":
                        if (Country == null)
                        {
                            error = "Обязательное поле";
                        }
                        else
                        {
                            if (Country.Length == 0)
                            {
                                error = "Обязательное поле";
                            }
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
                        }
                        break;
                    case "SponsorName":
                        if(SponsorName == null)
                        {
                            error = "Обязательное поле";
                        }
                        else
                        {
                            if (SponsorName.Length == 0)
                            {
                                error = "Обязательное поле";
                            }
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
