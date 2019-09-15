﻿using System;
using static System.Math;

namespace Sunset
{
    static class Sun
    {
        /// <summary>
        /// Calculate sunrise and sunset given latitude, longitude and date
        /// </summary>
        /// <param name="rise">output - the calulcated sunrise (0.0-1.0) fraction of a day</param>
        /// <param name="set">output - the calulcated sunset (0.0-1.0) fraction of a day</param>
        /// <param name="lat">latitude in raidans</param>
        /// <param name="lon">longitude in raidans with negative being west from Greenwich</param>
        /// <param name="date">the date for which to calculate sunrise and sunset</param>
        static public void SunRise(out double rise, out double set, double lat, double lon, DateTime date)
        {
            double T;
            double jd = GetJulianDate(date);
            T = (jd - 2451545.0) / 36525.0;

            // sidereal time at greenwich page 83
            double theta0 = 100.46061837 + 36000.770053608 * T +
                            0.000387933 * T * T - (T * T * T) / 38710000.0;

            theta0 = theta0 % 360.0;
            if (theta0 < 0.0)
            {
                theta0 += 360.0;
            }
            theta0 *= Math.PI / 180.0;

            // sun's position on the day before, the day specified and the day after
            GetSunPos(out var alpha1, out var delta1, date - TimeSpan.FromDays(1));
            GetSunPos(out var alpha2, out var delta2, date);
            GetSunPos(out var alpha3, out var delta3, date + TimeSpan.FromDays(1));
            Console.WriteLine("------------------");
            //phms(alpha2);
            //pdms(delta2);
            Console.WriteLine("------------------");

            // for the sun only - see pages 97-98 - get h0 in radians:
            double h0 = -0.8333333333 * Math.PI / 180.0; // RADIANS

            // use correct symbols - see page 97 and covert to radians:
            double phi = lat * Math.PI / 180.0;
            double L = lon * Math.PI / 180.0;

            // equation 14.1 - see page 98
            double cosH0 = (Sin(h0) - Sin(phi) *
                            Sin(delta2)) /
                           (Cos(phi) * Cos(delta2));

            double cosHM1 = (Sin(h0) - Sin(phi) *
                            Sin(delta1)) /
                           (Cos(phi) * Cos(delta1));


            double cosHP1 = (Sin(h0) - Sin(phi) *
                            Sin(delta3)) /
                           (Cos(phi) * Cos(delta3));

            // find H0 itself from its cosine:
            if (cosH0 > 1.0000000000000)
            {
                cosH0 = cosH0 + 0.0;
            }
            double H0 = Acos(cosH0);

            // calculate approximate times in fractions of a day:
            // (equations 14.2 - see page 98)
            double m0 = (alpha2 + L - theta0) / 2.0 / PI;
            double m1 = m0 - (H0 / 2.0 / PI);
            double m2 = m0 + (H0 / 2.0 / PI);

            // make sure the above calculated fractions are between zero and one:
            Normalize(ref m0);
            Normalize(ref m1);
            Normalize(ref m2);

            // Interpolate and iterate using the method on page 99
            rise = FinalM(alpha1, alpha2, alpha3, delta1, delta2, delta3,
                phi, L, m1, theta0, h0);

            set = FinalM(alpha1, alpha2, alpha3, delta1, delta2, delta3,
                phi, L, m2, theta0, h0);
        }

        private static void Normalize(ref double m)
        {
            while (m < 0.0)
            {
                m += 1.0;
            }

            while (m > 1.0)
            {
                m -= 1.0;
            }
        }

        static double Interpolate(double x1, double x2, double x3, double n)
        {
            double a = x2 - x1;
            double b = x3 - x2;
            double c = b - a;
            return x2 + 0.5 * n * (a + b + n * c);
        }

        static double FinalM(double a1, double a2, double a3,
                      double d1, double d2, double d3,
                      double phi, double L, double m,
                      double theta0, double h0)
        {
            double n, alpha, delta, H, sinh, deltam, h;
            const double deltaT = 67.0;

            double theta;

            do
            {
                theta = theta0 + (360.985647 * PI / 180.0) * m; // page 99
                n = m + deltaT / 86400.0;        // page 99
                alpha = Interpolate(a1, a2, a3, n);   // interpolate - equation 3.3 page 25
                delta = Interpolate(d1, d2, d3, n);   // interpolate - equation 3.3 page 25

                H = theta - L - alpha;

                sinh = Sin(phi) * Sin(delta) +
                       Cos(phi) * Cos(delta) * Cos(H);

                h = Asin(sinh);

                deltam = (h - h0) /
                        (2 * PI * Cos(delta) * Cos(phi) * Sin(H));

                m = m + deltam;
            }
            while (Abs(deltam) > 0.00001);
            return m;
        }




