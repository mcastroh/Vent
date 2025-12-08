using Microsoft.EntityFrameworkCore;
using Vent.DataAccess;
using Vent.Repositories.Interfaces;
using Vent.Shared.Entities;

namespace Vent.Repositories.Implementations;

public class CountryRepository : ICountryRepository
{
    private readonly DataContext _context;

    public CountryRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Country>> GetAsync()
        => await _context.Countries.ToListAsync();

    public async Task<IEnumerable<Country>> GetAllAsync()
       => await _context.Countries.Include(e => e.States)!.ThenInclude(c => c.Cities).ToListAsync();
}