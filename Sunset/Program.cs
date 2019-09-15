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
                var lat = 50.0755;
                var lon = 14.4378;
                var date = DateTime.Today;
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
    }
}
