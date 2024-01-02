﻿using System.Text.Json.Serialization;

namespace Library.Application
{
    public sealed record Error(
        string Code,
        string? Description = null)
    {
        public static readonly Error None = new(string.Empty);

        public static implicit operator Result(Error error) => Result.Failure(error);
    }

    public class Result
    {
        [JsonIgnore] public bool IsSuccess { get; }
        [JsonIgnore] public bool IsFailure => !IsSuccess;
        public Error Error { get; }

        private Result(
            bool isSuccess,
            Error error)
        {
            if (isSuccess && error != Error.None ||
                !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid Error", nameof(error));
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new(true, Error.None);

        public static Result Failure(Error error) => new(false, error);
    }
}
