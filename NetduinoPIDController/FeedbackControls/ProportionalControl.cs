using System;
using Microsoft.SPOT;
using NetduinoPIDController.Window;

namespace NetduinoPIDController.FeedbackControls
{
    public class ProportionalControl : IFeedbackControl
    {
        private TransferFunction _window;

        public ProportionalControl(float gain)
        {
            _window = WindowHelpers.GainWindow(gain);
        }

        public ProportionalControl(TransferFunction window)
        {
            _window = window;
        }

        public float GetValue(float error)
        {
            return _window.GetValue(error);
        }

        public void Reset()
        {
            // No state to reset on proportional controller
        }

    }
}
