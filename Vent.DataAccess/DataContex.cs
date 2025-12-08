using Microsoft.EntityFrameworkCore;
using Vent.Shared.Entities;

namespace Vent.DataAccess;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<City> Cities => Set<City>();
    public DbSet<State> States => Set<State>();

    //public DbSet<Country> Countries { get; set; }
    //public DbSet<City> Cities { get; set; }
    //public DbSet<State> States { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Country>().HasIndex(x => x.Name).IsUnique();
        modelBuilder.Entity<City>().HasIndex(x => x.Name).IsUnique();
        modelBuilder.Entity<State>().HasIndex(x => x.Name).IsUnique();

        //modelBuilder.Entity<City>().HasIndex(x => new { x.Name, x.StateId }).IsUnique();
        //modelBuilder.Entity<State>().HasIndex(x => new { x.Name, x.Country }).IsUnique();

        DisableCascadingDelete(modelBuilder);
    }

    private void DisableCascadingDelete(ModelBuilder modelBuilder)
    {
        var relationships = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys());

        foreach (var relationship in relationships)
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}