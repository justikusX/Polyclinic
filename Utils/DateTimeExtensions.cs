using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Polyclinic.Utils
{
    public static class DateTimeExtensions
    {
        public static string ToShortDateString(this DateTime? dateTime)
        {
            return dateTime?.ToShortDateString() ?? string.Empty;
        }
    }
}