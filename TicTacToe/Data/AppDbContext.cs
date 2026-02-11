using Microsoft.EntityFrameworkCore;
using TicTacToe.Models;

namespace TicTacToe.Data;
public class AppDbContext : DbContext
{
    public DbSet<Player> Players { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Move> Moves { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
}