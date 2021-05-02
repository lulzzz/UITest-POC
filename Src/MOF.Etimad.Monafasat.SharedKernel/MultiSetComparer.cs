using System.Collections.Generic;
using System.Linq;

namespace MOF.Etimad.Monafasat.SharedKernel
{
    /// <summary>
    /// Equality comparer for Enumerable of Type T, The equality check
    /// passes if both sets have the same items despite of the order.
    /// </summary>
    /// <typeparam name="T">Enumerable type</typeparam>
    public class MultiSetComparer<T> : IEqualityComparer<IEnumerable<T>>
    {
        private MultiSetCompareMethod compareMethod;

        /// <summary>
        /// Create MultiSetComparer with the default MultiSetCompareMethod.Containment
        /// </summary>
        public MultiSetComparer()
        {
            compareMethod = MultiSetCompareMethod.Containment;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="compareMethod">Compare method</param>
        public MultiSetComparer(MultiSetCompareMethod compareMethod)
        {
            this.compareMethod = compareMethod;
        }

        public bool Equals(IEnumerable<T> first, IEnumerable<T> second)
        {
            if (first == null)
                return second == null;

            if (second == null)
                return false;

            if (ReferenceEquals(first, second))
                return true;

            var firstCollection = first as ICollection<T>;
            var secondCollection = second as ICollection<T>;
            if (firstCollection != null && secondCollection != null)
            {
                if (firstCollection.Count != secondCollection.Count)
                    return false;

                if (firstCollection.Count == 0)
                    return true;
            }

            if (compareMethod == MultiSetCompareMethod.Sequence)
            {
                return firstCollection.SequenceEqual(secondCollection);
            }

            return firstCollection.All(secondCollection.Contains);
        }

        /// <summary>
        /// Get the comulated Hash Code from all elements inside the set
        /// </summary>
        /// <param name="enumerable">The set</param>
        /// <returns></returns>
        public int GetHashCode(IEnumerable<T> enumerable)
        {
            return ObjectHasher.GetHashCode(enumerable);
        }
    }

    public enum MultiSetCompareMethod
    {
        /// <summary>
        /// The two sets are equals if they are contains the same elements
        /// despite of order
        /// </summary>
        Containment,
        /// <summary>
        /// The two sets are equals if they are contains the same elements
        /// with the same order
        /// </summary>
        Sequence
    }
}