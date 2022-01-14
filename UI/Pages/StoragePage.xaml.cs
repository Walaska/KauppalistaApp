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
    /// Interaction logic for StoragePage.xaml
    /// </summary>
    public partial class StoragePage : Page
    {
        Appp app = new Appp();
        public StoragePage()
        {
            InitializeComponent();
            products.ItemsSource = app.PrintStorage();
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            var ClickedButton = e.OriginalSource as NavButton;




            NavigationService.Navigate(ClickedButton.NavUri);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string newItem;
            string item = products.SelectedItem.ToString();
            int idx = item.LastIndexOf(" ");
            if (idx > -1)
            {
                newItem = item.Remove(idx);
            }
            else
            {
                newItem = item;
            }
            app.RemoveFromStorage(newItem);
            products.ItemsSource = app.PrintStorage();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            List<string> x;
            if (app.IfProductExists(search.Text) == true)
            {
                x = app.SearchStorageProduct(search.Text);
                products.ItemsSource = x;
                search.Text = "";
            }
            else if (search.Text == "")
            {
                products.ItemsSource = app.PrintStorage();
            }
            else if (app.IfProductExists(search.Text) == false)
            {
                MessageBox.Show("Product not found!");
                search.Text = "";
            }
        }
    }
}
