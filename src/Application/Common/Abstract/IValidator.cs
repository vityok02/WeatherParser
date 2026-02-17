using Domain.Abstract;

namespace Application.Common.Abstract;

public interface IValidator<T> where T : class
{
    ValidationResult Validate(T instance);
}
