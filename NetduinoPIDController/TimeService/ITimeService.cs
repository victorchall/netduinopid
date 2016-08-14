using System;
using Microsoft.SPOT;

namespace NetduinoPIDController.TimeService
{
    public interface ITimeService
    {
        float GetTimeDeltaSeconds();
    }
}
