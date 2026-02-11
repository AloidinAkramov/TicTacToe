using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TicTacToe.Hubs;
using TicTacToe.Services;

namespace TicTacToe.Controllers;
public class LobbyController : Controller
{
    private readonly IGameService _gameService;
    private readonly IHubContext<GameHub> _hub;

    public LobbyController(IGameService gameService, IHubContext<GameHub> hub)
    {
        _gameService = gameService;
        _hub = hub;
    }

    public IActionResult Index()
    {
        var playerName = HttpContext.Session.GetString("PlayerName");

        if (string.IsNullOrEmpty(playerName))
            return RedirectToAction("Index", "Home");

        var games = _gameService.GetAvailableGames();
        return View(games);
    }

    [HttpPost]
    [IgnoreAntiforgeryToken]
    public IActionResult Create()
    {
        var playerName = HttpContext.Session.GetString("PlayerName");

        if (string.IsNullOrEmpty(playerName))
            return RedirectToAction("Index", "Home");

        var game = _gameService.CreateGame(playerName);

        return RedirectToAction("Index", "Game", new { gameId = game.Id });
    }

    [HttpPost]
    [IgnoreAntiforgeryToken]
    public IActionResult Join(Guid gameId)
    {
        var playerName = HttpContext.Session.GetString("PlayerName");

        if (string.IsNullOrEmpty(playerName))
            return RedirectToAction("Index", "Home");

        _gameService.JoinGame(gameId, playerName);

        return RedirectToAction("Index", "Game", new { gameId });
    }
}