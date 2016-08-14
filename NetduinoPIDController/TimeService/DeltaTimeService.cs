using System;
using NetduinoPIDController.Helpers;

namespace NetduinoPIDController.TimeService
{
    public class DeltaTimeService : ITimeService
    {
        private DateTime _lastTime = DateTime.MinValue;

        public DeltaTimeService()
        {
            _lastTime = DateTime.Now;
        }

        public float GetTimeDeltaSeconds()
        {
            var span = DateTime.Now - _lastTime;

            return span.TotalSeconds();
        }
    }
}
