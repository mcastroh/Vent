using Microsoft.EntityFrameworkCore;
using Vent.DataAccess;
using Vent.Repositories.Helpers;
using Vent.Repositories.Interfaces;
using Vent.Shared.Dtos;
using Vent.Shared.Responses;

namespace Vent.Repositories.Implementations;

public class CountryRepository : ICountryRepository
{
    private readonly DataContext _context;

    public CountryRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Response> GetAsync(PaginationDto pagination)
    {
        var queryable = _context.Countries.Include(s => s.States).AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
            queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));

        double count = await queryable.CountAsync();

        Response result = new()
        {
            IsSuccess = true,
            CountItem = await queryable.CountAsync(),
            Result = await queryable.OrderBy(x => x.Name).Paginate(pagination).ToListAsync()
        };

        return result;
    }
}