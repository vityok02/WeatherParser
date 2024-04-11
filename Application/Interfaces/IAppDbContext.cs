using Domain.Locations;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IAppDbContext
{
    public DbSet<User> Users { get; }
    public DbSet<Location> Locations { get; }
    public DbSet<Coordinates> Coordinates { get; }
}
