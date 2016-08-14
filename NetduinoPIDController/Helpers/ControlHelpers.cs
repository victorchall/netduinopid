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
            IFeedbackControl[] controls = new IFeedbackControl[3];

            controls[0] = new ProportionalControl(kp);
            controls[0] = new DiscreteSummationControl(ki);
            controls[0] = new DifferenceControl(kd);

            return controls;
        }
    }
}
