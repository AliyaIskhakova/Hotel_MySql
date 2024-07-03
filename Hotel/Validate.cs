using System.Text.RegularExpressions;

namespace Hotel
{
    public class Validate
    {
        public string message = "";
        
        public int ValidateSurnameOrName(string surname)
        {
            if (surname.Length > 40 || surname.Length < 1)
            {
                return 1;
            }
            else if (surname.Contains(" "))
            {
                return 2;
            }
            else
            {
                foreach (char c in surname)
                {
                    if (!char.IsLetter(c))
                    {
                        return 3;
                    }
                }
            }
            return 4;
        }
        public int ValidatePatronymic(string patronymic)
        {
            if (patronymic.Length == 0)
            {
                return 4;
            }
            if (patronymic.Length > 40)
            {
                return 1;
            }
            else if (patronymic.Contains(" "))
            {
                return 2;
            }
            else
            {
                foreach (char c in patronymic)
                {
                    if (!char.IsLetter(c))
                    {
                        return 3;
                    }
                }
            }
            return 4;
        }
        public int ValidateTelephone(string telephone)
        {
            if (telephone.Contains(" ") || telephone.Contains(",") || telephone.Length != 11)
            {
                return 0;
            }
            foreach (char c in telephone)
            {
                if (!char.IsDigit(c))
                {
                    return 0;
                }
            }
            return 1;
        }
        public int ValidatePassportSeria(string seria)
        {
            if (seria.Length < 4)
            {
                return 0;
            }
            else if (!int.TryParse(seria, out int s)|| seria.Contains(" "))
            {
                return 1;
            }
            return 2;
        }
        public int ValidatePassportNumber(string number)
        {
            if (number.Length < 6)
            {
                return 0;
            }
            else if (!int.TryParse(number, out int s) || number.Contains(" "))
            {
                return 1;
            }
            return 2;
        }
        public int ValidateEmail(string email)
        {
            if (!Regex.IsMatch(email, @"^[a-zA-Z0-9_]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$"))
            {
                return 0;
            }  
            return 1;
            
        }
        public int ValidateBirthday(DateTime day) { 
            if ( day <= DateTime.Now.AddYears(-100) || day >= DateTime.Now.AddYears(-14))
            {
                return 0;
            }
            return 1;
        }
        public bool ValidateAll(string Surname, string Name, string Patronymic, string Telephone, string Email, string PassportSeria, string PassportNumber, DateTime day)
        {
            int s = ValidateSurnameOrName(Surname);
            int n = ValidateSurnameOrName(Name);
            int ptr = ValidatePatronymic(Patronymic);
            int t = ValidateTelephone(Telephone);
            int e = ValidateEmail(Email);
            int pS = ValidatePassportSeria(PassportSeria);
            int pN = ValidatePassportNumber(PassportNumber);
            int d = ValidateBirthday(day);


            if (s != 4 || n != 4 || ptr!=4 || t != 1 || e != 1 || pS!=2 || pN!=2|| d!=1)
            {
                switch (s)
                {
                    case 1:
                        message += "Фамилия должна содержать 1 до 40 символов\n";
                        break;
                    case 2:
                        message += "Фамилия не должна содержать пробелы\n";
                        break;
                    case 3:
                        message += "Фамилия должна содержать только буквы\n";
                        break;
                }
                switch (n)
                {
                    case 1:
                        message += "Имя должно содержать 1 до 40 символов\n";
                        break;
                    case 2:
                        message += "Имя не должно содержать пробелы\n";
                        break;
                    case 3:
                        message += "Имя должно содержать только буквы\n";
                        break;
                }
                switch (ptr)
                {
                    case 1:
                        message += "Отчество должно содержать до 40 символов\n";
                        break;
                    case 2:
                        message += "Отчество не должно содержать пробелы\n";
                        break;
                    case 3:
                        message += "Отчество должно содержать только буквы\n";
                        break;
                }
                switch (t)
                {
                    case 0:
                        message += "Неверный формат номера телефона\n";
                        break;
                }
                switch (e)
                {
                    case 0:
                        message += "Неверный формат почты\n";
                        break;
                }
                switch (pS)
                {
                    case 0:
                        message += "Серия паспорта должна содержать 4 цифры\n";
                        break;
                    case 1:
                        message += "Серия паспорта должна содержать только цифры\n";
                        break;
                }
                switch (pN)
                {
                    case 0:
                        message += "Номер паспорта должен содержать 6 цифр\n";
                        break;
                    case 1:
                        message += "Номер паспорта должен содержать только цифры\n";
                        break;
                }
                switch (d)
                {
                    case 0:
                        message += "Некорректная дата рождения\n";
                        break;
                }
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
