using System;
using System.ComponentModel.DataAnnotations;

namespace FinancasApp.Attributes;

public class RequiredIfAttribute : ValidationAttribute
{
    private readonly string _dependentProperty;
    private readonly object _targetValue;

    public RequiredIfAttribute(string dependentProperty, object targetValue)
    {
        _dependentProperty = dependentProperty;
        _targetValue = targetValue;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var property = validationContext.ObjectType.GetProperty(_dependentProperty);
        if (property == null)
        {
            return new ValidationResult($"Property '{_dependentProperty}' not found.");
        }

        var dependentValue = property.GetValue(validationContext.ObjectInstance);

        if (dependentValue != null && dependentValue.Equals(_targetValue) && value == null)
        {
            return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} is required.");
        }

        return ValidationResult.Success;
    }

}
