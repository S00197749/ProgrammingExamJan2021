using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //create one list and one ObservableCollection of type Account
        List<Account> accounts = new List<Account>();
        ObservableCollection<Account> filteredAccounts = new ObservableCollection<Account>();
        public MainWindow()
        {
            InitializeComponent();
            //makes both checkboxes checked upon initialization
            cboxCurrentAccount.IsChecked = true;
            cboxSavingsAccount.IsChecked = true;

            //create objects
            CurrentAccount acc1 = new CurrentAccount("John", "Doe", 100000);
            CurrentAccount acc2 = new CurrentAccount("Adam", "Apple", 200000);
            SavingsAccount acc3 = new SavingsAccount("Sarah", "Farrel", 15000);
            SavingsAccount acc4 = new SavingsAccount("Darragh", "Weir", 20000);

            //add objects to list
            accounts.Add(acc1);
            accounts.Add(acc2);
            accounts.Add(acc3);
            accounts.Add(acc4);

        }

        private void tblkTransactionAmount_GotFocus(object sender, RoutedEventArgs e)
        {
            tbxTransactionAmount.Clear();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //clear tbx each time a new account is selected
            tbxTransactionAmount.Clear();
            tblkInterest.Text = "";
            tblkInterestDate.Text = "";

            //check which account is selected
            Account selectedAccount = lbxAccounts.SelectedItem as Account;

            //check to see if there is an account selected
            if (selectedAccount != null)
            {
                //display account's name and balance
                tblkFirstName.Text = selectedAccount.FirstName;
                tblkLastName.Text = selectedAccount.LastName;
                tblkBalance.Text = selectedAccount.Balance.ToString();
                //tblkInterestDate = selectedAccount.InterestDate;

                //check if selected account is Current or Savings
                if (selectedAccount is CurrentAccount)
                {
                    //puts selected account in new account of correct derived class (CurrentAccount) so you can call the correct properties (InterestRate)
                    CurrentAccount selectedCurrentAccount = selectedAccount as CurrentAccount;
                    //displays Account Type 
                    tblkAccountType.Text = "Current Account";
                    //displays accounts monthly pay by calling the class method
                    //tblkCalcMonthlyPay.Text = ("€" + selectedFTEmployee.CalculateMonthlyPay().ToString("F"));
                }
                else if (selectedAccount is SavingsAccount)
                {
                    //puts selected account in new account of correct derived class (SavingsAccount) so you can call the correct properties (InterestRate)
                    SavingsAccount selectedSavingsAccount = selectedAccount as SavingsAccount;
                    //displays Account Type 
                    tblkAccountType.Text = "Savings Account";
                    //displays employees monthly pay by calling the class method
                    //tblkCalcMonthlyPay.Text = ("€" + selectedPTEmployee.CalculateMonthlyPay().ToString("F"));
                }
            }
        }

        private void cboxCurrentAccount_Checked(object sender, RoutedEventArgs e)
        {
            //clears the ObservableCollection
            filteredAccounts.Clear();

            //check if both check boxes are checked, if not, move onto else statement
            if (cboxCurrentAccount.IsChecked == true && cboxSavingsAccount.IsChecked == true)
            {
                //display accounts in listbox
                lbxAccounts.ItemsSource = accounts;
            }
            else
            {
                //check if Current is checked and Savings is not checked
                if (cboxCurrentAccount.IsChecked == true && cboxSavingsAccount.IsChecked == false)
                {
                    //loop through all the accounts in accounts list
                    foreach (Account acc in accounts)
                    {
                        //check if account is a CurrentAccount
                        if (acc is CurrentAccount)
                        {
                            //add CurrentAccount accounts to ObservableCollection
                            filteredAccounts.Add(acc);
                        }
                    }
                }
                //check if Savings is checked and Current is not checked
                else if (cboxSavingsAccount.IsChecked == true && cboxCurrentAccount.IsChecked == false)
                {
                    //loop through all the accounts in accounts list
                    foreach (Account acc in accounts)
                    {
                        //check if account is a SavingsAccount
                        if (acc is SavingsAccount)
                        {
                            //add SavingsAccount accounts to ObservableCollection
                            filteredAccounts.Add(acc);
                        }
                    }
                }
                //display filterdAccounts
                lbxAccounts.ItemsSource = filteredAccounts;
            }
        }

        private void btnDeposit_Click(object sender, RoutedEventArgs e)
        {
            //check which account was selected
            Account selectedAccount = lbxAccounts.SelectedItem as Account;

            //call deposit method
            selectedAccount.Deposit(decimal.Parse(tbxTransactionAmount.Text));

            //update display manually
            tblkBalance.Text = "";
            tblkBalance.Text = selectedAccount.Balance.ToString();
        }

        private void btnWithdraw_Click(object sender, RoutedEventArgs e)
        {
            //check which account was selected
            Account selectedAccount = lbxAccounts.SelectedItem as Account;

            //call deposit method
            selectedAccount.Withdraw(decimal.Parse(tbxTransactionAmount.Text));

            //update display manually
            tblkBalance.Text = "";
            tblkBalance.Text = selectedAccount.Balance.ToString();
        }

        private void btnInterest_Click(object sender, RoutedEventArgs e)
        {
            //check which account was selected
            Account selectedAccount = lbxAccounts.SelectedItem as Account;

            //check if selected account is Current or Savings
            if (selectedAccount is CurrentAccount)
            {
                //puts selected account in new account of correct derived class (CurrentAccount) so you can call the correct properties (InterestRate)
                CurrentAccount selectedCurrentAccount = selectedAccount as CurrentAccount;
                tblkInterest.Text = selectedCurrentAccount.CalculateInterest().ToString();
            }
            else if (selectedAccount is SavingsAccount)
            {
                //puts selected account in new account of correct derived class (SavingsAccount) so you can call the correct properties (InterestRate)
                SavingsAccount selectedSavingsAccount = selectedAccount as SavingsAccount;
                tblkInterest.Text = selectedSavingsAccount.CalculateInterest().ToString();
            }
        }
    }
}
