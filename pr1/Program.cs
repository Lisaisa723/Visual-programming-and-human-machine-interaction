using pr1;

class Program
{
    public static void Main(string[] args)
    {
        Account account = new Account();
        account.AddNotifyer(new SMSLowBalanceNotifyer("+79875643223", 1000));
        account.AddNotifyer(new EMailBalanceChangedNotifyer("VladimirIvanov2004@gmail.com"));

        account.ChangeBalance(12000);
        account.ChangeBalance(-4000);
        account.ChangeBalance(-7500);


    }
}