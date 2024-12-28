using FluentValidation;
using WebAPI.Models;

namespace WebAPI
{
    public class CountryModelValidator : AbstractValidator<CountryModel>
    {
        public CountryModelValidator()
        {
            // Validate CountryName is not empty and has a minimum length of 3
            RuleFor(country => country.CountryName)
                .NotEmpty().WithMessage("Country Name is required.");

            // Validate CountryCode is not empty and should have a fixed length (e.g., 2 or 3 characters)
            RuleFor(country => country.CountryCode)
                .NotEmpty().WithMessage("Country Code is required.");
        }
    }
    public class StateModelValidator : AbstractValidator<StateModel>
    {
        public StateModelValidator()
        {
            // Validate CountryID (must always be provided and greater than 0)
            RuleFor(state => state.CountryID)
                .GreaterThan(0).WithMessage("Country ID is required and must be greater than 0.");

            // Validate StateName (required and must have a length between 3 and 100 characters)
            RuleFor(state => state.StateName)
                .NotEmpty().WithMessage("State Name is required.");

            // Validate StateCode (required and must have exactly 2-3 characters)
            RuleFor(state => state.StateCode)
                .NotEmpty().WithMessage("State Code is required.");
        }
    }
    public class CityModelValidator : AbstractValidator<CityModel>
    {
        public CityModelValidator()
        {
            // Validate CityName (required and must have a length between 3 and 100 characters)
            RuleFor(city => city.CityName)
                .NotEmpty().WithMessage("City Name is required.");

            // Validate CountryID (required and greater than 0)
            RuleFor(city => city.CountryID)
                .GreaterThan(0).WithMessage("Country ID is required and must be greater than 0.");

            // Validate StateID (required and greater than 0)
            RuleFor(city => city.StateID)
                .GreaterThan(0).WithMessage("State ID is required and must be greater than 0.");

            // Validate CityCode (required and must have exactly 2-3 characters)
            RuleFor(city => city.CityCode)
                .NotEmpty().WithMessage("City Code is required.");
        }
    }
}
