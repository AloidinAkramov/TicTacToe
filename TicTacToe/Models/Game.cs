using Microsoft.EntityFrameworkCore.Migrations;

namespace TicTacToe.Models;
public class Game
{
    public Guid Id { get; set; }

    public Guid PlayerXId { get; set; }
    public Player PlayerX { get; set; }

    public Guid? PlayerOId { get; set; }
    public Player? PlayerO { get; set; }
    public string?[] Board { get; set; } = new string?[9];
    public string CurrentTurn { get; set; }
    public bool IsFinished { get; set; }

    public int PlayerXScore { get; set; } = 0;
    public int PlayerOScore { get; set; } = 0;

    public DateTime CreatedAt { get; set; }

}