        /// <summary>
        /// Get the sun's right ascension and declination IN RADIANS
        /// See the chapter on Solar Coordinates (Chapter 24)</summary>
        /// Page 151 onwards
        /// This function uses the more accurate method of page 154 onwards.
        /// <param name="alpha"></param>
        /// <param name="delta"></param>
        /// <param name="date">The date for which to get the sun's position</param>
        /// <param name="time">The time for which to get the sun's position (zero = midnight, 1.0 = midnight the next day)</param>
        private static void GetSunPos(out double alpha, out double delta, DateTime date, double time = 0)
        {
            double jde = GetJulianDate(date) + time;

            double tau = (jde - 2451545.0) / 365250;

            double l, b, r;

            // get heliocentric coordinates for earth from VSOP series:
            l = SumSeries(VSOPEarth.Along, tau); // radians
            b = SumSeries(VSOPEarth.Alat, tau);  // radians
            r = SumSeries(VSOPEarth.ARad, tau);  // astronomical units

            // adjust to get geocentric coordinates of the sun:
            double sun = l + PI;
            double beta = -b;

            // get in 0-360 degrees:
            sun = Fix2Pi(sun);


            // Do the FK5 corrections - page 154 equations 24.9
            double T = 10 * tau;
            double lamdadash = sun - (1.397 * T + 0.00031 * T * T) * PI / 180.0;
            double deltasun = -0.09033 / 3600.0 * PI / 180.0;
            double deltabeta = 0.03916 * (Cos(lamdadash) - Sin(lamdadash)) / 3600.0 * PI / 180.0;
            sun += deltasun;

            beta += deltabeta;

            // Get nutations in longitude and obliquity - these are returned
            // in RADIANS:
            double dpsi, depsilon;
            GetNutation(dpsi, depsilon, jde);

            sun += dpsi;

            // Caculate the correction in the sun's longitude due to abberation:
            // Equations 24.11 on page 155 and the long equation on page 156
            double deltalambda = GetDeltaLamda(jde); // in RADIANS
            double abberation = 0.005775518 * r * deltalambda;
            sun -= abberation;

            double epsilon0 = GetEpsilon0(jde);
            epsilon0 += depsilon;

            // get alpha and delta in RADIANS:
            alpha = Atan2((Cos(epsilon0) * Sin(sun)),
                                 Cos(sun));

            delta = Asin(Sin(epsilon0) * Sin(sun));
        }

        private static double GetEpsilon0(double jde)
        {
            throw new NotImplementedException();
        }

        private static double GetDeltaLamda(double jde)
        {
            throw new NotImplementedException();
        }

        private static void GetNutation(double dpsi, double depsilon, double jde)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sum a VSOP series for a given tau
        /// </summary>
        /// <param name="series"></param>
        /// <param name="tau"></param>
        /// <returns></returns>
        private static double SumSeries(double[][] series, double tau)
        {
            var sums = new double[series.Length];
            for (int i = 0; i < series.Length; i++)
            {
                double sum = 0;
                for (int j = 0; j < series[i].Length; j++)
                {
                    double a = series[i][j * 3];
                    double b = series[i][j * 3 + 1];
                    double c = series[i][j * 3 + 2];
                    double term = a * Cos(b + c * tau);
                    sum += term;
                }
                sums[i] = sum;
            }

            var i2 = 0;
            double finalSum = 0.0, tauPower = 1.0;
            for (; i2 < series.Length; i2++, tauPower *= tau)
            {
                finalSum += sums[i2] * tauPower;
            }

            return finalSum;
        }

        /// <summary>
        /// Given a DateTime, return a Julian Date. The time component of the input date is ignored.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private static double GetJulianDate(DateTime date)
        {
            var d = date.Day;
            var m = date.Month;
            var y = date.Year;
            var serial = (1461 * (y + 4800 + (m - 14) / 12)) / 4 +
                 (367 * (m - 2 - 12 * ((m - 14) / 12))) / 12 -
                 (3 * ((y + 4900 + (m - 14) / 12) / 100)) / 4 + d - 32075;
            return serial - 0.5;
        }
    }
}