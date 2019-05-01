using api;
using api.Features.House;
using Microsoft.EntityFrameworkCore;

public class RoomMateDbContext : DbContext
{
    public DbSet<RoomMate> RoomMates { get; set; }
    public DbSet<House> Houses { get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer ("Server=localhost;Database=Test;User Id=SA;Password=ToBeChanged.");
    }
}