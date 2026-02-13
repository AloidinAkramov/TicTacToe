using TicTacToe.Models;

namespace TicTacToe.Services;
public interface IGameService
{
    Game CreateGame(string playerName);
    Game JoinGame(Guid gameId, string playerName);
    Game MakeMove(Guid gameId, int index, string playerName);
    List<Game> GetAvailableGames();
    Game GetGame(Guid gameId);

    void FinishGame(Guid gameId, string winnerName);
    void FinishDraw(Guid gameId);
    List<Player> GetTopPlayers();
    Game ResetGame(Guid gameId);
}