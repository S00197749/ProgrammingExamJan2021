using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public abstract class Account
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Balance { get; set; }
        public DateTime InterestDate { get; set; }
        public Account(string firstname, string lastname, decimal balance)
        {
            FirstName = firstname;
            LastName = lastname;
            Balance = balance;
        }
        public void Deposit(decimal input)
        {
            Balance += input;
        }
        public void Withdraw(decimal input)
        {
            Balance -= input;
        }
        public abstract decimal CalculateInterest();
        public override string ToString()
        {
            return string.Format($"{LastName}, {FirstName}");
        }

    }
    public class CurrentAccount : Account
    {
        public decimal InterestRate { get; set; }
        public CurrentAccount(string firstname, string lastname, decimal balance): base(firstname, lastname, balance)
        {
            InterestRate = 0.03m;
        }
        public override decimal CalculateInterest()
        {
            DateTime date = DateTime.Today;
            InterestDate = date;
            return Balance * InterestRate;
        }
    }
    public class SavingsAccount: Account
    {
        public decimal InterestRate { get; set; }
        public SavingsAccount(string firstname, string lastname, decimal balance) : base(firstname, lastname, balance)
        {
            InterestRate = 0.06m;
        }
        public override decimal CalculateInterest()
        {
            DateTime date = DateTime.Today;
            InterestDate = date;
            return Balance * InterestRate;
        }
    }
}
