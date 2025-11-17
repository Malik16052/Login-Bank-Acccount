namespace Login_Bank_Account;

public class BankS
{
    public class BankService
    {
        private Bank _bank;

        public BankService(Bank bank)
        {
            _bank = bank;
        }

        public void AddUser(User user)
        {
            _bank.Users.Add(user);
        }

        public User FindUser(string email)
        {
            return _bank.Users.FirstOrDefault(u => u.Email == email);
        }

        public bool IsEmailTaken(string email)
        {
            return _bank.Users.Any(u => u.Email == email);
        }

        public List<User> GetAllUsers()
        {
            return _bank.Users;
        }
    }

}
