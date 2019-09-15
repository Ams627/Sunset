using System;
using static System.Math;

namespace Sunset
{
    class NutationArgs
    {
        public double D { get; set; }
        public double M { get; set; }
        public double MDash { get; set; }
        public double F { get; set; }
        public double Omega { get; set; }
    }

    class NutationCoefficients
    {
        public double Lon0 { get; set; }
        public double Lon1 { get; set; }
        public double Obl0 { get; set; }
        public double Obl1 { get; set; }
    }

    static class Nutation
    {

        static readonly NutationArgs[] nutationArgs = {
            new NutationArgs {D = 0.0, M = 0.0,  MDash = 0.0,  F = 0.0, Omega= 1.0},
            new NutationArgs {D = -2.0,      M = 0.0,   MDash = 0.0,    F = 2.0,   Omega = 2.0},
            new NutationArgs {D = 0.0,       M = 0.0,   MDash = 0.0,    F = 2.0,   Omega = 2.0},
            new NutationArgs {D = 0.0,       M = 0.0,   MDash = 0.0,    F = 0.0,   Omega = 2.0},
            new NutationArgs {D = 0.0,       M = 1.0,   MDash = 0.0,    F = 0.0,   Omega = 0.0},
            new NutationArgs {D = 0.0,       M = 0.0,   MDash = 1.0,    F = 0.0,   Omega = 0.0},
            new NutationArgs {D = -2.0,      M = 1.0,   MDash = 0.0,    F = 2.0,   Omega = 2.0},
            new NutationArgs {D = 0.0,       M = 0.0,   MDash = 0.0,    F = 2.0,   Omega = 1.0},
            new NutationArgs {D = 0.0,       M = 0.0,   MDash = 1.0,    F = 2.0,   Omega = 2.0},
            new NutationArgs {D = -2.0,      M = -1.0,  MDash = 0.0,    F = 2.0,   Omega = 2.0},
            new NutationArgs {D = -2.0,      M = 0.0,   MDash = 1.0,    F = 0.0,   Omega = 0.0},
            new NutationArgs {D = -2.0,      M = 0.0,   MDash = 0.0,    F = 2.0,   Omega = 1.0},
            new NutationArgs {D = 0.0,       M = 0.0,   MDash = -1.0,   F = 2.0,   Omega = 2.0},
            new NutationArgs {D = 2.0,       M = 0.0,   MDash = 0.0,    F = 0.0,   Omega = 0.0},
            new NutationArgs {D = 0.0,       M = 0.0,   MDash = 1.0,    F = 0.0,   Omega = 1.0},
            new NutationArgs {D = 2.0,       M = 0.0,   MDash = -1.0,   F = 2.0,   Omega = 2.0},
            new NutationArgs {D = 0.0,       M = 0.0,   MDash = -1.0,   F = 0.0,   Omega = 1.0},
            new NutationArgs {D = 0.0,       M = 0.0,   MDash = 1.0,    F = 2.0,   Omega = 1.0},
            new NutationArgs {D = -2.0,      M = 0.0,   MDash = 2.0,    F = 0.0,   Omega = 0.0},
            new NutationArgs {D = 0.0,       M = 0.0,   MDash = -2.0,   F = 2.0,   Omega = 1.0},
            new NutationArgs {D = 2.0,       M = 0.0,   MDash = 0.0,    F = 2.0,   Omega = 2.0},
            new NutationArgs {D = 0.0,       M = 0.0,   MDash = 2.0,    F = 2.0,   Omega = 2.0},
            new NutationArgs {D = 0.0,       M = 0.0,   MDash = 2.0,    F = 0.0,   Omega = 0.0},
            new NutationArgs {D = -2.0,      M = 0.0,   MDash = 1.0,    F = 2.0,   Omega = 2.0},
            new NutationArgs {D = 0.0,       M = 0.0,   MDash = 0.0,    F = 2.0,   Omega = 0.0},
            new NutationArgs {D = -2.0,      M = 0.0,   MDash = 0.0,    F = 2.0,   Omega = 0.0},
            new NutationArgs {D = 0.0,       M = 0.0,   MDash = -1.0,   F = 2.0,   Omega = 1.0},
            new NutationArgs {D = 0.0,       M = 2.0,   MDash = 0.0,    F = 0.0,   Omega = 0.0},
            new NutationArgs {D = 2.0,       M = 0.0,   MDash = -1.0,   F = 0.0,   Omega = 1.0},
            new NutationArgs {D = -2.0,      M = 2.0,   MDash = 0.0,    F = 2.0,   Omega = 2.0},
            new NutationArgs {D = 0.0,       M = 1.0,   MDash = 0.0,    F = 0.0,   Omega = 1.0},
            new NutationArgs {D = -2.0,      M = 0.0,   MDash = 1.0,    F = 0.0,   Omega = 1.0},
            new NutationArgs {D = 0.0,       M = -1.0,  MDash = 0.0,    F = 0.0,   Omega = 1.0},
            new NutationArgs {D = 0.0,       M = 0.0,   MDash = 2.0,    F = -2.0,  Omega = 0.0},
            new NutationArgs {D = 2.0,       M = 0.0,   MDash = -1.0,   F = 2.0,   Omega = 1.0},
            new NutationArgs {D = 2.0,       M = 0.0,   MDash = 1.0,    F = 2.0,   Omega = 2.0},
            new NutationArgs {D = 0.0,       M = 1.0,   MDash = 0.0,    F = 2.0,   Omega = 2.0},
            new NutationArgs {D = -2.0,      M = 1.0,   MDash = 1.0,    F = 0.0,   Omega = 0.0},
            new NutationArgs {D = 0.0,       M = -1.0,  MDash = 0.0,    F = 2.0,   Omega = 2.0},
            new NutationArgs {D = 2.0,       M = 0.0,   MDash = 0.0,    F = 2.0,   Omega = 1.0},
            new NutationArgs {D = 2.0,       M = 0.0,   MDash = 1.0,    F = 0.0,   Omega = 0.0},
            new NutationArgs {D = -2.0,      M = 0.0,   MDash = 2.0,    F = 2.0,   Omega = 2.0},
            new NutationArgs {D = -2.0,      M = 0.0,   MDash = 1.0,    F = 2.0,   Omega = 1.0},
            new NutationArgs {D = 2.0,       M = 0.0,   MDash = -2.0,   F = 0.0,   Omega = 1.0},
            new NutationArgs {D = 2.0,       M = 0.0,   MDash = 0.0,    F = 0.0,   Omega = 1.0},
            new NutationArgs {D = 0.0,       M = -1.0,  MDash = 1.0,    F = 0.0,   Omega = 0.0},
            new NutationArgs {D = -2.0,      M = -1.0,  MDash = 0.0,    F = 2.0,   Omega = 1.0},
            new NutationArgs {D = -2.0,      M = 0.0,   MDash = 0.0,    F = 0.0,   Omega = 1.0},
            new NutationArgs {D = 0.0,       M = 0.0,   MDash = 2.0,    F = 2.0,   Omega = 1.0},
            new NutationArgs {D = -2.0,      M = 0.0,   MDash = 2.0,    F = 0.0,   Omega = 1.0},
            new NutationArgs {D = -2.0,      M = 1.0,   MDash = 0.0,    F = 2.0,   Omega = 1.0},
            new NutationArgs {D = 0.0,       M = 0.0,   MDash = 1.0,    F = -2.0,  Omega = 0.0},
            new NutationArgs {D = -1.0,      M = 0.0,   MDash = 1.0,    F = 0.0,   Omega = 0.0},
            new NutationArgs {D = -2.0,      M = 1.0,   MDash = 0.0,    F = 0.0,   Omega = 0.0},
            new NutationArgs {D = 1.0,       M = 0.0,   MDash = 0.0,    F = 0.0,   Omega = 0.0},
            new NutationArgs {D = 0.0,       M = 0.0,   MDash = 1.0,    F = 2.0,   Omega = 0.0},
            new NutationArgs {D = 0.0,       M = 0.0,   MDash = -2.0,   F = 2.0,   Omega = 2.0},
            new NutationArgs {D = -1.0,      M = -1.0,  MDash = 1.0,    F = 0.0,   Omega = 0.0},
            new NutationArgs {D = 0.0,       M = 1.0,   MDash = 1.0,    F = 0.0,   Omega = 0.0},
            new NutationArgs {D = 0.0,       M = -1.0,  MDash = 1.0,    F = 2.0,   Omega = 2.0},
            new NutationArgs {D = 2.0,       M = -1.0,  MDash = -1.0,   F = 2.0,   Omega = 2.0},
            new NutationArgs {D = 0.0,       M = 0.0,   MDash = 3.0,    F = 2.0,   Omega = 2.0},
            new NutationArgs {D = 2.0,       M = -1.0,  MDash = 0.0,    F = 2.0,   Omega = 2.0}};

