using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pr1
{
    internal class Account
    {
        private decimal _balance;
        private List<INotifyer> _notifyers;


        public Account()
        {
            _balance = 0;
            _notifyers = [];
        }
        public Account(decimal balance) { 
            _balance = balance;
            _notifyers = [];
        }
        public void AddNotifyer(INotifyer notifyer)
        {
            _notifyers.Add(notifyer);
        }
        public void ChangeBalance(decimal balance)
        {
            _balance += balance;
            Notification();
        }
        public decimal Balance()
        {
            return _balance;
        }
        public void Notification()
        {
            for(int i = 0; i < _notifyers.Count; i++)
            {
                _notifyers[i].Notify(_balance);
            }
        }

    }
}
