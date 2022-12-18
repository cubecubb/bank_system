using Bank;
using bank_system.Model;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace bank_system
{
    /// <summary>
    /// Логика взаимодействия для authoriz.xaml
    /// </summary>
    public partial class authoriz : Window
    {
        public authoriz()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            string login = txb_login.Text;
            string password = txt_pass.Text;
            Helper helper = new Helper();
            modulesEntities1 modulesEntities = Helper.GetContext();
            if (login == "")
            {
                if (password == "")
                {
                    MessageBox.Show("Введите логин и пароль.");
                }
                else
                {
                    MessageBox.Show("Введите логин.");
                }
            }
            else if (password == "")
            {
                MessageBox.Show("Введите пароль");
            }
            else
            {
                string result = helper.FindUsers(login, password);
                MessageBox.Show(result);
            }
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
