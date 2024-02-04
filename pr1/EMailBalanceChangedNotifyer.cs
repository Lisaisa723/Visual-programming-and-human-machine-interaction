using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pr1
{
    internal class EMailBalanceChangedNotifyer : INotifyer
    {
        private string _email;

        public EMailBalanceChangedNotifyer(string email)
        {
            _email = email;
        }

        public void Notify(decimal balance)
        {
            Console.WriteLine(this);
            Console.WriteLine(balance);
        }
    }
}
