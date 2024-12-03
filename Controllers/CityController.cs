using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly CItyRepository _cityRepository;

        public CityController(CItyRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        [HttpGet]
        public IActionResult GetAllCities()
        {
            try
            {
                // Fetch all cities using the repository
                var cities = _cityRepository.GetAllCities();

                // Return the serialized list of cities as JSON
                return Ok(JsonConvert.SerializeObject(cities));
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a proper error response
                return StatusCode(500, new { message = "An error occurred while fetching cities.", error = ex.Message });
            }
        }
    }
}
