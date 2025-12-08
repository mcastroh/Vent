using Vent.Repositories.Interfaces;
using Vent.Services.Interfaces;
using Vent.Shared.Dtos;
using Vent.Shared.Responses;

namespace Vent.Services.Implementations;

public class CountryService : ICountryService
{
    private readonly ICountryRepository _countryRepository;

    public CountryService(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }

    public async Task<Response> GetAsync(PaginationDto pagination)
        => await _countryRepository.GetAsync(pagination);
}