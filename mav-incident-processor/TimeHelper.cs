using System;
using System.Collections.Generic;
using System.Text;

namespace mav_incident_processor
{
    static class TimeHelper
    {

        private static Dictionary<string, int> MONTHS = new Dictionary<string, int>()
        {
            { "január", 1 },
            { "február", 2 },
            { "március", 3 },
            { "április", 4 },
            { "május", 5 },
            { "június", 6 },
            { "július", 7 },
            { "augusztus", 8 },
            { "szeptember", 9 },
            { "október", 10 },
            { "november", 11 },
            { "december", 12 }
        };

        public static int? ToMonth(this string monthName)
        {
            if (MONTHS.TryGetValue(monthName, out int month))
                return month;
            return null;
        }

        private static DateTime UNIX_TIMESTAMP_ZERO = new DateTime(1970, 1, 1);

        public static int UnixTimestamp(this DateTime dateTime)
        {
            // @source https://stackoverflow.com/a/17632585/9642069
            return (Int32)(dateTime.Subtract(UNIX_TIMESTAMP_ZERO)).TotalSeconds;
        }

    }
}
