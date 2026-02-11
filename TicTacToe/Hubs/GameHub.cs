using Microsoft.AspNetCore.SignalR;
using TicTacToe.Services;

namespace TicTacToe.Hubs;
public class GameHub : Hub
{
    private readonly IGameService _gameService;

    public GameHub(IGameService gameService)
    {
        _gameService = gameService;
    }

    public async Task JoinGame(string gameId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, gameId);

        var game = _gameService.GetGame(Guid.Parse(gameId));

        await Clients.Group(gameId)
            .SendAsync("GameStarted", game.CurrentTurn);
    }

    public async Task MakeMove(string gameId, int index, string playerName)
    {
        var game = _gameService.MakeMove(
            Guid.Parse(gameId),
            index,
            playerName
        );

        var board = game.Board;

        var winCombo = CheckWinner(board);

        if (winCombo != null)
        {
            game.IsFinished = true;

            await Clients.Group(gameId)
                .SendAsync("GameOver",
                    board,
                    winCombo,
                    playerName);

            return;
        }

        if (board.All(x => x != null))
        {
            game.IsFinished = true;

            await Clients.Group(gameId)
                .SendAsync("GameDraw", board);

            return;
        }

        await Clients.Group(gameId)
            .SendAsync("MoveMade",
                board,
                game.CurrentTurn);
    }

    private int[]? CheckWinner(string?[] board)
    {
        int[][] combos =
        {
            new [] {0,1,2},
            new [] {3,4,5},
            new [] {6,7,8},
            new [] {0,3,6},
            new [] {1,4,7},
            new [] {2,5,8},
            new [] {0,4,8},
            new [] {2,4,6}
        };

        foreach (var combo in combos)
        {
            var a = combo[0];
            var b = combo[1];
            var c = combo[2];

            if (board[a] != null &&
                board[a] == board[b] &&
                board[a] == board[c])
            {
                return combo;
            }
        }

        return null;
    }
}