using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAPI.Data;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly CountryRepository _CountryRepository;

        public CountryController(CountryRepository CountryRepositry)
        {
            _CountryRepository = CountryRepositry;
        }

        [HttpGet]
        public IActionResult GetAllCountries()
        {
            try
            {
                // Fetch all cities using the repository
                var countries = _CountryRepository.GetAllCountries();
                // Return the serialized list of cities as JSON
                return Ok(JsonConvert.SerializeObject(countries));
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a proper error response
                return StatusCode(500, new { message = "An error occurred while fetching countries.", error = ex.Message });
            }
        }
    }
}
