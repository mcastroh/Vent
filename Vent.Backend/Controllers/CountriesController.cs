using Microsoft.AspNetCore.Mvc;
using Vent.Services.Interfaces;
using Vent.Shared.Entities;

namespace Vent.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : ControllerBase
{
    private readonly ICountryService _countryService;

    public CountriesController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Country>>> GetAsync()
    {
        var countryList = await _countryService.GetAsync();
        return Ok(countryList);
    }

    [HttpGet("getAll")]
    public async Task<ActionResult<IEnumerable<Country>>> GetAllAsync()
    {
        var countryList = await _countryService.GetAllAsync();
        return Ok(countryList);
    }
}