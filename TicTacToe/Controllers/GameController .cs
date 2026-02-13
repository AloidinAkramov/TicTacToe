using Microsoft.AspNetCore.Mvc;
using TicTacToe.Services;

namespace TicTacToe.Controllers;
public class GameController : Controller
{
    // Load game page
    public IActionResult Index(Guid gameId)
    {
        var playerName = HttpContext.Session.GetString("PlayerName");

        // IMPORTANT: Player must exist in session
        if (string.IsNullOrEmpty(playerName))
            return RedirectToAction("Index", "Home");

        ViewBag.GameId = gameId;
        ViewBag.PlayerName = playerName;

        return View();
    }
}