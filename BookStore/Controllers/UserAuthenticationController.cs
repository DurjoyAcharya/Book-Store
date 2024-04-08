using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers;

public class UserAuthenticationController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}