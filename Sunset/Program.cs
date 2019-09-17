using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunset
{
    class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                // 50.6611° N, 14.0531° E Ústí nad Labem
                // 34.9011° S, 56.1645° Montevideo
                var lat = 50.6611;
                var lon = 14.0531;
                var date = new DateTime(2019, 9, 17);

                Sun.SunRise(out var rise, out var set, lat, lon, date);
                Console.WriteLine($"sun rise {Utils.GetTimeFromDayFraction(rise)} set {Utils.GetTimeFromDayFraction(set)}");
            }
            catch (Exception ex)
            {
                var fullname = System.Reflection.Assembly.GetEntryAssembly().Location;
                var progname = Path.GetFileNameWithoutExtension(fullname);
                Console.Error.WriteLine(progname + ": Error: " + ex.Message);
            }

        }

        private static void SumSeries2()
        {
            var date = new DateTime(2019, 9, 16);
            double jde = Utils.GetJulianDate(date);

            double tau = (jde - 2451545.0) / 365250;

            double l, b, r;

            // get heliocentric coordinates for earth from VSOP series:
            l = VSop87d.SumSeries(VSOPEarth.Along, tau); // radians
            b = VSop87d.SumSeries(VSOPEarth.Alat, tau);  // radians
            r = VSop87d.SumSeries(VSOPEarth.ARad, tau);  // astronomical units
        }
    }
}
