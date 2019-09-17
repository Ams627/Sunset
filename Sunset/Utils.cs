using System;

namespace Sunset
{
    static class Utils
    {
        /// <summary>
        /// Given a DateTime, return a Julian Date. The time component of the input date is ignored.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static double GetJulianDate(DateTime date)
        {
            var d = date.Day;
            var m = date.Month;
            var y = date.Year;
            var serial = (1461 * (y + 4800 + (m - 14) / 12)) / 4 +
                 (367 * (m - 2 - 12 * ((m - 14) / 12))) / 12 -
                 (3 * ((y + 4900 + (m - 14) / 12) / 100)) / 4 + d - 32075;

            double seconds = date.Hour * 3600 + date.Minute * 60 + date.Second;
            return serial - 0.5 + seconds / 86400.0;
        }
        
        /// <summary>
        /// Given a double as a fraction of a day (i.e. in the range 0.0->1.0), calculate the time
        /// and return as hours, minutes and seconds:
        /// </summary>
        /// <param name="h">Output - the hour of the day</param>
        /// <param name="m">Output - the minute of the hour</param>
        /// <param name="s">Output - the second of the minute</param>
        /// <param name="dayFraction">The input in the range zero to one</param>
        public static void GetTimeFromDayFraction(out int h, out int m, out int s, double dayFraction)
        {
            var totalSeconds = (int)(dayFraction * 86400.0 + 0.5);
            h = totalSeconds / 3600;
            m = totalSeconds % 3600 / 60;
            s = (totalSeconds % 3600) % 60;
        }

        public static string GetTimeFromDayFraction(double dayFraction)
        {
            var totalSeconds = (int)(dayFraction * 86400.0 + 0.5);
            var h = totalSeconds / 3600;
            var m = totalSeconds % 3600 / 60;
            var s = (totalSeconds % 3600) % 60;
            return $"{h:D2}:{m:D2}:{s:D2}";
        }
    }
}
