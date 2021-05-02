using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MOF.Etimad.SharedKernel
{
    public static class EnumFactory
    {
        public static string ToDisplay<TEnum>(TEnum value)
        {
            var attributes = (DisplayAttribute[])value.GetType().GetField(value.ToString())
                .GetCustomAttributes(typeof(DisplayAttribute), false);
            return attributes.Length > 0 ? attributes[0].Name : value.ToString();
        }

        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
