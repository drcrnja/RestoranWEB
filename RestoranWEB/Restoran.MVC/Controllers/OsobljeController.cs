using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[AllowAnonymous]  
public class OsobljeController : Controller
{
    public IActionResult Index() => View();
}