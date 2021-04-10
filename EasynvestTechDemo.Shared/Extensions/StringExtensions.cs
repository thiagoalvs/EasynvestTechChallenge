using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EasynvestTechDemo.Shared.Extensions
{
    public static class StringExtensions
    {
        public static DateTime ToDateTime(this string content, string format = "yyyy-MM-ddTHH:mm:ss")
        {
            if (!DateTime.TryParseExact(content, format, null, DateTimeStyles.None, out DateTime result))
                throw new InvalidOperationException($"{content} não é uma data valida");

            return result;
        }
    }
}
