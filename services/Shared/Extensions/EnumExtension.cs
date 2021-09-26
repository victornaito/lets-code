using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SharedKernel.Extensions
{
    public static class EnumExtension
    {
        public static string ObterDescricaoEnum(Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }

        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}