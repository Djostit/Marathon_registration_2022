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
        public string Name { get; set; }
        public string Age { get; set; }
        public string Position { get; set; }
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Age":
                        if (Age != null)
                        {
                            try
                            {
                                string check = Age.Split('@')[1];
                                Debug.WriteLine(Age.Split('@')[0].Length);
                                if (check != "mail.ru" || Age.Split('@')[0].Length == 0)
                                {
                                    error = "Fuck";
                                }
                            }
                            catch { error = "Fuck"; }
                        }
                        break;
                    case "Name":
                        
                        break;
                    case "Position":
                        
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
