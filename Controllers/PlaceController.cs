using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelTours.Core.Entities;
using TravelTours.Core.Especification;
using TravelTours.Core.Interfaces;
using TravelTours.Dtos;

namespace TravelTours.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PlaceController : ControllerBase
    {
        private readonly IPlaceRepository _placeRepository;
        private readonly IRepository<Place> placeRepo;
        private readonly IMapper mapper;

        public PlaceController(IRepository<Place> PlaceRepo, IMapper mapper, IPlaceRepository placeRepository)
        {

            placeRepo = PlaceRepo;
            this.mapper = mapper;
            _placeRepository = placeRepository;
        }
        //READ PLACE 
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<PlaceDto>>> GetPlaces()
        {
            var espec = new PlaceEspecification();
            var places = await placeRepo.GetAllEspecification(espec);
            return Ok(mapper.Map<IReadOnlyList<Place>, IReadOnlyList<PlaceDto>>(places));
        }
        //READ PLACE BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<PlaceDto>> GetPlace(int id)
        {
            var espec = new PlaceEspecification(id);
            var place = await placeRepo.GetEspecification(espec);
            return mapper.Map<Place, PlaceDto>(place);
        }
        //CREATE PLACE
        [HttpPost]
        public async Task<ActionResult<Place>> AddPlace(Place place)
        {
            var placeResult = await _placeRepository.AddPlace(place);
            return Ok(placeResult);
        }
        //UPDATE PLACE
        [HttpPut("{id}")]
        public async Task<ActionResult<Place>> UpdatePlace(int id, Place place)
        {
            try
            {
                if (id != place.Id)
                {
                    return BadRequest("place Id mismatch");
                }
                var countryResult = await _placeRepository.UpdatePlace(place);
                if (countryResult != null)
                {
                    return NotFound($"Place with Id = {id} not found");
                }
                return await _placeRepository.UpdatePlace(place);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Update place record");
            }
        }
        //DELETE PLACE
        [HttpDelete("{id}")]
        public async Task<ActionResult<Place>> DeletePlace(int id)
        {
            try
            {
                var deletePlace = await _placeRepository.GetPlaceById(id);
                if(deletePlace != null)
                {
                    return NotFound($"Place with Id = {id} not found");
                }
                await _placeRepository.DeletePlace(id);
                return Ok($"Place with Id = {id} deleted");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error delete place record");
            }
        }
       
    }
}


