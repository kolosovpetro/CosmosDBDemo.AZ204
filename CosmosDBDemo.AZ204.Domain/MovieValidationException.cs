using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CosmosDBDemo.AZ204.Domain;

public class MovieValidationException : Exception
{
    public string Code { get; }

    public Dictionary<string, string[]> Errors { get; }


    public MovieValidationException()
    {
    }

    public MovieValidationException(string code) => this.Code = code;

    public MovieValidationException(Dictionary<string, string[]> errors, string message)
        : this("invalid_model_state", message)
    {
        Errors = errors;
    }

    public MovieValidationException(string message, params object[] args)
        : this(string.Empty, message, args)
    {
    }

    private MovieValidationException(string code, string message, params object[] args)
        : this(null, code, message, args)
    {
    }

    public MovieValidationException(
        Exception innerException, 
        string message, params object[] args) : this(innerException, string.Empty, message, args)
    {
    }

    private MovieValidationException(
        Exception innerException,
        string code,
        string message,
        params object[] args)
        : base(string.Format(message, args), innerException)
    {
        Code = code;
    }
}