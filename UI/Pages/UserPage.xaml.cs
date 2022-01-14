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

namespace UI.Pages
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        Appp app = new Appp();
        public UserPage()
        {
            InitializeComponent();
            List<string> info = app.GetInfo();
            first_name.Text += " " + info[0];
            last_name.Text += " " + info[1];
            email.Text += " " + info[2];
            gender.Text += " " + info[3];
            phone_number.Text += " " + info[4];
            location.Text += " " + info[5];
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            var ClickedButton = e.OriginalSource as NavButton;
            NavigationService.Navigate(ClickedButton.NavUri);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //display a new MainWindow
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            //close Window1
            UserWindow w = Application.Current.Windows.OfType<UserWindow>().FirstOrDefault();
            w.Close();
        }

        private void Grid_Click_1(object sender, RoutedEventArgs e)
        {
            var ClickedButton = e.OriginalSource as AddButton;
            NavigationService.Navigate(ClickedButton.NavUri);
        }
    }
}
