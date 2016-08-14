using Microsoft.SPOT.Hardware;
using NetduinoPIDController.FeedbackControls;
using NetduinoPIDController.IOControls;

namespace NetduinoPIDController
{
    public struct PIDControllerParameters
    {
        public IInputControl InputControl;
        public IOutputControl OutputControl;
        public IFeedbackControl[] FeedbackControls;
        public float SetPoint;
        public float Frequency;
    }
}
