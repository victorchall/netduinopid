using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using NetduinoPIDController;
using NetduinoPIDController.Helpers;
using NetduinoPIDController.IOControls;

namespace CoffeeTemperatureController
{
    public class Program
    {
        public static void Main()
        {
            var pidParams = Helpers.GetDefaultPIDParameters();

            PIDController pid = new PIDController(pidParams);

            // TODO: Read on on/off button, read a pot or stepper for temp setting, output to display, etc. 
            //pid.FahrenheitSetPoint = 180f;
            //pid.Enable();
            //Thread.Sleep(60 * 1000);
            //pid.Disable();
        }

    }
}
