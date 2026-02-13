namespace TicTacToe.Models;
public class Player
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public int Wins { get; set; } = 0;
    public int Losses { get; set; } = 0;
    public int Draws { get; set; } = 0;
    public int TotalGames { get; set; } = 0;
}