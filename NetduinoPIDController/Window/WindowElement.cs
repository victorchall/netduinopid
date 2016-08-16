using System;
using Microsoft.SPOT;

namespace NetduinoPIDController.Window
{
    public class WindowElement
    {
        public float InValue { get; }
        public float OutValue { get; }

        /// <summary>
        /// Correlates a key and value
        /// </summary>
        /// <param name="inValue">key for lookup</param>
        /// <param name="outValue">related value</param>
        public WindowElement (float inValue, float outValue)
        {
            InValue = inValue;
            OutValue = outValue;
        }
    }
}
