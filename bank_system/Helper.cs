using bank_system.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank_system
{
    public class Helper
    {
        private static modulesEntities1 modulesEntities;
        public static modulesEntities1 GetContext()
        {
            if (modulesEntities == null)
            {
                modulesEntities = new modulesEntities1();
            }
            return modulesEntities;
        }

        public int FindUsers(string Login, string Password)
        {
            var User = modulesEntities.User.Where(x => x.Login == Login).FirstOrDefault();
            if (User == null)
            {
                return -1;
            }
            else
            {
                return User.IDUser;
            }
        }

        public string LastBankAccount(int IDUser, DateTime date, float balance, int type)
        {
            var bankAccount = modulesEntities.BankAccount.OrderByDescending(x => x.UserID == IDUser && x.DateOpen == date && x.Balance == balance && x.TypeID == type).FirstOrDefault();
            return bankAccount.NumberAccount;
        }
        public int LastContract(string numberAccount, int userID, int amount, int period, DateTime date, float percet)
        {
            var contract = modulesEntities.Contract.OrderByDescending(x => x.NumberAccount == numberAccount && x.UserID == userID && x.Amount == amount && x.Period == period && x.ExpirationDate == date && x.Percent == percet).FirstOrDefault();
            return contract.IDContract;
        }

        public int CreateContract(int IDUser, int amount, int period, double percet)
        {
            BankAccount bankAccount = new BankAccount();
            bankAccount.UserID = IDUser;
            DateTime date = DateTime.Today;
            bankAccount.DateOpen = date;
            bankAccount.Balance = amount;
            bankAccount.TypeID = 3;
            modulesEntities.BankAccount.Add(bankAccount);
            modulesEntities.SaveChanges();
            string number_account = LastBankAccount(IDUser, date, amount, 3);
            Contract contract = new Contract();
            contract.NumberAccount = number_account;
            contract.UserID = IDUser;
            contract.Amount = amount;
            contract.Period = period;
            contract.Percent = percet;
            DateTime date1 = DateTime.Today.AddMonths(period);
            contract.ExpirationDate = date1;
            modulesEntities.Contract.Add(contract);
            modulesEntities.SaveChanges();
            int IDContract = LastContract(number_account, IDUser, amount, period, date1, (float)percet);
            return IDContract;
        }

        public User FindUser(int IdUser)
        {
            var user = modulesEntities.User.Where(x => x.IDUser == IdUser).FirstOrDefault();
            return user;
        }
        public BankAccount FindBankAccount(string number_account)
        {
            var bankAccount = modulesEntities.BankAccount.Where(x => x.NumberAccount == number_account).FirstOrDefault();
            return bankAccount;
        }
        public Contract FindContract(int IDContract)
        {
            var contract = modulesEntities.Contract.Where(x => x.IDContract == IDContract).FirstOrDefault();
            return contract;
        }
    }
}
