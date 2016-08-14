using System;
using Microsoft.SPOT;

namespace NetduinoPIDController.IOControls
{
    public interface IOutputControl
    {
        void DriveOutput(float driveValue);
    }
}
