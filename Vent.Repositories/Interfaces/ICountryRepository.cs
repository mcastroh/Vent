using Vent.Shared.Dtos;
using Vent.Shared.Responses;

namespace Vent.Repositories.Interfaces;

public interface ICountryRepository
{
    Task<Response> GetAsync(PaginationDto pagination);
}