using Microsoft.AspNetCore.Mvc;
using TicTacToe.Services;

namespace TicTacToe.Controllers;
public class LeaderboardController : Controller
{
    private readonly IGameService _gameService;

    public LeaderboardController(IGameService gameService)
    {
        _gameService = gameService;
    }

    // Load leaderboard page with top players
    public IActionResult Index()
    {
        var players = _gameService.GetTopPlayers();
        return View(players);
    }
}