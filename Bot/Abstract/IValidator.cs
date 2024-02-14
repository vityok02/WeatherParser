using System.ComponentModel.DataAnnotations;

namespace Bot.Abstract;

public interface IValidator<T> where T : class
{
    ValidationResult Validate(T instance);
}
