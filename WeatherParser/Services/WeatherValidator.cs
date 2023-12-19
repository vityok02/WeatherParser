using System.Text;
using WeatherParser.Models;

namespace WeatherParser.Services;

public interface IWeatherValidator
{
    ValidationResult Validate(Weather? weather);
}

public class WeatherValidator : IWeatherValidator
{
    public ValidationResult Validate(Weather? weather)
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

public class ValidationResult
{
    public List<string> Errors { get; set; } = new List<string>();
    public bool IsValid => Errors.Count == 0;

    public override string ToString()
    {
        var builder = new StringBuilder();

        foreach (var error in Errors)
        {
            builder.AppendLine(error);
        }

        return builder.ToString();
    }
}
