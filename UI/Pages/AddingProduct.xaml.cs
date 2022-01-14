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
    /// Interaction logic for AddingProduct.xaml
    /// </summary>
    public partial class AddingProduct : Page
    {
        Database datab = new Database();
        Appp app = new Appp();
        public AddingProduct()
        {
            InitializeComponent();
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            if (app.IfProductExists(product_name.Text) == false)
            {
                app.AddRecipeProducts(product_name.Text, quantity_for_1.Text);
            }
            else if (app.IfProductExists(product_name.Text) == true)
            {
                app.AddOnlyRecipeProduct(product_name.Text, quantity_for_1.Text);
            }
            var ClickedButton = e.OriginalSource as AddButton;
            NavigationService.Navigate(ClickedButton.NavUri);
        }

        private void Grid_Click_1(object sender, RoutedEventArgs e)
        {
            var ClickedButton = e.OriginalSource as NavButton;
            NavigationService.Navigate(ClickedButton.NavUri);
        }
    }
}
