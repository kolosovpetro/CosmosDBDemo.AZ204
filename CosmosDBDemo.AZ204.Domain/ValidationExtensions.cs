using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using FluentValidation;
using FluentValidation.Results;

namespace CosmosDBDemo.AZ204.Domain;

public static class ValidationExtensions
{
    public static void ValidateAndThrowException<T>(this AbstractValidator<T> validator, T obj)
    {
        var validationResult = validator.Validate(obj);


        if (validationResult.IsValid)
        {
            return;
        }

        var dictionary = validationResult.Errors
            .GroupBy((Func<ValidationFailure, string>)(x => x.PropertyName))
            .ToDictionary(
                (Func<IGrouping<string, ValidationFailure>, string>)(x => x.Key),
                (Func<IGrouping<string, ValidationFailure>, string[]>)(x =>
                    x.Select((Func<ValidationFailure, string>)(y => y.ErrorMessage)).ToArray()));

        var interpolatedStringHandler = new DefaultInterpolatedStringHandler(21, 1);

        interpolatedStringHandler.AppendLiteral("Invalid model state. ");

        interpolatedStringHandler.AppendFormatted(validationResult);

        var stringAndClear = interpolatedStringHandler.ToStringAndClear();

        throw new MovieValidationException(dictionary, stringAndClear);
    }
}