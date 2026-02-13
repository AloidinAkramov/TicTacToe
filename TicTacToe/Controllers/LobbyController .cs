using Microsoft.AspNetCore.Mvc;
using TicTacToe.Services;

namespace TicTacToe.Controllers;
public class LobbyController : Controller
{
    private readonly IGameService _gameService;

    public LobbyController(IGameService gameService)
    {
        _gameService = gameService;
    }

    // Load lobby with available games
    public IActionResult Index()
    {
        var playerName = HttpContext.Session.GetString("PlayerName");

        // IMPORTANT: Player must exist in session
        if (string.IsNullOrEmpty(playerName))
            return RedirectToAction("Index", "Home");

        var games = _gameService.GetAvailableGames();
        return View(games);
    }

    [HttpPost]
    public IActionResult Create()
    {
        var playerName = HttpContext.Session.GetString("PlayerName");

        // IMPORTANT: Prevent creating game without player
        if (string.IsNullOrEmpty(playerName))
            return RedirectToAction("Index", "Home");

        var game = _gameService.CreateGame(playerName);

        return RedirectToAction("Index", "Game", new { gameId = game.Id });
    }

    [HttpPost]
    public IActionResult Join(Guid gameId)
    {
        var playerName = HttpContext.Session.GetString("PlayerName");

        // IMPORTANT: Prevent joining without session
        if (string.IsNullOrEmpty(playerName))
            return RedirectToAction("Index", "Home");

        _gameService.JoinGame(gameId, playerName);

        return RedirectToAction("Index", "Game", new { gameId });
    }
}