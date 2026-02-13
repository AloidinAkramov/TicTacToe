using Microsoft.AspNetCore.Mvc;

namespace TicTacToe.Controllers;
public class HomeController : Controller
{
    // Load home page
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SaveName(string playerName)
    {
        // IMPORTANT: Prevent empty or invalid name
        if (string.IsNullOrWhiteSpace(playerName))
            return RedirectToAction("Index");

        // Store player name in session
        HttpContext.Session.SetString("PlayerName", playerName);

        return RedirectToAction("Index", "Lobby");
    }
}