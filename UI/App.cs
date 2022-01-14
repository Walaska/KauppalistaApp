using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace UI
{
    class Appp
    {
        private static string recipe;
        Database datab = new Database();
        
        public void FirstCheck(string firstname, string lastname, string email, string password)
        {
            datab.AddUser(firstname, lastname, email, password);
        }
        public void AddRecipe(string recipe_name, string description, string portions, string skill)
        {
            datab.AddRecipe(recipe_name, description, portions, skill);
            recipe = recipe_name;
        }
        public void AddRecipeProducts(string product, string quantity)
        {
            datab.AddProduct(product);
            datab.AddStorage(datab.GetProductId(product), product, "0");
            datab.AddRecipeIngrets(datab.GetProductId(product), quantity, datab.GetRecipeId(recipe));
        }
        public void AddOnlyRecipeProduct(string product, string quantity)
        {
            datab.AddRecipeIngrets(datab.GetProductId(product), quantity, datab.GetRecipeId(recipe));
        }
        public void AddProductAndStorage(string product_name)
        {
            datab.AddProduct(product_name);
            datab.AddStorage(datab.GetProductId(product_name), product_name, "0");
        }
        public List<string> GetRecipes()
        {
            return datab.GetRecipes();
        }
        public void AddInfo(string gender, string number, string location)
        {
            datab.AddGender(gender);
            datab.AddNumber(number);
            datab.AddLocation(location);
        }
        public List<string> GetInfo()
        {
            return datab.GetInfo();
        }
        public List<string> PrintShoppingProducts(string recipe)
        {
            int portions = datab.GetRecipePortions(recipe);
            int i = 0;
            List<int> recipeP = datab.GetRecipeProductQ(recipe, datab.GetRecipeId(recipe));
            List<int> storageP = datab.GetStoragePQForRecipe(datab.GetRecipeIngProductId(datab.GetRecipeId(recipe)));
            List<string> products = datab.ProductNameRecipe(datab.GetRecipeIngProductId(datab.GetRecipeId(recipe)));
            List<string> productsToReturn = new List<string>();
            foreach (int sp in storageP)
            {
                if (sp < (recipeP[i] * portions))
                {
                    productsToReturn.Add(products[i] + " " + ((recipeP[i] * portions) - sp) + "x");
                    i++;
                }
                else if (sp >= (recipeP[i] * portions))
                {
                    i++;
                }
            }
            return productsToReturn;
        }
        public void DeleteFromShoppingList(string product_name)
        {
            datab.DeleteShopList(datab.GetProductId(product_name));
        }
        public List<string> PrintShoppingList()
        {
            return datab.GetShoppingListInfo();
        }
        public void AddQuantityToShoppingList(string product_name, string quantity)
        {
            datab.AddShoppingListQuantity(datab.GetProductId(product_name), quantity);
        }
        public bool CheckProductsInShoppingList(string product_name)
        {
            List<string> product_id = datab.GetShoppingListProdIds();
            foreach (string product in product_id)
            {
                if(datab.GetProductName(product) == product_name)
                {
                    return true;
                }
            }
            return false;
        }
        public void AddShoppingList(string quantity, string product_name)
        {
            datab.NewShoppinglist(quantity, datab.GetProductId(product_name));
        }
        public void ConsumeRecipe(string recipe)
        {
            int z = 0;
            List<int> recipePQ = datab.GetRecipeProductQ(recipe, datab.GetRecipeId(recipe));
            List<string> products = datab.ProductNameRecipe(datab.GetRecipeIngProductId(datab.GetRecipeId(recipe)));
            foreach (int pq in recipePQ)
            {
                datab.ConsumeRecipe(pq, products[z], datab.GetRecipePortions(recipe));
                z++;
            }
        }
        public string GetPortions(string recipe)
        {
            return datab.GetRecipePortions(recipe).ToString();
        }
        public string GetSkillLvl(string recipe)
        {
           return datab.GetSkillLvl(recipe);
        }
        public string GetDescription(string recipe)
        {
            return datab.GetDescription(recipe);
        }
        public List<string> GetRecipeProducts(string recipe)
        {
            int x = 0;
            List<string> prodName = datab.ProductNameRecipe(datab.GetRecipeIngProductId(datab.GetRecipeId(recipe)));
            List<int> prodQ = datab.GetRecipeProductQ(recipe, datab.GetRecipeId(recipe));
            List<string> whatToReturn = new List<string>();
            foreach(string prod in prodName)
            {
                whatToReturn.Add(prod + " " + prodQ[x] + "x");
                x++;
            }
            return whatToReturn;
        }
        public List<string> SearchStorageProduct(string product_name)
        {
            return datab.SearchStorageProduct(product_name);
        }
        public List<string> SearchRecipe(string recipe_name)
        {
            return datab.SearchRecipe(recipe_name);
        }
        public List<string> PrintStorage()
        {
            return datab.PrintStorage();
        }
        public void AddToStorageFromShoppingList(string quantity, string product)
        {
            datab.UpdateStorage(quantity, product);
        }
        public void RemoveFromStorage(string productName)
        {
            datab.RemoveFromStorage(productName);
        }
        public bool IfRecipeExists(string recipe_name)
        {
            return datab.IfRecipeExist(recipe_name);
        }
        public bool IfProductExists(string product_name)
        {
            return datab.IfProductExist(product_name);
        }
    }
}
