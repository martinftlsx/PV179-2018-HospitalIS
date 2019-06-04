using HospitalIS.Infrastructure.Query.Predicates.Operators;
using System;

namespace HospitalIS.Infrastructure.Query.Predicates
{
    public class SimplePredicate : IPredicate
    {
        public string TargetPropertyName { get; }

        public ValueComparingOperator ValueComparingOperator { get; }

        public object ComparedValue { get; }

        public SimplePredicate(string targetPropertyName, ValueComparingOperator valueComparingOperator, object comparedValue)
        {
            if (valueComparingOperator == ValueComparingOperator.None) throw new ArgumentException("ValueComparingOperator must be defined in simple predicate.");
            if (string.IsNullOrWhiteSpace(targetPropertyName)) throw new ArgumentException("Target property name must be defined.");
            TargetPropertyName = targetPropertyName;
            ValueComparingOperator = valueComparingOperator;
            ComparedValue = comparedValue;
        }

        protected bool Equals(SimplePredicate other)
        {
            return string.Equals(TargetPropertyName, other.TargetPropertyName) && Equals(ComparedValue, other.ComparedValue) && ValueComparingOperator == other.ValueComparingOperator;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return obj.GetType() == this.GetType() && Equals((SimplePredicate)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = TargetPropertyName != null ? TargetPropertyName.GetHashCode() : 0;
                hashCode = (hashCode * 1013) ^ (ComparedValue != null ? ComparedValue.GetHashCode() : 0);
                hashCode = (hashCode * 1013) ^ (int)ValueComparingOperator;
                return hashCode;
            }
        }
    }
}
