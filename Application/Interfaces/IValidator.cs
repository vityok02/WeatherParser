﻿using Domain;

namespace Application.Interfaces;

public interface IValidator<T> where T : class
{
    ValidationResult Validate(T instance);
}
