using WeatherParser.Abstract;
using WeatherParser.Models;

namespace WeatherParser.Features.Weather;

public class WeatherValidator : IValidator<Models.Weather>
{
    public ValidationResult Validate(Models.Weather? weather)
    {
        var validationResult = new ValidationResult();

        var errors = validationResult.Errors;

        if (weather is null)
        {
            errors.Add("Failed to get weather");
            return validationResult;
        }
        if (weather.CurrentTemperature is null)
        {
            errors.Add("Failed to get the current temperature");
        }
        if (weather.MinTemperature is null)
        {
            errors.Add("Failed to get the min temperature");
        }
        if (weather.MaxTemperature is null)
        {
            errors.Add("Failed to get the max temperature");
        }
        if (weather.Location is null)
        {
            errors.Add("Failed to get the location");
        }
        if (weather.ObservationTime is null)
        {
            errors.Add("Failed to get the observation time");
        }

        return validationResult;
    }
}
