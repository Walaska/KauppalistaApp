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
    /// Interaction logic for ShoppingList.xaml
    /// </summary>
    public partial class ShoppingList : Page
    {
        Recipe r = new Recipe();
        Appp app = new Appp();
        static List<string> products;
        static int i = 0;
        public ShoppingList()
        {
            InitializeComponent();
            products = r.getValues();                       
            shoplist.ItemsSource = products;
        }

       private void Grid_Click(object sender, RoutedEventArgs e)
       {
            var ClickedButton = e.OriginalSource as NavButton;




          NavigationService.Navigate(ClickedButton.NavUri);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
 
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string item = shoplist.SelectedItem.ToString();
            string newItem;
            int idx = item.LastIndexOf(" ");
            if (idx > -1)
            {
                newItem = item.Remove(idx);
            }
            else
            {
                newItem = item;
            }
            app.DeleteFromShoppingList(newItem);
            r.RefreshShopList();
            products = r.getValues();
            shoplist.ItemsSource = products;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            bool x = app.IfProductExists(product.Text);
            if (x == false)
            {
                app.AddProductAndStorage(product.Text);
                app.AddShoppingList(quantity.Text, product.Text);
            }
            else
            {
                if (app.CheckProductsInShoppingList(product.Text) == false)
                {
                    app.AddShoppingList(quantity.Text, product.Text);
                }
                else if (app.CheckProductsInShoppingList(product.Text) == true)
                {
                    app.AddQuantityToShoppingList(product.Text, quantity.Text);
                }
            }
            product.Text = "";
            quantity.Text = "";
            r.RefreshShopList();
            products = r.getValues();
            shoplist.ItemsSource = products;
            i++;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            string newString;
            string newQuantity;
            string item = shoplist.SelectedItem.ToString();
            string q = shoplist.SelectedItem.ToString();
            int qua_idx = q.LastIndexOf(" ") + 1;
            int idx = item.LastIndexOf(" ");
            if (idx > -1)
            {
                newString = item.Remove(idx);
            }
            else
            {
                newString = item;
            }
            if (qua_idx > -1)
            {
                newQuantity = q.Substring(qua_idx);
            }
            else
            {
                newQuantity = q;
            }
            newQuantity = newQuantity.Remove(newQuantity.Length - 1);
            app.AddToStorageFromShoppingList(newQuantity, newString);
            app.DeleteFromShoppingList(newString);
            products = r.getValues();
            shoplist.ItemsSource = products;
        }
    }
}
