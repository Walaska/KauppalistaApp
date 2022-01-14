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
    /// Interaction logic for AddingRecipe.xaml
    /// </summary>
    public partial class AddingRecipe : Page
    {
        private static string recipe;
        private static string desc;
        private static string portions;
        private static string skill;
        private static int i;
        Database datab = new Database();
        Appp app = new Appp();
        public AddingRecipe()
        {
            InitializeComponent();
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            recipe = recipe_name.Text;
            desc = description.Text;
            portions = portion.Text;
            skill = Skill.Text;
            if (i == 0)
            {
                app.AddRecipe(recipe_name.Text, description.Text, portion.Text, Skill.Text);
                i++;
            }
            else
            {
                recipe_name.Text = recipe;
                description.Text = desc;
                portion.Text = portions;
                Skill.Text = skill;
            }
            var ClickedButton = e.OriginalSource as NavButton;
            NavigationService.Navigate(ClickedButton.NavUri);
        }

        private void Grid_Click_1(object sender, RoutedEventArgs e)
        {
            i = 0;
            var ClickedButton = e.OriginalSource as AddButton;
            NavigationService.Navigate(ClickedButton.NavUri);
        }

        private void Grid_Click_2(object sender, RoutedEventArgs e)
        {
            var ClickedButton = e.OriginalSource as NavButton;
            NavigationService.Navigate(ClickedButton.NavUri);
        }
    }
}
