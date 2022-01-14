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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Database datab = new Database();
        public MainWindow()
        {
            InitializeComponent();
            datab.DbConnect();
        }

        private void BtnClickSignUp(object sender, RoutedEventArgs e)
        {
            Hide();
            Register win = new Register();
            win.ShowDialog();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (datab.CheckUser(email.Text, password.Password) == false)
            {
                MessageBox.Show("Password or email is not correct!");
            }
            else
            {
                datab.GetId(email.Text, password.Password);
                Hide();
                UserWindow win = new UserWindow();
                win.ShowDialog();
            }
        }
    }
}