        static readonly NutationCoefficients[] nutationCoefficients = new[] {
            new NutationCoefficients {Lon0 = -171996.0, Lon1 = -174.2, Obl0 = 92025.0, Obl1 = 8.9},
            new NutationCoefficients {Lon0 = -13187.0,  Lon1 = -1.6,   Obl0 = 5736.0, Obl1 = -3.1},
            new NutationCoefficients {Lon0 = -2274.0,   Lon1 =  0.2,   Obl0 = 977.0,  Obl1 = -0.5},
            new NutationCoefficients {Lon0 = 2062.0,    Lon1 = 0.2,    Obl0 = -895.0, Obl1 = 0.5},
            new NutationCoefficients {Lon0 = 1426.0,    Lon1 = -3.4,   Obl0 =  54.0,  Obl1 = -0.1},
            new NutationCoefficients {Lon0 = 712.0,     Lon1 = 0.1,    Obl0 = -7.0,   Obl1 = 0.0},
            new NutationCoefficients {Lon0 = -517.0,    Lon1 = 1.2,    Obl0 = 224.0,  Obl1 = -0.6},
            new NutationCoefficients {Lon0 = -386.0,    Lon1 = -0.4,   Obl0 =  200.0, Obl1 = 0.0},
            new NutationCoefficients {Lon0 = -301.0,    Lon1 = 0.0,    Obl0 = 129.0,  Obl1 = -0.1},
            new NutationCoefficients {Lon0 = 217.0,     Lon1 = -0.5,   Obl0 =  -95.0, Obl1 = 0.3},
            new NutationCoefficients {Lon0 = -158.0,    Lon1 = 0.0,    Obl0 = 0.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = 129.0,     Lon1 = 0.1,    Obl0 = -70.0,  Obl1 = 0.0},
            new NutationCoefficients {Lon0 = 123.0,     Lon1 = 0.0,    Obl0 = -53.0,  Obl1 = 0.0},
            new NutationCoefficients {Lon0 = 63.0,      Lon1 = 0.0,    Obl0 = 0.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = 63.0,      Lon1 = 1.0,    Obl0 = -33.0,  Obl1 = 0.0},
            new NutationCoefficients {Lon0 = -59.0,     Lon1 = 0.0,    Obl0 = 26.0,   Obl1 = 0.0},
            new NutationCoefficients {Lon0 = -58.0,     Lon1 = -0.1,   Obl0 = 32.0,   Obl1 = 0.0},
            new NutationCoefficients {Lon0 = -51.0,     Lon1 = 0.0,    Obl0 = 27.0,   Obl1 = 0.0},
            new NutationCoefficients {Lon0 = 48.0,      Lon1 = 0.0,    Obl0 = 0.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = 46.0,      Lon1 = 0.0,    Obl0 = -24.0,  Obl1 = 0.0},
            new NutationCoefficients {Lon0 = -38.0,     Lon1 = 0.0,    Obl0 = 16.0,   Obl1 = 0.0},
            new NutationCoefficients {Lon0 = -31.0,     Lon1 = 0.0,    Obl0 = 13.0,   Obl1 = 0.0},
            new NutationCoefficients {Lon0 = 29.0,      Lon1 = 0.0,    Obl0 = 0.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = 29.0,      Lon1 = 0.0,    Obl0 = -12.0,  Obl1 = 0.0},
            new NutationCoefficients {Lon0 = 26.0,      Lon1 = 0.0,    Obl0 = 0.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = -22.0,     Lon1 = 0.0,    Obl0 = 0.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = 21.0,      Lon1 = 0.0,    Obl0 = -10.0,  Obl1 = 0.0},
            new NutationCoefficients {Lon0 = 17.0,      Lon1 = -0.1,   Obl0 = 0.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = 16.0,      Lon1 = 0.0,    Obl0 = -8.0,   Obl1 = 0.0},
            new NutationCoefficients {Lon0 = -16.0,     Lon1 = 0.1,    Obl0 = 7.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = -15.0,     Lon1 = 0.0,    Obl0 = 9.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = -13.0,     Lon1 = 0.0,    Obl0 = 7.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = -12.0,     Lon1 = 0.0,    Obl0 = 6.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = 11.0,      Lon1 = 0.0,    Obl0 = 0.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = -10.0,     Lon1 = 0.0,    Obl0 = 5.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = -8.0,      Lon1 = 0.0,    Obl0 = 3.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = 7.0,       Lon1 = 0.0,    Obl0 = -3.0,   Obl1 = 0.0},
            new NutationCoefficients {Lon0 = -7.0,      Lon1 = 0.0,    Obl0 = 0.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = -7.0,      Lon1 = 0.0,    Obl0 = 3.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = -7.0,      Lon1 = 0.0,    Obl0 = 3.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = 6.0,       Lon1 = 0.0,    Obl0 = 0.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = 6.0,       Lon1 = 0.0,    Obl0 = -3.0,   Obl1 = 0.0},
            new NutationCoefficients {Lon0 = 6.0,       Lon1 = 0.0,    Obl0 = -3.0,   Obl1 = 0.0},
            new NutationCoefficients {Lon0 = -6.0,      Lon1 = 0.0,    Obl0 = 3.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = -6.0,      Lon1 = 0.0,    Obl0 = 3.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = 5.0,       Lon1 = 0.0,    Obl0 = 0.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = -5.0,      Lon1 = 0.0,    Obl0 = 3.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = -5.0,      Lon1 = 0.0,    Obl0 = 3.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = -5.0,      Lon1 = 0.0,    Obl0 = 3.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = 4.0,       Lon1 = 0.0,    Obl0 = 0.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = 4.0,       Lon1 = 0.0,    Obl0 = 0.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = 4.0,       Lon1 = 0.0,    Obl0 = 0.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = -4.0,      Lon1 = 0.0,    Obl0 = 0.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = -4.0,      Lon1 = 0.0,    Obl0 = 0.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = -4.0,      Lon1 = 0.0,    Obl0 = 0.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = 3.0,       Lon1 = 0.0,    Obl0 = 0.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = -3.0,      Lon1 = 0.0,    Obl0 = 0.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = -3.0,      Lon1 = 0.0,    Obl0 = 0.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = -3.0,      Lon1 = 0.0,    Obl0 = 0.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = -3.0,      Lon1 = 0.0,    Obl0 = 0.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = -3.0,      Lon1 = 0.0,    Obl0 = 0.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = -3.0,      Lon1 = 0.0,    Obl0 = 0.0,    Obl1 = 0.0},
            new NutationCoefficients {Lon0 = -3.0,      Lon1 = 0.0,    Obl0 = 0.0,    Obl1 = 0.0}
        };



