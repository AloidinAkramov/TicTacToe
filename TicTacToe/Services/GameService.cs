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

    // Create new game and assign PlayerX
    public Game CreateGame(string playerName)
    {
        var player = GetOrCreatePlayer(playerName);

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

    // Join game as PlayerO
    public Game JoinGame(Guid gameId, string playerName)
    {
        var game = GetGame(gameId);

        // IMPORTANT: Prevent more than two players
        if (game.PlayerOId != null)
            throw new Exception("This game already has two players.");

        var player = GetOrCreatePlayer(playerName);

        game.PlayerOId = player.Id;
        _context.SaveChanges();

        return game;
    }

    // Make move on board
    public Game MakeMove(Guid gameId, int index, string playerName)
    {
        var game = GetGame(gameId);

        // IMPORTANT: Block move if game is finished
        if (game.IsFinished)
            throw new Exception("Game is already finished.");

        // IMPORTANT: Wait until second player joins
        if (game.PlayerO == null)
            throw new Exception("Waiting for second player.");

        // IMPORTANT: Enforce turn order
        if (game.CurrentTurn != playerName)
            throw new Exception("It is not your turn.");

        var board = game.Board;

        // IMPORTANT: Prevent overwriting cell
        if (board[index] != null)
            throw new Exception("This cell is already taken.");

        // Assign symbol
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

    // Get games waiting for second player
    public List<Game> GetAvailableGames()
    {
        return _context.Games
            .Include(g => g.PlayerX)
            .Include(g => g.PlayerO)
            .Where(g => !g.IsFinished && g.PlayerOId == null)
            .OrderByDescending(g => g.CreatedAt)
            .ToList();
    }

    // Get existing player or create new one
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

    // Get game by Id
    public Game GetGame(Guid gameId)
    {
        var game = _context.Games
            .Include(g => g.PlayerX)
            .Include(g => g.PlayerO)
            .FirstOrDefault(g => g.Id == gameId);

        // IMPORTANT: Ensure game exists
        if (game == null)
            throw new Exception("Game not found.");

        return game;
    }

    // Top 10 players ranked by wins
    public List<Player> GetTopPlayers()
    {
        return _context.Players
            .OrderByDescending(p => p.Wins)
            .ThenByDescending(p => p.TotalGames)
            .Take(10)
            .ToList();
    }

    // Finish game and update winner stats
    public void FinishGame(Guid gameId, string winnerName)
    {
        var game = GetGame(gameId);

        // IMPORTANT: Prevent double finishing
        if (game.IsFinished)
            return;

        game.IsFinished = true;

        var playerX = game.PlayerX;
        var playerO = game.PlayerO;

        if (playerX == null || playerO == null)
            return;

        playerX.TotalGames++;
        playerO.TotalGames++;

        if (playerX.Name == winnerName)
        {
            playerX.Wins++;
            playerO.Losses++;
            game.PlayerXScore++;
        }
        else
        {
            playerO.Wins++;
            playerX.Losses++;
            game.PlayerOScore++;
        }

        _context.SaveChanges();
    }

    // Finish game as draw
    public void FinishDraw(Guid gameId)
    {
        var game = GetGame(gameId);

        if (game.IsFinished)
            return;

        game.IsFinished = true;

        var playerX = game.PlayerX;
        var playerO = game.PlayerO;

        if (playerX == null || playerO == null)
            return;

        playerX.TotalGames++;
        playerO.TotalGames++;

        playerX.Draws++;
        playerO.Draws++;

        _context.SaveChanges();
    }

    // Reset board for replay
    public Game ResetGame(Guid gameId)
    {
        var game = GetGame(gameId);

        game.Board = new string?[9];
        game.IsFinished = false;

        game.CurrentTurn = game.PlayerX.Name;

        _context.SaveChanges();

        return game;
    }
}