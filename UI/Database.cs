using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace UI
{
    class Database
    {
        public static string id;
        public static OleDbConnection dbcon = new OleDbConnection();
        public void DbConnect()
        {
            string pPath = @"C:\Users\Kalle\Desktop\UI\UI";
            dbcon.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0;" + @"Data Source = " + pPath + @"\AppDatabase.accdb;";
            dbcon.Open();
        }
        public void DbClose()
        {
            dbcon.Close();
        }
        private void AddToDatabase(string where, string ins, string val)
        {
            OleDbCommand addToData = new OleDbCommand();
            addToData.Connection = dbcon;
            addToData.CommandText = "INSERT INTO " + where + "(" + ins + ") VALUES (" + val + ")";
            addToData.ExecuteNonQuery();
        }
        private OleDbDataReader GetFromDatabase(string from, string what, string where)
        {
            OleDbCommand getFromDb = new OleDbCommand();
            OleDbDataReader reader;
            getFromDb.Connection = dbcon;
            getFromDb.CommandText = "SELECT " + what + " FROM " + from + " WHERE " + where;
            getFromDb.CommandType = CommandType.Text;
            reader = getFromDb.ExecuteReader();
            return reader;
        }
        private void UpdateData(string what, string set, string where)
        {
            OleDbCommand updateData = new OleDbCommand();
            updateData.Connection = dbcon;
            updateData.CommandText = "UPDATE " + what + " SET " + set + " WHERE " + where;
            updateData.ExecuteNonQuery();
        }
        public void DeleteShopList(string product_id)
        {
            OleDbCommand delete = new OleDbCommand();
            delete.Connection = dbcon;
            delete.CommandText = "DELETE FROM shopping_list WHERE user_id = " + id + " AND product_id = " + product_id;
            delete.ExecuteNonQuery();
        }
        public void AddUser(string fname, string lname, string email, string password)
        {
            string val = "'" + fname + "', '" + lname + "','" + email + "','" + password + "','-','-','-'";
            AddToDatabase("Userx", "first_name, last_name, email_add, passwrd, gender, numba, location_", val);
        }
        public void AddRecipe(string recipe, string desc, string port, string skill)
        {
            string val = "'" + recipe + "','" + desc + "','" + port + "','" + skill + "', " + id.ToString();
            AddToDatabase("recipe", "recipe_name, description, portions, skill, user_id", val);
        }
        public void AddRecipeIngrets(string product_id, string quantity, string recipe_id)
        {
            string val = "'" + product_id + "','" + quantity + "'," + id + ",'" + recipe_id + "'";
            AddToDatabase("recipe_ingredients", "product_id, quantity, user_id, recipe_id", val);
        }
        public void AddProduct(string product_name)
        {
            string val = "'" + product_name + "'," + id;
            AddToDatabase("product", "product_name, user_id", val);
        }
        public void UpdateStorage(string newQuantity, string productName)
        {
            string set = "available_quantity = available_quantity + '" + newQuantity + "'";
            string val = "user_id = " + id + " AND product_name = '" + productName + "'";
            UpdateData("storage", set, val);
        }
        public void RemoveFromStorage(string productName)
        {
            string val = "user_id = " + id + " AND product_name = '" + productName + "'";
            UpdateData("storage", "available_quantity = 0", val);
        }
        public void AddStorage(string product_id, string product_name, string quantity)
        {
            string val = product_id + ",'" + product_name + "'," + id + "," + quantity;
            AddToDatabase("storage", "product_id, product_name, user_id, available_quantity", val);
        }
        public void AddGender(string gender)
        {
            string set = "gender = '" + gender + "'";
            string val = "user_id = " + id;
            UpdateData("Userx", set, val);
        }
        public void AddNumber(string number)
        {
            string set = "numba = '" + number + "'";
            string val = "user_id = " + id;
            UpdateData("Userx", set, val);
        }
        public void AddLocation(string location)
        {
            string set = "location_ = '" + location + "'";
            string val = "user_id = " + id;
            UpdateData("Userx", set, val);
        }
        public List<string> GetShoppingListInfo()
        {
            bool x;
            int i = 0;
            string prod_id;
            int value;
            string prod;
            string quan;
            List<string> products = new List<string>();
            string val = "user_id = " + id;
            OleDbDataReader r = GetFromDatabase("shopping_list", "quantity, product_id", val);
            x = r.Read();
            while (x)
            {
                prod_id = r["product_id"].ToString();
                prod = GetProductName(prod_id);
                value = int.Parse(r["quantity"].ToString());
                quan = value.ToString() + "x";
                products.Add(prod + " " + quan);
                x = r.Read();
                i++;
            }
            return products;
        }
        public List<string> GetShoppingListProdIds()
        {
            bool x;
            List<string> products = new List<string>();
            string val = "user_id = " + id;
            OleDbDataReader r = GetFromDatabase("shopping_list", "product_id", val);
            x = r.Read();
            while (x)
            {
                products.Add(r["product_id"].ToString());
                x = r.Read();
            }
            return products;
        }
        public void NewShoppinglist(string quantity, string product_id)
        {
            string val = "'x','" + quantity + "'," + id + ",'" + product_id + "'";
            AddToDatabase("shopping_list", "list_name, quantity, user_id, product_id", val);
        }
        public string GetProductId(string product_name)
        {
            string val = "user_id = " + id + " AND product_name = '" + product_name + "'";
            OleDbDataReader r = GetFromDatabase("product", "product_id", val);
            r.Read();
            return r["product_id"].ToString();
        }
        public string GetProductName(string product_id)
        {
            string val = "user_id = " + id + " AND product_id = " + product_id + "";
            OleDbDataReader r = GetFromDatabase("product", "product_name", val);
            r.Read();
            return r["product_name"].ToString();
        }
        public string GetSkillLvl(string recipe)
        {
            string val = "user_id = " + id + " AND recipe_name = '" + recipe + "'";
            OleDbDataReader r = GetFromDatabase("recipe", "skill", val);
            r.Read();
            return r["skill"].ToString();
        }
        public string GetDescription(string recipe)
        {
            string val = "user_id = " + id + " AND recipe_name = '" + recipe + "'";
            OleDbDataReader r = GetFromDatabase("recipe", "description", val);
            r.Read();
            return r["description"].ToString();
        }
        public List<string> GetInfo()
        {
            List<string> info = new List<string>();
            string val = "user_id = " + id;
            OleDbDataReader r = GetFromDatabase("Userx", "first_name, last_name, email_add, gender, numba, location_", val);
            r.Read();
            info.Add(r["first_name"].ToString());
            info.Add(r["last_name"].ToString());
            info.Add(r["email_add"].ToString());
            info.Add(r["gender"].ToString());
            info.Add(r["numba"].ToString());
            info.Add(r["location_"].ToString());
            return info;
        }
        public List<string> GetRecipes()
        {
            bool x;
            List<string> recipes = new List<string>();
            string val = "user_id = " + id;
            OleDbDataReader r = GetFromDatabase("recipe", "recipe_name", val);
            x = r.Read();
            while (x)
            {
                recipes.Add(r["recipe_name"].ToString());
                x = r.Read();
            }
            return recipes;
        }
        public List<string> GetRecipeIngProductId(string recipe_id)
        {
            bool x;
            List<string> products = new List<string>();
            string val = "user_id = " + id + " AND recipe_id = " + recipe_id;
            OleDbDataReader r = GetFromDatabase("recipe_ingredients", "product_id", val);
            x = r.Read();
            while (x)
            {
                products.Add(r["product_id"].ToString());
                x = r.Read();
            }
            return products;
        }
        public bool IfProductExist(string product_name)
        {
            string val = "user_id = " + id + " AND product_name = '" + product_name + "'";
            OleDbDataReader r = GetFromDatabase("storage", "product_name", val);
            if (r.Read())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IfRecipeExist(string recipe_name)
        {
            string val = "user_id = " + id + " AND recipe_name = '" + recipe_name + "'";
            OleDbDataReader r = GetFromDatabase("recipe", "recipe_name", val);
            if (r.Read())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string GetRecipeId(string recipe_name)
        {
            string val = "user_id = " + id + " AND recipe_name = '" + recipe_name + "'";
            OleDbDataReader r = GetFromDatabase("recipe", "recipe_id", val);
            r.Read();
            return r["recipe_id"].ToString();
        }
        public List<string> SearchStorageProduct(string product_name)
        {
            List<string> products = new List<string>();
            string val = "user_id = " + id + " AND product_name = '" + product_name + "'";
            OleDbDataReader r = GetFromDatabase("storage", "product_name, available_quantity", val);
            r.Read();
            products.Add(r["product_name"].ToString() + " " + r["available_quantity"].ToString() + "x");
            return products;
        }
        public List<string> SearchRecipe(string recipe_name)
        {
            List<string> recipes = new List<string>();
            string val = "user_id = " + id + " AND recipe_name = '" + recipe_name + "'";
            OleDbDataReader r = GetFromDatabase("recipe", "recipe_name", val);
            r.Read();
            recipes.Add(r["recipe_name"].ToString());
            return recipes;
        }
        public List<int> GetRecipeProductQ(string recipe, string recipe_id)
        {
            bool x;
            string val = "user_id = " + id + " AND recipe_id = " + int.Parse(recipe_id);
            List<int> quantity = new List<int>();
            OleDbDataReader r = GetFromDatabase("recipe_ingredients", "quantity", val);
            x = r.Read();
            while (x)
            {
                quantity.Add(int.Parse(r["quantity"].ToString()));
                x = r.Read();
            }
            return quantity;
        }
        public void AddShoppingListQuantity(string product_id, string quantity)
        {
            string set = "quantity = quantity + " + quantity;
            string val = "user_id = " + id + " AND product_id = " + product_id;
            UpdateData("shopping_list", set, val);
        }
        public List<int> GetStoragePQForRecipe(List<string> recipeIngProductId)
        {
            int i = 0;
            string val = "user_id = " + id + " AND product_id = " + recipeIngProductId[0];
            List<int> storageq = new List<int>();
            OleDbDataReader r = GetFromDatabase("storage", "available_quantity", val);
            while (recipeIngProductId.Count > i)
            {
                r.Read();
                storageq.Add(int.Parse(r["available_quantity"].ToString()));
                i++;
                if (i == recipeIngProductId.Count)
                {
                    break;
                }
                val = "user_id = " + id + " AND product_id = " + recipeIngProductId[i];
                r = GetFromDatabase("storage", "available_quantity", val);
            }
            return storageq;
        }
        public List<string> ProductNameRecipe(List<string> recipeIngProductId)
        {
            int i = 0;
            string val = "user_id = " + id + " AND product_id = " + recipeIngProductId[0];
            List<string> products = new List<string>();
            OleDbDataReader r = GetFromDatabase("storage", "product_name", val);
            while (recipeIngProductId.Count > i)
            {
                r.Read();
                products.Add(r["product_name"].ToString());
                i++;
                if (i == recipeIngProductId.Count)
                {
                    break;
                }
                val = "user_id = " + id + " AND product_id = " + recipeIngProductId[i];
                r = GetFromDatabase("storage", "product_name", val);
            }
            return products;
        }
        public void ConsumeRecipe(int q, string product_name, int portions)
        {
            int minus = portions * q;
            string val = "available_quantity = available_quantity - " + minus.ToString();
            string where = "user_id = " + id + " AND product_name = '" + product_name + "'";
            UpdateData("storage", val, where);
        }
        public int GetRecipePortions(string recipe_name)
        {
            string val = "user_id = " + id + " AND recipe_name = '" + recipe_name + "'";
            OleDbDataReader r = GetFromDatabase("recipe", "portions", val);
            r.Read();
            return int.Parse(r["portions"].ToString());
        }
        public bool CheckUser(string email, string password)
        {
            string val = "email_add = '" + email + "' AND passwrd = '" + password + "'";
            OleDbDataReader r = GetFromDatabase("Userx", "email_add, passwrd", val);
            if (r.Read())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<string> PrintStorage()
        {
            bool x;
            List<string> prods = new List<string>();
            string val = "user_id = " + id.ToString();
            OleDbDataReader r = GetFromDatabase("storage", "product_name, available_quantity", val);
            x = r.Read();
            while (x)
            {
                prods.Add(r["product_name"].ToString() + " " + r["available_quantity"].ToString() + "x");
                x = r.Read();
            }
            return prods;
        }
        public void GetId(string email, string password)
        {
            string val = "email_add = '" + email + "' AND passwrd = '" + password + "'";
            OleDbDataReader r = GetFromDatabase("Userx", "user_id", val);
            r.Read();
            string idd = r["user_id"].ToString();
            id = idd;
        }
    }
}
