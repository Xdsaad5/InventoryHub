using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Product_Inventory.Models;

namespace Product_Inventory.Controllers;

public class HomeController : Controller
{
    private readonly UserRepo userRep;

    public HomeController(UserRepo userRepo)
    {
        userRep = userRepo;

    }
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult signin()
    {
        return View();
    }
    [HttpPost]
    public IActionResult signinForm(User user)
    {
        List<User> result = userRep.verifyRegularUsre(user);
        if (result.Count == 0)
            return View("signin", "Incorrect Email or Password.");

        HttpContext.Session.SetString("Email", user.Email);
        HttpContext.Session.SetString("Name", result[0].Name);
        HttpContext.Session.SetString("MobileNumber", result[0].MobileNumber);
        HttpContext.Session.SetString("location", result[0].Location);
        return View("dashboard", new User { Name = result[0].Name, Email = user.Email, MobileNumber = result[0].MobileNumber, Location = result[0].Location });
    }
    public IActionResult signup()
    {
        return View();
    }
    [HttpPost]
    public IActionResult signupForm(User user)
    {

        if (!userRep.addRegularUser(user))
            return View("signup","User with this Email already exist.");
        return View("signin");
    }
    public IActionResult dashboard()
    {
        string? email = HttpContext.Session.GetString("Email");

        if (email == null)
            return View("signin", "You don't sign in your account");
        User user = new User
        {
            Name = HttpContext.Session.GetString("Name"),
            Email = HttpContext.Session.GetString("Email"),
            MobileNumber = HttpContext.Session.GetString("MobileNumber"),
            Location = HttpContext.Session.GetString("location")

        };
        return View(user);
    }
    public IActionResult listItem()
    {
        string? email = HttpContext.Session.GetString("Email");

        if (email == null)
            return View("signin", "You don't sign in your account");
        List<Product> allProducts = userRep.getAllProducts();
        /*if(allProducts.Count == 0)
            return View("listItem", allProducts);*/
        foreach (var product in allProducts)
            product.Description = $"Its a {userRep.getCategory(product.CategoryId).ToLower()} item.\n{product.Description}";
        return View("listItem", allProducts);
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
        string? email = HttpContext.Session.GetString("Email");

        if (email == null)
            return View("signin", "You don't sign in your account");
        List<Product> allProducts = userRep.searchProduct(productName);
        Console.WriteLine(allProducts.Count);
        if (allProducts.Count == 0)
            return View("searchItem", "No Item Found");
        foreach (var product in allProducts)
            product.Description = userRep.getCategory(product.CategoryId);
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

}
