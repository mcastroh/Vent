using Vent.Repositories.Interfaces;
using Vent.Services.Interfaces;
using Vent.Shared.Dtos;
using Vent.Shared.Entities;
using Vent.Shared.Responses;

namespace Vent.Services.Implementations;

public class CountryService : ICountryService
{
    private readonly ICountryRepository _countryRepository;

    public CountryService(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }

    public async Task<Response> AddAsync(Country entity)
        => await _countryRepository.AddAsync(entity);

    public async Task<Response> DeleteAsync(int id)
    => await _countryRepository.DeleteAsync(id);

    public async Task<Response> GetAsync(PaginationDto pagination)
        => await _countryRepository.GetAsync(pagination);

    public async Task<Response> GetByIdAsync(int id)
        => await _countryRepository.GetByIdAsync(id);

    public async Task<Response> UpdateAsync(Country entity)
     => await _countryRepository.UpdateAsync(entity);
}