using static Login_Bank_Account.BankS;

namespace Login_Bank_Account;

public class AccountS
{
    public class AccountService
    {
        private BankService _bankService;
        private User _currentUser;

        public AccountService(BankService bankService)
        {
            _bankService = bankService;
        }

        public User CurrentUser => _currentUser;

        public string Register(string name, string surname, string email, string password, bool isAdmin)
        {
            if (name.Length < 3 || surname.Length < 3)
                return "Name və ya surname minimum 3 simvol olmalıdır.";

            if (!email.Contains("@"))
                return "Email düzgün formatda deyil.";

            if (_bankService.IsEmailTaken(email))
                return "Bu email artıq istifadə olunub.";

            if (!IsValidPassword(password))
                return "Password minimum 8 simvol, 1 böyük və 1 kiçik hərf olmalıdır.";

            User user = new User
            {
                Id = new Random().Next(1000, 9999),
                Name = name,
                Surname = surname,
                Email = email,
                Password = password,
                Balance = 0,
                IsBlocked = false,
                IsAdmin = isAdmin,
                IsLogged = false
            };

            _bankService.AddUser(user);

            return "Qeydiyyat uğurla tamamlandı.";
        }

        public string Login(string email, string password)
        {
            var user = _bankService.FindUser(email);

            if (user == null)
                return "User tapılmadı.";

            if (user.IsBlocked)
                return "Bu user sistemdən blok edilib.";

            if (user.Password != password)
                return "Şifrə yanlışdır.";

            user.IsLogged = true;
            _currentUser = user;
            return $"Xoş gəldin, {user.Name}";
        }

        public void Logout()
        {
            if (_currentUser != null)
                _currentUser.IsLogged = false;

            _currentUser = null;
        }

        public double CheckBalance()
        {
            return _currentUser.Balance;
        }

        public double TopUp(double amount)
        {
            _currentUser.Balance += amount;
            return _currentUser.Balance;
        }

        public string ChangePassword(string newPass)
        {
            if (!IsValidPassword(newPass))
                return "Şifrə tələblərə uyğun deyil!";

            _currentUser.Password = newPass;
            return "Şifrə yeniləndi.";
        }

        private bool IsValidPassword(string pass)
        {
            return pass.Length >= 8 &&
                   pass.Any(char.IsLower) &&
                   pass.Any(char.IsUpper);
        }
    }

}
