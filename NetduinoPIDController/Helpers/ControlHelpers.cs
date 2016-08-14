using System;
using Microsoft.SPOT;
using NetduinoPIDController.FeedbackControls;
using NetduinoPIDController.Window;

namespace NetduinoPIDController.Helpers
{
    public class ControlHelpers
    {
        public static IFeedbackControl[] CreatePID(float kp, float ki, float kd)
        {
            var controls = new IFeedbackControl[3];

            controls[0] = new ProportionalControl(kp);
            controls[1] = new DiscreteSummationControl(ki);
            controls[2] = new DifferenceControl(kd);

            return controls;
        }
    }
}
