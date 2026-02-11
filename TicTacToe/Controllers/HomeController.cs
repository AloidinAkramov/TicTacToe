using Microsoft.AspNetCore.Mvc;

namespace TicTacToe.Controllers;
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SaveName(string playerName)
    {
        if (string.IsNullOrWhiteSpace(playerName))
            return RedirectToAction("Index");

        HttpContext.Session.SetString("PlayerName", playerName);

        return RedirectToAction("Index", "Lobby");
    }
}