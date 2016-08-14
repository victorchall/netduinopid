using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using NetduinoPIDController.IOControls;

namespace CoffeeTemperatureController
{
    internal class TemperatureInput : AbstractAnalogInput
    {
        public TemperatureInput(Cpu.AnalogChannel inputChannel) : base(inputChannel)
        {
        }
    }
}
