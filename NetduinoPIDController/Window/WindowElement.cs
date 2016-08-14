using System;
using Microsoft.SPOT;

namespace NetduinoPIDController.Window
{
    public class WindowElement
    {
        public float InValue { get; }
        public float OutValue { get; }

        /// <summary>
        /// Correlates an error amount to corresponding drive value to use. 
        /// </summary>
        /// <param name="error"></param>
        /// <param name="driveValue"></param>
        public WindowElement (float error, float driveValue)
        {
            InValue = error;
            OutValue = driveValue;
        }
    }
}
