using System;
using Microsoft.SPOT;

namespace NetduinoPIDController.IOControls
{
    public interface IInputControl
    {
        float ReadValue();
    }
}
