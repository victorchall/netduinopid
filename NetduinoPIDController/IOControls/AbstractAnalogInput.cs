using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace NetduinoPIDController.IOControls
{
    public abstract class AbstractAnalogInput : IInputControl
    {
        protected AnalogInput InputChannel;

        protected AbstractAnalogInput(Cpu.AnalogChannel inputChannel)
        {
            InputChannel = new AnalogInput(inputChannel);
        }

        public virtual float ReadValue()
        {
            double rawInput = InputChannel.Read();
            return Helpers.GeneralHelpers.Clamp(rawInput);
        }        
    }
}
