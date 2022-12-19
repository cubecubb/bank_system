using Bank;
using bank_system.Model;
using System;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;
using Word = Microsoft.Office.Interop.Word;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;

namespace bank_system
{
    /// <summary>
    /// Логика взаимодействия для authoriz.xaml
    /// </summary>
    public partial class authoriz : Window
    {
        int amount = 1000;
        int period = 1;
        double percet = 8;

        public authoriz(int amount, int period, double percet)
        {
            InitializeComponent();

        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            string login = txb_login.Text;
            string password = txt_pass.Text;
            Helper helper = new Helper();
            modulesEntities1 db = Helper.GetContext();
            if (login == "")
            {
                if (password == "") 
                    MessageBox.Show ("Введите логин и пароль.");
                else MessageBox.Show("Введите логин.");
            }
            else
            {
                if (password == "")
                {
                    MessageBox.Show("Введите пароль.");
                }
                else
                {
                    int IDUser = helper.FindUsers(login, password);
                    if (IDUser > 0)
                    {
                        int IDContract = helper.CreateContract(IDUser, amount, period, percet);
                        Contract contract = helper.FindContract(IDContract);
                        CreateWord(IDUser, contract.NumberAccount, IDContract);
                    }
                    else
                    {
                        MessageBox.Show("Такого пользователя не существует.");
                    }
                }
            }
        }

        private void CreateWord(int IDUser, string IDBankAccount, int IdContract)
        {
            Helper helper = new Helper();
            modulesEntities1 db = Helper.GetContext();
            User user = helper.FindUser(IDUser);
            BankAccount bankAccount = helper.FindBankAccount(IDBankAccount);
            Contract contract = helper.FindContract(IdContract);
            string TemplateFileName = @"C:\Users\msi gf65\source\repos\bank_system\shablon.docx";
            var wordApp = new Word.Application();
            wordApp.Visible = false;

            var wordDocument = wordApp.Documents.Open(TemplateFileName);
            ReplaceWordStub("<NUMBER>", contract.IDContract.ToString(), wordDocument);
            ReplaceWordStub("<DAY>", DateTime.Today.ToString("dd"), wordDocument);
            ReplaceWordStub("<MONTH>", DateTime.Today.ToString("MM"), wordDocument);
            ReplaceWordStub("<TWO>", DateTime.Today.ToString("yy"), wordDocument);
            ReplaceWordStub("<FIO>", user.Surname + " " + user.Name + " " + user.Patronymic, wordDocument);
            ReplaceWordStub("<FIO1>", user.Surname + " " + user.Name + " " + user.Patronymic, wordDocument);
            ReplaceWordStub("<SUM>", contract.Amount.ToString(), wordDocument);
            ReplaceWordStub("<PERIOD>", contract.Period.ToString(), wordDocument);
            ReplaceWordStub("END", contract.ExpirationDate.ToString("dd/MM/yyyy"), wordDocument);
            ReplaceWordStub("<PERCENT>", contract.Percent.ToString(), wordDocument);
            ReplaceWordStub("<NUMBER#2>", bankAccount.NumberAccount.ToString(), wordDocument);
            ReplaceWordStub("<ADRESSREG>", user.Adress.ToString(), wordDocument);
            ReplaceWordStub("<EMAIL>", user.E_Mail.ToString(), wordDocument);
            ReplaceWordStub("<SERIES>", user.Series.ToString(), wordDocument);
            ReplaceWordStub("<NUMS>", user.Number.ToString(), wordDocument);
            ReplaceWordStub("<WHENWHO>", user.Issued.ToString() + " " + user.DateOfIssue.ToString(), wordDocument);
            ReplaceWordStub("<DATEOFBIRTH>", user.DateOfBirth.ToString("dd/MM/yyyy"), wordDocument);
            ReplaceWordStub("<PLACEOFBIRTH>", user.PlaceOfBirth.ToString(), wordDocument);
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                wordDocument.SaveAs(@"C:\Users\msi gf65\source\repos\bank_system\dogovor.docx");
            }
            wordDocument.Close();
        }

        private void ReplaceWordStub(string stubToReplace, string text, Word.Document wordDocument)
        {
            var range = wordDocument.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text);
        }
        private void txb_login_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txb_login_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void txb_login_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void txt_pass_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
