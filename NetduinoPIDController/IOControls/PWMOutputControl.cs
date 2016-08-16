using Microsoft.SPOT.Hardware;
using NetduinoPIDController.Helpers;

namespace NetduinoPIDController.IOControls
{
    public class PWMOutputControl : IOutputControl
    {
        private readonly PWM _pwm;

        public PWMOutputControl(Cpu.PWMChannel pwmChannel, double frequency)
        {
            _pwm = new PWM(pwmChannel, frequency, 0, false);
        }

        /// <summary>
        /// Interprets input value of 0 to 1 as 0% to 100% duty cycle
        /// </summary>
        /// <param name="driveValue">0 to 100</param>
        public void DriveOutput(float driveValue)
        {
            driveValue = MathHelpers.Min(driveValue, 100f);
            driveValue = MathHelpers.Max(driveValue, 0f);

            _pwm.DutyCycle = driveValue;
        }
    }
}
