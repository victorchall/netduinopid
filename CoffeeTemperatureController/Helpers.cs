using NetduinoPIDController;
using NetduinoPIDController.Helpers;
using NetduinoPIDController.IOControls;
using Microsoft.SPOT.Hardware;

namespace CoffeeTemperatureController
{
    public class Helpers
    {
        public static PIDControllerParameters GetDefaultPIDParameters()
        {
            var pidParams = new PIDControllerParameters
            {
                FeedbackControls = ControlHelpers.CreatePID(5f, 0.05f, 0.05f),
                Frequency = 1,
                InputControl = new TemperatureInput(Cpu.AnalogChannel.ANALOG_0),
                OutputControl = new PWMOutputControl(Cpu.PWMChannel.PWM_5, 1)
            };

            return pidParams;
        }
    }
}
