using System;
using Microsoft.SPOT;

namespace NetduinoPIDController.TimeService
{
    public class DiscreteTimeService : ITimeService
    {
        public float GetTimeDeltaSeconds()
        {
            return 1f;
        }
    }
}
