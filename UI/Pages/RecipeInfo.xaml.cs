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
    /// Interaction logic for RecipeInfo.xaml
    /// </summary>
    public partial class RecipeInfo : Page
    {
       Recipe r = new Recipe();
       Appp app = new Appp();
        public RecipeInfo()
        {
            InitializeComponent();
            recipe_name.Text = r.getRecipeName();
            portion.Text += " " + app.GetPortions(recipe_name.Text);
            skill.Text += " " + app.GetSkillLvl(recipe_name.Text);
            description.Text += " " + app.GetDescription(recipe_name.Text);
            List<string> prods = app.GetRecipeProducts(recipe_name.Text);
            products.Text += "\n";
            foreach(string prod in prods)
            {
                products.Text += prod + "\n";
            }
        }
        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            var ClickedButton = e.OriginalSource as NavButton;
            NavigationService.Navigate(ClickedButton.NavUri);
        }
    }
}
