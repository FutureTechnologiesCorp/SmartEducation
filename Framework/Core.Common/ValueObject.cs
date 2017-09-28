using System;
namespace Core.Common
{
    public abstract class ValueObject<T> where T: ValueObject<T>
    {
        public override bool Equals(object obj)
        {
            var valueObject = obj as T;
            if (ReferenceEquals(valueObject, null))
            {
                return false;
            }

            return EqualCore(valueObject);
        }

        protected abstract bool EqualCore(T other);

        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }

        public abstract int GetHashCodeCore();

        public static bool operator ==(ValueObject<T> firstObject, ValueObject<T> secondObject)
        {
            if (ReferenceEquals(firstObject, null) && ReferenceEquals(secondObject, null))
            {
                return true;
            }

			if (ReferenceEquals(firstObject, null) || ReferenceEquals(secondObject, null))
			{
                return false;
			}

            return firstObject.Equals(secondObject);
        }

        public static bool operator !=(ValueObject<T> firstObject, ValueObject<T> secondObject)
        {
            return !(firstObject == secondObject);
        }
    }
}
