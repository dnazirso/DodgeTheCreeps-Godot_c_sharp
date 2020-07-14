using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Direction
{
    /// <summary>
    /// Enumaration abstraction class instead of Enums
    /// </summary>
    public abstract class EnumerationBase : IComparable
    {
        public IDirection Direction { get; private set; }

        public string UiDirection { get; private set; }

        protected EnumerationBase(string UiDirection, IDirection Direction)
        {

            this.UiDirection = UiDirection;
            this.Direction = Direction;
        }

        public override string ToString() => nameof(Direction);

        public static IEnumerable<T> GetAll<T>() where T : EnumerationBase
        {
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is EnumerationBase otherValue))
                return false;

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = UiDirection.Equals(otherValue.UiDirection);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => base.GetHashCode();

        public int CompareTo(object obj) => UiDirection.CompareTo(((EnumerationBase)obj).UiDirection);
    }

}
