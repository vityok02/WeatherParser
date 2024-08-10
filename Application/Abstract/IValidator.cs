using Domain.Abstract;

namespace Application.Abstract;

public interface IValidator<T> where T : class
{
    ValidationResult Validate(T instance);
}
