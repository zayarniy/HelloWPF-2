using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace MVVMPasswordLightTool.ViewModel
{
    public class PasswordValidationRule : ValidationRule
    {

        //(Строчные и прописные латинские буквы, цифры):
        //Regex regex = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).*$");

        // (Строчные и прописные латинские буквы, цифры, спецсимволы. Минимум 8 символов):
        Regex regex = new Regex(@"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$");
        
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string password = value.ToString();
            //if (password.Length < 6) return new ValidationResult(false, "Пароль меньше 6 символов");
            if (!regex.IsMatch(password)) return new ValidationResult(false, "от 6 символов с использованием цифр, спец. символов, латиницы, наличием строчных и прописных символов");
            return new ValidationResult(true, null);
        }
    }

    public class LoginValidationRule : ValidationRule
    {

        //(Строчные и прописные латинские буквы, цифры):
        //Regex regex = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).*$");

        // Имя пользователя (с ограничением 2-20 символов, которыми могут быть буквы и цифры, первый символ обязательно буква)
        Regex regex = new Regex(@"^[a-zA-Z][a-zA-Z0-9-_\.]{1,20}$");

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string login = value.ToString();
            if (!regex.IsMatch(login)) return new ValidationResult(false, "ограничение 2-20 символов, которыми могут быть буквы и цифры, первый символ обязательно буква");
            return new ValidationResult(true, null);
        }
    }
}
