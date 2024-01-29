using WeatherParser.Models;

namespace WeatherParser.Abstract;

public interface IValidator<T> where T : class
{
    ValidationResult Validate(T instance);
}
