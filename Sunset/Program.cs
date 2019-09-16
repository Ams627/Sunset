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
                // SumSeries2();
                // 50.6611° N, 14.0531° E Ústí nad Labem
                // 34.9011° S, 56.1645° Montevideo
                var lat = 69;
                var lon = 7;
                var date = new DateTime(2019, 6, 16);

                Sun.SunRise(out var rise, out var set, lat, lon, date);
                rise *= 24.0;
                set *= 24.0;
                Console.WriteLine($"sun rise {(int)(rise)}:{(int)((rise - Math.Truncate(rise)) * 60)} set {(int)(set)}:{(int)((set - Math.Truncate(set)) * 60)}");
                Console.WriteLine();
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
