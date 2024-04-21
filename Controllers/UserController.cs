using Microsoft.AspNetCore.Mvc;
using Product_Inventory.Models;

namespace Product_Inventory.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepo userRep;

        public UserController(UserRepo userRepo)
        {
            userRep = userRepo;
        }
        public IActionResult Index()
        {
            return View("signin");
        }
        [HttpPost]
        public IActionResult signinForm(User user)
        {
            List<User> result = userRep.verifyRegularUsre(user);
            if (result.Count == 0)
                return View("signin", "Incorrect Email or Password.");

            HttpContext.Session.SetString("Email", user.Email);
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
                return View("signup");
            return View("signin");
        }
        public IActionResult dashboard()
        {
            //User user = new User("Saad Sultan", "xdsaad5@gmail.com","Lahore", "3008101304");
            return View();
        }

    }
}
