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
    public class DiscreteTimeBoxedIntegralControl : IFeedbackControl
    {
        private float _driveIntegral;
        private readonly float[] _errorHistory;
        private int _errorHistoryPreviousIndex;
        private readonly ITransferFunction _errorToDriveTransferFunction;
        
        private DiscreteTimeBoxedIntegralControl(int historyLength = 10)
        {
            _errorHistory = new float[historyLength];
        }

        /// <summary>
        /// IntegralControl with a min/max wind up and a given window
        /// </summary>
        /// <param name="windowElements">window used for each iteration</param>
        /// <param name="historyLength">length of integral in discrete time</param>
        public DiscreteTimeBoxedIntegralControl(WindowElement[] windowElements, int historyLength = 10) : this(historyLength)
        {
            _errorToDriveTransferFunction = new TableLookupTransferFunction(windowElements);
        }

        /// <summary>
        /// IntegralControl with limits on wind up and a gain value.
        /// </summary>
        /// <param name="gain">slope of drive value over error</param>
        /// <param name="historyLength">length of integral in discrete time</param>
        public DiscreteTimeBoxedIntegralControl(float gain, int historyLength) : this(historyLength)
        {
            _errorToDriveTransferFunction = new GainTransferFunction(gain);
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
            _driveIntegral += _errorToDriveTransferFunction.GetValue(error);

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
