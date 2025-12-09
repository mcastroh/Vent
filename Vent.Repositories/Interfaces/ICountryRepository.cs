using Vent.Shared.Dtos;
using Vent.Shared.Entities;
using Vent.Shared.Responses;

namespace Vent.Repositories.Interfaces;

public interface ICountryRepository
{
    Task<Response> AddAsync(Country entity);

    Task<Response> DeleteAsync(int id);

    Task<Response> GetAsync(PaginationDto pagination);

    Task<Response> GetByIdAsync(int id);

    Task<Response> UpdateAsync(Country entity);
}