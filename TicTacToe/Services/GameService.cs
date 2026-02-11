using Microsoft.EntityFrameworkCore;
using TicTacToe.Data;
using TicTacToe.Models;

namespace TicTacToe.Services;
public class GameService : IGameService
{
    private readonly AppDbContext _context;

    public GameService(AppDbContext context)
    {
        _context = context;
    }

    public Game CreateGame(string playerName)
    {
        var player = GetOrCreatePlayer(playerName);

        var emptyBoard = new string?[9];

        var game = new Game
        {
            Id = Guid.NewGuid(),
            PlayerXId = player.Id,
            Board = new string?[9],
            CurrentTurn = player.Name,
            IsFinished = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.Games.Add(game);
        _context.SaveChanges();

        return game;
    }

    public Game JoinGame(Guid gameId, string playerName)
    {
        var game = GetGame(gameId);

        if (game.PlayerOId != null)
            throw new Exception("This game already has two players.");

        var player = GetOrCreatePlayer(playerName);

        game.PlayerOId = player.Id;
        _context.SaveChanges();

        return game;
    }

    public Game MakeMove(Guid gameId, int index, string playerName)
    {
        var game = GetGame(gameId);

        if (game.IsFinished)
            throw new Exception("Game is already finished.");


        if (game.PlayerO == null)
            throw new Exception("Waiting for second player.");

        if (game.CurrentTurn != playerName)
            throw new Exception("It is not your turn.");

        var board = game.Board;

        if (board[index] != null)
            throw new Exception("This cell is already taken.");

        board[index] = playerName == game.PlayerX.Name ? "X" : "O";

        if (game.PlayerX.Name == playerName)
        {
            game.CurrentTurn = game.PlayerO.Name;
        }
        else
        {
            game.CurrentTurn = game.PlayerX.Name;
        }
        _context.SaveChanges();

        return game;
    }

    public List<Game> GetAvailableGames()
    {
        return _context.Games
            .Include(g => g.PlayerX)
            .Include(g => g.PlayerO)
            .Where(g => !g.IsFinished && g.PlayerOId == null)
            .OrderByDescending(g => g.CreatedAt)
            .ToList();
    }

    private Player GetOrCreatePlayer(string name)
    {
        var player = _context.Players.FirstOrDefault(p => p.Name == name);

        if (player == null)
        {
            player = new Player
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            _context.Players.Add(player);
            _context.SaveChanges();
        }

        return player;
    }

    public Game GetGame(Guid gameId)
    {
        var game = _context.Games
            .Include(g => g.PlayerX)
            .Include(g => g.PlayerO)
            .FirstOrDefault(g => g.Id == gameId);

        if (game == null)
            throw new Exception("Game not found.");

        return game;
    }
}