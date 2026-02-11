namespace TicTacToe.Models;
public class Move
{
    public Guid Id { get; set; }
    public Guid GameId { get; set; }
    public int CellIndex { get; set; }
    public string Player { get; set; }
    public DateTime PlayedAt { get; set; }
}