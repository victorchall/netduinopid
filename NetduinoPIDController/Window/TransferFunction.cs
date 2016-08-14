using System;
using Microsoft.SPOT;
using System.Collections;
using NetduinoPIDController;

namespace NetduinoPIDController.Window
{
    public class TransferFunction
    {
        private WindowElement[] _window;

        /// <summary>
        /// Defines error to drive amount.  Error value must increase in order of array!
        /// </summary>
        /// <param name="window">Error to drive value lookups.  Error values MUST increase!</param>
        public TransferFunction(WindowElement[] window)
        {
            float previous = float.MinValue;
            foreach(WindowElement current in window)
            {
                if (current.InValue < previous)
                {
                    throw new ArgumentException("Window has decreasing value at error" + current.InValue);
                }
                previous = current.InValue;
            }

            _window = window;
        }

        /// <summary>
        /// Performs a lookup on the window.
        /// </summary>
        /// <param name="inValue">Error value for lookup</param>
        /// <returns>Drive value</returns>
        public float GetValue(float inValue)
        {
            for (int i = 0; i < _window.Length; i++)
            {
                var currentWindowElement = _window[i];

                if (inValue <= currentWindowElement.InValue)
                {
                    // If error is outside on the window on the low side, just use lowest possible value
                    return currentWindowElement.OutValue;
                }

                if (i == _window.Length - 1)
                {
                    // If error outside the window on high side, just use highest possible value
                    return currentWindowElement.OutValue;
                }
                else if (inValue < _window[i + 1].InValue)
                {
                    return GetWeightedMeanOfWindowElements(inValue, _window[i], _window[i+1]);
                }
            }            

            throw new ApplicationException("Error in window lookup, likely improper window construction");
        }

        private float GetWeightedMeanOfWindowElements(float actualError, WindowElement lesserPoint, WindowElement greaterPoint)
        {
            var localWindowSize = greaterPoint.InValue - lesserPoint.InValue;

            var distanceToLesser = actualError - lesserPoint.InValue;
            var distanceToGreater = greaterPoint.InValue - actualError;
            
            var weightOfLesser = (distanceToLesser / localWindowSize);
            var weightOfGreater = (distanceToGreater / localWindowSize);

            var driveValue = lesserPoint.OutValue * weightOfLesser;
            driveValue += (greaterPoint.OutValue * weightOfGreater);

            return driveValue;
        }
    }
}
