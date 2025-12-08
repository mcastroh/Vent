using Vent.Shared.Dtos;
using Vent.Shared.Responses;

namespace Vent.Services.Interfaces;

public interface ICountryService
{
    Task<Response> GetAsync(PaginationDto pagination);
}