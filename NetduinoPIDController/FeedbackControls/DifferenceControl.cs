using System;
using Microsoft.SPOT;
using NetduinoPIDController.TimeService;
using NetduinoPIDController.Window;

namespace NetduinoPIDController.FeedbackControls
{
    /// <summary>
    /// Provides
    /// </summary>
    public class DifferenceControl : IFeedbackControl
    {
        private TransferFunction _window;
        private ITimeService _timeProvider;
        private float _previousError;
        
        private DifferenceControl()
        {
            _timeProvider = new DiscreteTimeProvider();
        }

        /// <summary>
        /// DerivativeControl with unbounded drive
        /// </summary>
        /// <param name="gain">slope of drive over 1st derivative of error</param>
        public DifferenceControl(float gain) : this()
        {
            _window = WindowHelpers.GainWindow(gain, int.MaxValue);            
        }

        public void Reset()
        {
            _previousError = 0;
        }

        /// <summary>
        /// Return the accumulated drive value, adding new error drive before return
        /// </summary>
        /// <param name="error">Error for window lookup</param>
        /// <returns>Accumulated error drive value</returns>
        public float GetValue(float error)
        {
            float derivative = error / _previousError;

            _previousError = error;

            return _window.GetValue(derivative);
        }
    }
}
