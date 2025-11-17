using static Login_Bank_Account.AccountS;
using static Login_Bank_Account.BankS;

namespace Login_Bank_Account;

public class MenuS
{
    public class MenuService
    {
        private AccountService _accountService;
        private BankService _bankService;

        public MenuService(AccountService accountService, BankService bankService)
        {
            _accountService = accountService;
            _bankService = bankService;
        }

        public void Start()
        {
            while (true)
            {
                if (_accountService.CurrentUser == null)
                {
                    ShowGuestMenu();
                }
                else
                {
                    ShowUserMenu();
                }
            }
        }

        private void ShowGuestMenu()
        {
            Console.WriteLine("\n1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Find User");
            Console.WriteLine("0. Exit");

            Console.Write("Seçim: ");
            var key = Console.ReadLine();

            switch (key)
            {
                case "1":
                    RegisterMenu();
                    break;
                case "2":
                    LoginMenu();
                    break;
                case "3":
                    FindUserMenu();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
            }
        }

        private void RegisterMenu()
        {
            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Surname: ");
            string surname = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Password: ");
            string pass = Console.ReadLine();

            Console.Write("Is Admin? (true/false): ");
            bool isAdmin = bool.Parse(Console.ReadLine());

            Console.WriteLine(_accountService.Register(name, surname, email, pass, isAdmin));
        }

        private void LoginMenu()
        {
            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Password: ");
            string pass = Console.ReadLine();

            Console.WriteLine(_accountService.Login(email, pass));
        }

        private void FindUserMenu()
        {
            Console.Write("Email daxil edin: ");
            string email = Console.ReadLine();

            var user = _bankService.FindUser(email);

            if (user == null)
                Console.WriteLine("User tapılmadı.");
            else
                Console.WriteLine($"{user.Name} {user.Surname}");
        }

        private void ShowUserMenu()
        {
            var user = _accountService.CurrentUser;

            Console.WriteLine($"\n*** Xoş gəldin {user.Name}! ***");
            Console.WriteLine("1. Check Balance");
            Console.WriteLine("2. Top up balance");
            Console.WriteLine("3. Change password");

            if (user.IsAdmin)
            {
                Console.WriteLine("4. Bank user list");
                Console.WriteLine("5. Block user");
            }

            Console.WriteLine("0. Log out");

            Console.Write("Seçim: ");
            var key = Console.ReadLine();

            switch (key)
            {
                case "1":
                    Console.WriteLine("Balans: " + _accountService.CheckBalance());
                    break;

                case "2":
                    Console.Write("Məbləğ daxil edin: ");
                    double amount = double.Parse(Console.ReadLine());
                    Console.WriteLine("Yeni balans: " + _accountService.TopUp(amount));
                    break;

                case "3":
                    Console.Write("Yeni şifrə: ");
                    string pass = Console.ReadLine();
                    Console.WriteLine(_accountService.ChangePassword(pass));
                    break;

                case "4":
                    if (user.IsAdmin)
                        foreach (var u in _bankService.GetAllUsers())
                            Console.WriteLine($"{u.Name} {u.Surname}");
                    break;

                case "5":
                    if (user.IsAdmin)
                    {
                        Console.Write("Email: ");
                        string email = Console.ReadLine();
                        var u = _bankService.FindUser(email);

                        if (u != null)
                        {
                            u.IsBlocked = true;
                            Console.WriteLine("User blok edildi.");
                        }
                        else Console.WriteLine("User tapılmadı.");
                    }
                    break;

                case "0":
                    _accountService.Logout();
                    break;
            }
        }
    }

}
