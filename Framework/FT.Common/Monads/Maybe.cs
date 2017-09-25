using System;
namespace FT.Common.Monads
{
    public struct Maybe<T> : IEquatable<Maybe<T>> where T : class
    {
        private readonly T _value;

        public T Value
        {
            get
            {
                if (HasNoValue)
                {
                    throw new InvalidOperationException();
                }

                return _value;
            }
        }

        public bool HasValue => _value != null;
        public bool HasNoValue => HasValue == false;

        public Maybe(T value)
        {
            _value = value;
        }

        public static implicit operator Maybe<T>(T value)
        {
            return new Maybe<T>(value);
        }

        public static bool operator ==(Maybe<T> maybe, T value)
        {
            if (maybe.HasNoValue)
            {
                return false;
            }

            return maybe.Value.Equals(value);
        }

        public static bool operator !=(Maybe<T> maybe, T value)
        {
            return (maybe.Value == value);
        }

        public static bool operator ==(Maybe<T> first, Maybe<T> second)
		{
            return first.Equals(second);
		}

		public static bool operator !=(Maybe<T> first, Maybe<T> second)
		{
            return !(first == second);
		}

        public override bool Equals(object obj)
        {
            if(!(obj is Maybe<T>))
            {
                return false;
            }

            var other = (Maybe<T>)obj;
            return Equals(other);
        }

        public bool Equals(Maybe<T> other)
        {
            if (HasNoValue && other.HasNoValue)
            {
                return true;
            }

            if (HasNoValue || other.HasNoValue)
            {
                return false;
            }

            return _value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public T Unwrap(T defaultValue = default(T))
        {
            if(HasValue)
            {
                return Value;
            }

            return defaultValue;
        }
    }
}
