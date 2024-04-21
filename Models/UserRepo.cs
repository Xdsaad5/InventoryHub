using System;

namespace Product_Inventory.Models
{
    public class UserRepo
    {
        public UserRepo()
        {

        }
        public bool addRegularUser(User regularUsre)
        {
            try
            {

                InventoryHubDbContext pdb = new InventoryHubDbContext();
                pdb.Users.Add(regularUsre);
                pdb.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding admin: {ex.Message}");
                return false;
            }
        }
        public List<User> verifyRegularUsre(User regularUsre)
        {
            try
            {
                InventoryHubDbContext pdb = new InventoryHubDbContext();
                var data = pdb.Users.Where(x => x.Email == regularUsre.Email && x.Password == regularUsre.Password && x.IsAdmin == 0).ToList();
                return data;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding admin: {ex.Message}");
                return new List<User>();
            }
        }
        public string getCategory(int? id)
        {
            InventoryHubDbContext pdb = new InventoryHubDbContext();
            var result = pdb.Categories.Where(x => x.CategoryId == id).ToList();
            if (result.Count == 0)
                return "No category Selected";
            return result[0].Name;
        }
        public List<Product> searchProduct(String productName)
        {
            try
            {
                InventoryHubDbContext pdb = new InventoryHubDbContext();
                var data = pdb.Products.Where(x => x.Name.Contains(productName)).ToList();
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding admin: {ex.Message}");
                return new List<Product>();
            }
        }
        public List<Product> getAllProducts()
        {
            try
            {
                InventoryHubDbContext pdb = new InventoryHubDbContext();
                var data = pdb.Products.ToList();
                return data;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding admin: {ex.Message}");
                return new List<Product>();
            }
        }
    }
}

