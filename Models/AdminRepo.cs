using System;

namespace Product_Inventory.Models
{
	public class AdminRepo
	{
		public AdminRepo()
		{

		}
		public bool addAdmin(User admin)
		{
			try
			{
				admin.IsAdmin = 1;
                InventoryHubDbContext pdb = new InventoryHubDbContext();
				pdb.Users.Add(admin);
				pdb.SaveChanges();
				return true;

			}
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding admin: {ex.Message}");
                return false;
            }
		}
        public List<User> verifyAdmin(User admin)
        {
            try
            {
                InventoryHubDbContext pdb = new InventoryHubDbContext();
                var data = pdb.Users.Where(x => x.Email == admin.Email && x.Password == admin.Password && x.IsAdmin == 1).ToList();
                return data;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding admin: {ex.Message}");
                return new List<User>();
            }
        }
        public bool addProduct(Product p,string category)
        {
            try
            {
                
                InventoryHubDbContext pdb = new InventoryHubDbContext();
                var id = pdb.Categories.Where(x => x.Name == category).ToList();
                Console.WriteLine(id.Count);
                if (id.Count == 0)
                    return false;
                p.CategoryId = id[0].CategoryId;
                pdb.Products.Add(p);
                pdb.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding admin: {ex.Message}");
                return false;
            }
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
        public string getCategory(int? id)
        {
            InventoryHubDbContext pdb = new InventoryHubDbContext();
            var result = pdb.Categories.Where(x => x.CategoryId == id).ToList();
            if (result.Count == 0)
                return "No category Selected";
            return result[0].Name;
        }
        public bool updateItemDetails(Product p, string category)
        {
            if (p.Price <= 0 && p.Quantity <= 0 && category == null && p.Name == null && p.Description == null)
                return false;
            try
            {

                InventoryHubDbContext pdb = new InventoryHubDbContext();
                var categoryResult = pdb.Categories.Where(x => x.Name == category).ToList();
                var productResult = pdb.Products.Where(x => x.ProductId == p.ProductId).ToList();
                if (categoryResult.Count != 0)
                    productResult[0].CategoryId = categoryResult[0].CategoryId;
                if (p.Name != null)
                    productResult[0].Name = p.Name;
                if (p.Description != null)
                    productResult[0].Description = p.Description;
                if (p.Price > 0)
                    productResult[0].Price = p.Price;
                if (p.Quantity > 0)
                    productResult[0].Quantity = p.Quantity;
                pdb.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding admin: {ex.Message}");
                return false;
            }
        }
        public bool removeItem(int productId)
        {
            try
            {
                InventoryHubDbContext pdb = new InventoryHubDbContext();
                var product = pdb.Products.FirstOrDefault(x => x.ProductId == productId);
                if (product != null)
                {
                    pdb.Products.Remove(product);
                    pdb.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing item: {ex.Message}");
                return false;
            }
        }


    }

}

