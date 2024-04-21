using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Product_Inventory.Models;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Product_Inventory.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminRepo adminRep;

        public AdminController(AdminRepo adminRepo)
        {
            adminRep = adminRepo;
           
        }
        public IActionResult Index()
        {
            return View("signin");
        }
        [HttpPost]
        public IActionResult signinForm(User admin)
        {
            List<User> result = adminRep.verifyAdmin(admin);
            if (result.Count==0)
                return View("signin", "Incorrect Email or Password.");

            HttpContext.Session.SetString("Email", admin.Email);
            HttpContext.Session.SetString("Name", result[0].Name);
            HttpContext.Session.SetString("MobileNumber", result[0].MobileNumber);
            HttpContext.Session.SetString("location", result[0].Location);
            return View("dashboard", new User { Name = result[0].Name, Email = admin.Email, MobileNumber = result[0].MobileNumber, Location = result[0].Location });
        }
        public IActionResult signup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult signupForm(User admin)
        {

            if (!adminRep.addAdmin(admin))
                return View("signup");
            return View("signin");
        }
        public IActionResult dashboard()
        {
            string? email = HttpContext.Session.GetString("Email");

            if (email == null)
                return View("signin", "You don't sign in your account");
            User admin = new User
            {
                Name = HttpContext.Session.GetString("Name"),
                Email = HttpContext.Session.GetString("Email"),
                MobileNumber = HttpContext.Session.GetString("MobileNumber"),
                Location = HttpContext.Session.GetString("location")

            };
            return View(admin);
        }
        public IActionResult addItem()
        {
            string? email = HttpContext.Session.GetString("Email");

            if (email == null)
                return View("signin", "You don't sign in your account");
            return View();
        }
        [HttpPost]
        public IActionResult addItemForm(Product p,string category)
        {

            Console.WriteLine($"Name: {p.Name}");
            if (!adminRep.addProduct(p, category))
                return View("addItem", "Error in Adding New Item");

            return View("addItem", "Successfully Add New Item.");
        }
        public IActionResult listItem()
        {
            string? email = HttpContext.Session.GetString("Email");

            if (email == null)
                return View("signin", "You don't sign in your account");
            List<Product> allProducts = adminRep.getAllProducts();
            /*if(allProducts.Count == 0)
                return View("listItem", allProducts);*/
            foreach (var product in allProducts)
                product.Description = $"Its a {adminRep.getCategory(product.CategoryId).ToLower()} item.\n{product.Description}";

            return View("listItem",allProducts);
        }
        public IActionResult Edit()
        {
            string? email = HttpContext.Session.GetString("Email");

            if (email == null)
                return View("signin", "You don't sign in your account");
            return View(); 
        }
        [HttpPost]
        public IActionResult editForm(Product p, string Category)
        {
            if (adminRep.updateItemDetails(p, Category))
                return View("edit", "Successfully Updated.");
            return View("edit", "No Field  is  updated.");
        }
        public IActionResult searchItem()
        {
            string? email = HttpContext.Session.GetString("Email");

            if (email == null)
                return View("signin", "You don't sign in your account");
            return View();
        }
        [HttpPost]
        public IActionResult searchItemForm(string productName)
        {
            
            List<Product> allProducts = adminRep.searchProduct(productName);
            Console.WriteLine(allProducts.Count);
            if (allProducts.Count == 0)
                return View("searchItem", "No Item Found");
            foreach (var product in allProducts)
                product.Description = $"Its a {adminRep.getCategory(product.CategoryId).ToLower()} item.\n{product.Description}";
            return View("listItem", allProducts);
        }
        public async Task<IActionResult> logout()
        {
            string? email = HttpContext.Session.GetString("Email");

            if (email == null)
                return View("signin", "You don't sign in your account");
            HttpContext.Session.Clear();
            return View("signin", "Successfully Logout.");
        }
        public IActionResult remove(int productId)
        {
            string? email = HttpContext.Session.GetString("Email");

            if (email == null)
                return View("signin", "You don't sign in your account");
            Console.WriteLine($"productId: {productId}");
            bool success = adminRep.removeItem(productId);
            List<Product> allProducts = adminRep.getAllProducts();
            foreach (var product in allProducts)
                product.Description = adminRep.getCategory(product.CategoryId);
            return View("listItem", allProducts);
        }

    }
}
