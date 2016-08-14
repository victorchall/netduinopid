using System;
using Microsoft.SPOT;

namespace NetduinoPIDController.Helpers
{
    public static class GeneralHelpers
    {
        /// <summary>
        /// Converts Timespan to seconds
        /// </summary>
        /// <param name="interval">span to covert</param>
        /// <returns>interval as seconds</returns>
        public static float TotalSeconds(this TimeSpan interval)
        {
            return interval.Days * 24 * 60 * 60 +
                    interval.Hours * 60 * 60 +
                    interval.Minutes * 60 +
                    interval.Seconds +
                    interval.Milliseconds / 1000;
        }

        public static float Min(float a, float b)
        {
            if (a < b)
                return a;

            return b;
        }

        public static float Max(float a, float b)
        {
            if (a > b)
                return a;

            return b;
        }

        public static float Clamp(double x)
        {
            if (x > float.MaxValue)
                return float.MaxValue;
            if (x < float.MinValue)
                return float.MinValue;

            return (float)x;
        }
    }
}
