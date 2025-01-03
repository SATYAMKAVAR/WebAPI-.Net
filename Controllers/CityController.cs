﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        #region CityRepository , Validator
        private readonly CityRepository _cityRepository;

        private readonly IValidator<CityModel> _validator;

        public CityController(CityRepository cityRepository , IValidator<CityModel> validator)
        {
            _cityRepository = cityRepository;
            _validator = validator;
        }
        #endregion

        #region GetAllCities

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
        #endregion

        #region DeleteCity

        [HttpDelete("{id}")]
        public IActionResult DeleteCity(int id)
        {
            var isDeleted = _cityRepository.Delete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion

        #region InsertCity
        [HttpPost]
        public IActionResult InsertCity([FromBody] CityModel city)
        {
            var validationResult = _validator.Validate(city);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (city == null)
                return BadRequest();

            bool isInserted = _cityRepository.Insert(city);

            if (isInserted)
                return Ok(new { Message = "City inserted successfully!" });

            return StatusCode(500, "An error occurred while inserting the city.");
        }
        #endregion

        #region UpdateCity

        [HttpPut("{id}")]
        public IActionResult UpdateCity(int id, [FromBody] CityModel city)
        {
            var validationResult = _validator.Validate(city);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (city == null || id != city.CityID)
                return BadRequest();

            var isUpdated = _cityRepository.Update(city);
            if (!isUpdated)
                return NotFound();

            return NoContent();
        }
        #endregion

        #region GetCountries

        [HttpGet("countries")]
        public IActionResult GetCountries()
        {
            var countries = _cityRepository.GetCountries();
            if (!countries.Any())
                return NotFound("No countries found.");

            return Ok(countries);
        }
        #endregion

        #region GetStatesByCountryID

        [HttpGet("states/{countryID}")]
        public IActionResult GetStatesByCountryID(int countryID)
        {
            if (countryID <= 0)
                return BadRequest("Invalid CountryID.");

            var states = _cityRepository.GetStatesByCountryID(countryID);
            if (!states.Any())
                return NotFound("No states found for the given CountryID.");

            return Ok(states);
        }
        #endregion

        #region GetCity

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            try
            {
                // Fetch the city using the city ID from the repository or service
                var city = _cityRepository.GetCity(id);

                if (city == null)
                {
                    return NotFound(new { message = "City not found." });
                }

                return Ok(JsonConvert.SerializeObject(city));
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return StatusCode(500, new { message = "An error occurred while fetching the city.", error = ex.Message });
            }
        }

        #endregion

    }

}
