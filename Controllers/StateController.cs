using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly StateRepository _stateRepository;
        public StateController(StateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }
        [HttpGet]
        public IActionResult GetAllStates()
        {
            try
            {
                // Fetch all cities using the repository
                var states = _stateRepository.GetAllStates();
                // Return the serialized list of cities as JSON
                return Ok(JsonConvert.SerializeObject(states));
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a proper error response
                return StatusCode(500, new { message = "An error occurred while fetching states.", error = ex.Message });
            }
        }
    }
}
