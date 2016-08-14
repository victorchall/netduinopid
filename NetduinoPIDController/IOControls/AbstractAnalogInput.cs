using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace NetduinoPIDController.IOControls
{
    public abstract class AbstractAnalogInput : IInputControl
    {
        protected AnalogInput _inputChannel;

        public AbstractAnalogInput(Cpu.AnalogChannel inputChannel)
        {
            _inputChannel = new AnalogInput(inputChannel);
        }

        public virtual float ReadValue()
        {
            double rawInput = _inputChannel.Read();
            return Helpers.GeneralHelpers.Clamp(rawInput);
        }        
    }
}
