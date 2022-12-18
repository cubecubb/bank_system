using Bank;
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

namespace bank_app
{
    /// <summary>
    /// Логика взаимодействия для main_window.xaml
    /// </summary>
    public partial class main_window : Window
    {
        public main_window()
        {
            InitializeComponent();
        }

        private void btn_perehod_Click(object sender, RoutedEventArgs e)
        {
            Calculator calculator = new Calculator();
            calculator.Show();
            this.Close();
        }
    }
}
