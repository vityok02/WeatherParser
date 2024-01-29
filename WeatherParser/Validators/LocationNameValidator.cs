using WeatherParser.Extensions;
using WeatherParser.Models;
using WeatherParser.Models.Interfaces;

namespace WeatherParser.Validators;

public class LocationNameValidator : IValidator<string>
{
    public ValidationResult Validate(string locationName)
    {
        var validationResult = new ValidationResult();
        var errors = validationResult.Errors;

        if (locationName.ContainsAnySymbol(['/']))
        {
            errors.Add("");
        }

        return validationResult;
    }
}
