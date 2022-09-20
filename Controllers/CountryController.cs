using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelTours.Core.Entities;
using TravelTours.Core.Interfaces;

namespace TravelTours.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {   
        private readonly ICountryRepository _countryRepository;
        private readonly IRepository<Country> countryRepo;
        private readonly IMapper mapper;

        public CountryController(IRepository<Country> countryRepo, IMapper mapper, ICountryRepository countryRepository)
        {
            this.countryRepo = countryRepo;
            this.mapper = mapper;
            _countryRepository = countryRepository;
        }
        //READ COUNTRIES
        [HttpGet("Countries")]
        public async Task<ActionResult<Country>> GetCountries()
        {
            return Ok(await countryRepo.GetAllASync());
        }
        //READ COUNTRY BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            try
            {
                var getCountry = await _countryRepository.GetCountryById(id);
                if(getCountry == null)
                {
                    return NotFound();
                }
                return getCountry;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        //CREATE COUNTRY
        [HttpPost]
        public async Task<ActionResult<Country>> CreateCountry(Country country)
        {
            try { 
                if(country == null)
                {
                    return BadRequest();
                }
                var countryResult = await _countryRepository.AddCountry(country);
                return CreatedAtAction(nameof(CreateCountry),new {Id =countryResult.Id},countryResult);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new country record");
            }
        }
        //UPDATE COUNTRY
        [HttpPut("{id}")]
        public async Task<ActionResult<Country>> UpdateCountry(int id,Country country)
        {
            try { 
            if(id != country.Id)
            {
                return BadRequest("country Id mismatch");
            }
            var countryResult = await _countryRepository.UpdateCountry(country);
                if(countryResult != null)
                {
                    return NotFound($"Country with Id = {id} not found");
                }
                return await _countryRepository.UpdateCountry(country);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Update country record");
            }
        }
        //DELETE COUNTRY
        [HttpDelete("{id}")]
        public async Task<ActionResult<Country>> DeleteCountry(int id)
        {
            try
            {
                var countrydelete = await _countryRepository.GetCountryById(id);
                if(countrydelete == null)
                {
                    return NotFound($"Country with Id = {id} not found");
                }
                await _countryRepository.DeleteCountry(id);
                return Ok($"Country with Id = {id} deleted");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error delete country record");
            }
        }

    }
}
