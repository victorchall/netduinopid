using System;
using Microsoft.SPOT;

namespace NetduinoPIDController.Window
{
    public class WindowHelpers
    {
        public static TransferFunction GainWindow(float gain)
        {
            return GainWindow(gain, 10000);
        }

        /// <summary>
        /// Creates transfer function with error width of -size to size, values set based on gain (slope). 
        /// </summary>
        /// <param name="gain">Slope of output over input</param>
        /// <param name="width">input min/max value</param>
        /// <returns></returns>
        public static TransferFunction GainWindow(float gain, int size)
        {
            var elements = new []
            {
                new WindowElement(-size, -size * gain),
                new WindowElement(size, size * gain)
            };

            return new TransferFunction(elements);
        }

        /// <summary>
        /// Creates a simple window with a gain of 1
        /// </summary>
        /// <param name="size">Distance from zero in each direction for window to extend</param>
        /// <returns></returns>
        public static TransferFunction SquareWindow(int size)
        {
            return GainWindow(1, size);
        }
    }
}
