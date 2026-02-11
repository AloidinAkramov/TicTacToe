using Microsoft.AspNetCore.Mvc;
using TicTacToe.Services;

namespace TicTacToe.Controllers;
public class GameController : Controller
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    public IActionResult Index(Guid gameId)
    {
        var playerName = HttpContext.Session.GetString("PlayerName");

        if (string.IsNullOrEmpty(playerName))
            return RedirectToAction("Index", "Home");

        ViewBag.GameId = gameId;
        ViewBag.PlayerName = playerName;

        return View();
    }
}