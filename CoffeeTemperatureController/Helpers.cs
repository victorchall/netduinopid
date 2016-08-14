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
            PIDControllerParameters pidParams = new PIDControllerParameters();
            pidParams.FeedbackControls = ControlHelpers.CreatePID(1f, 0.05f, 0.05f);
            pidParams.Frequency = 1;
            pidParams.InputControl = new TemperatureInput(Cpu.AnalogChannel.ANALOG_0);
            pidParams.OutputControl = new PWMOutputControl(Cpu.PWMChannel.PWM_5, 1);

            return pidParams;
        }
    }
}
