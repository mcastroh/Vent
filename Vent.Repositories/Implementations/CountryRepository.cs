using Microsoft.EntityFrameworkCore;
using Vent.DataAccess;
using Vent.Repositories.Helpers;
using Vent.Repositories.Interfaces;
using Vent.Shared.Dtos;
using Vent.Shared.Entities;
using Vent.Shared.Responses;

namespace Vent.Repositories.Implementations;

public class CountryRepository : ICountryRepository
{
    private readonly DataContext _context;

    public CountryRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Response> AddAsync(Country entity)
    {
        _context.Countries.Add(entity);

        try
        {
            await _context.SaveChangesAsync();
            return new Response() { IsSuccess = true, Result = entity };
        }
        catch (DbUpdateException)
        {
            return DbUpdateExceptionActionResponse();
        }
        catch (Exception exception)
        {
            return ExceptionActionResponse(exception);
        }
    }

    public async Task<Response> DeleteAsync(int id)
    {
        var row = await _context.Countries.FindAsync(id);
        if (row == null) return new Response() { IsSuccess = false, Message = "ERR001" };

        try
        {
            _context.Countries.Remove(row);
            await _context.SaveChangesAsync();
            return new Response() { IsSuccess = true };
        }
        catch
        {
            return new Response() { IsSuccess = false, Message = "ERR002" };
        }
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

    public async Task<Response> GetByIdAsync(int id)
    {
        var result = await _context.Countries.FindAsync(id);
        if (result == null) return new Response() { IsSuccess = false, Message = "ERR001" };
        return new Response() { IsSuccess = true, Result = result };
    }

    public async Task<Response> UpdateAsync(Country entity)
    {
        try
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return new Response() { IsSuccess = true, Result = entity };
        }
        catch (DbUpdateException)
        {
            return DbUpdateExceptionActionResponse();
        }
        catch (Exception exception)
        {
            return ExceptionActionResponse(exception);
        }
    }

    private Response ExceptionActionResponse(Exception exception)
        => new Response()
        {
            IsSuccess = false,
            Message = exception.Message
        };

    private Response DbUpdateExceptionActionResponse()
        => new Response
        {
            IsSuccess = false,
            Message = "ERR003"
        };
}