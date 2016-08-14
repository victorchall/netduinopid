using System;
using Microsoft.SPOT;
using NetduinoPIDController.TimeService;
using NetduinoPIDController.Helpers;
using NetduinoPIDController.Window;

namespace NetduinoPIDController.FeedbackControls
{
    /// <summary>
    /// Integrates a history of error values 
    /// </summary>
    public class TimeBoxedIntegralControl : IFeedbackControl
    {
        private float _driveIntegral;
        private float[] _errorHistory;
        private int _errorHistoryPreviousIndex;
        private TransferFunction _window;
        private ITimeService _timeService;
        
        private TimeBoxedIntegralControl(int historyLength = 10)
        {
            _timeService = new DiscreteTimeProvider();
        }

        private TimeBoxedIntegralControl(ITimeService timeService, int historyLength = 10)
        {
            _timeService = timeService;
            _errorHistory = new float[historyLength];
        }

        /// <summary>
        /// IntegralControl with a min/max wind up and a given window
        /// </summary>
        /// <param name="floor">minimum accumualated drive value</param>
        /// <param name="ceiling">maximum accumualated drive value</param>
        /// <param name="windowElements">window used for each iteration</param>
        public TimeBoxedIntegralControl(WindowElement[] windowElements, int historyLength = 10) : this(historyLength)
        {
            _window = new TransferFunction(windowElements);
        }

        /// <summary>
        /// IntegralControl with limits on wind up and a gain value.
        /// </summary>
        /// <param name="floor">Integral wind up min value, min returned by GetValue</param>
        /// <param name="ceiling">Integral wind up max value, max returned by GetValue</param>
        /// <param name="gain">slope of drive value over error</param>
        public TimeBoxedIntegralControl(float gain, int history) : this(10)
        {
            _window = WindowHelpers.GainWindow(gain, int.MaxValue);
        }

        /// <summary>
        /// IntegralControl with unbounded accumulation and gain
        /// </summary>
        /// <param name="floor"></param>
        /// <param name="ceiling"></param>
        /// <param name="gain"></param>
        public TimeBoxedIntegralControl(float gain) : this(10)
        {
            _window = WindowHelpers.GainWindow(gain, int.MaxValue);

        }

        public void Reset()
        {
            _driveIntegral = 0;
        }

        /// <summary>
        /// Return the accumulated drive value, adding new error drive before return
        /// </summary>
        /// <param name="error">Error for window lookup</param>
        /// <returns>Accumulated error drive value</returns>
        public float GetValue(float error)
        {
            _driveIntegral += _window.GetValue(error);

            StoreDriveIntegral();

            return _driveIntegral;
        }

        private void StoreDriveIntegral()
        {
            if (_errorHistoryPreviousIndex == _errorHistory.Length)
            {
                _errorHistoryPreviousIndex = 0;
            }
            else
            {
                _errorHistoryPreviousIndex++;
            }

            _errorHistory[_errorHistoryPreviousIndex] = _driveIntegral;
        }

    }
}
