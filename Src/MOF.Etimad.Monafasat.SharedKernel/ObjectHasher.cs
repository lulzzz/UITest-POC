using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.SharedKernel
{
    public class ObjectHasher
    {
        private static readonly int startValue = 17;
        private static readonly int multiplier = 59;

        public int ComputedHash { get; private set; }

        public static int GetHashCode(params object[] objects)
        {
            int hashCode = startValue;

            unchecked
            {
                foreach (var field in objects)
                {
                    if (field != null)
                    {
                        var generic = field as IEnumerable<object>;
                        if (generic != null)
                        {
                            foreach (var value in generic)
                            {
                                hashCode = (hashCode * multiplier) + value.GetHashCode();
                            }
                        }
                        else
                        {
                            hashCode = (hashCode * multiplier) + field.GetHashCode();
                        }
                    }
                }
            }

            return hashCode;
        }

        public ObjectHasher()
        {
            ComputedHash = startValue;
        }
    }
}