        // Get the nutation in Longitude (dpsi) and nutation in obliquity (despilon)
        // These values are returned in radians:
        // See page 132 for the details of the calculation

        public static void GetNutation(out double dpsi, out double depsilon, double jd)
        {
            double T = (jd - 2451545.0) / 36525;
            double T2 = T * T;
            double T3 = T2 * T;

            double D = 297.85036 + 445267.111480 * T - 0.0019142 * T2 + T3 / 189474.0;
            double M = 357.52772 + 35999.050340 * T - 0.0001603 * T2 - T3 / 300000.0;
            double MDASH = 134.96298 + 477198.867398 * T + 0.0086972 * T2 + T3 / 56250.0;
            double F = 93.2719100 + 483202.017538 * T - 0.0036825 * T2 + T3 / 327270.0;
            double OMEGA = 125.04452 - 1934.136261 * T + 0.0020708 * T2 + T3 / 450000.0;

            double argument, sincoeff, coscoeff;
            dpsi = depsilon = 0;

            for (int i = 0; i < nutationArgs.Length; i++)
            {
                argument = nutationArgs[i].D * D +
                           nutationArgs[i].M * M +
                           nutationArgs[i].MDash * MDASH +
                           nutationArgs[i].F * F +
                           nutationArgs[i].Omega * OMEGA;

                argument = argument * Math.PI / 180.0;
                sincoeff = nutationCoefficients[i].Lon0 + nutationCoefficients[i].Lon1 * T;
                coscoeff = nutationCoefficients[i].Obl0 + nutationCoefficients[i].Obl1 * T;

                dpsi += sincoeff * Sin(argument);
                depsilon += coscoeff * Cos(argument);
            }

            // convert dpsi and depsilon to radians:
            dpsi = dpsi * Math.PI / 36000000.0 / 180.0;
            depsilon = depsilon * Math.PI / 36000000.0 / 180.0;
        }
    }
}

