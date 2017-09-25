using System;
namespace FT.Common
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public string Error { get; private set; }

        protected Result(bool isSuccess, string error)
        {
            if (isSuccess && error != string.Empty)
            {
                throw new InvalidOperationException();
            }

            if (isSuccess == false && error == string.Empty)
            {
                throw new InvalidOperationException();
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Fail(string error)
        {
            return new Result(false, error);
        }

        public static Result<T> Fail<T>(string error)
        {
            return new Result<T>(default(T), false, error);
        }

        public static Result Ok()
        {
            return new Result(true, string.Empty);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, true, string.Empty);
        }
    }

    public class Result<T> : Result
    {
        private readonly T _value;

        public T Value
        {
            get
            {
                if (IsSuccess == false)
                {
                    throw new InvalidOperationException();
                }

                return _value;
            }
        }

        protected internal Result(T value, bool isSucess, string error)
            : base(isSucess, error)
        {
            _value = value;
        }
    }
}
