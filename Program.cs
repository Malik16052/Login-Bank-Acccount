using Login_Bank_Account;
using static Login_Bank_Account.AccountS;
using static Login_Bank_Account.BankS;
using static Login_Bank_Account.MenuS;

class Program
{
    static void Main()
    {
        Bank bank = new Bank { Id = 1 };
        BankService bankService = new BankService(bank);
        AccountService accountService = new AccountService(bankService);
        MenuService menuService = new MenuService(accountService, bankService);

        menuService.Start();
    }
}
