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
        private readonly float[] _errorHistory;
        private int _errorHistoryPreviousIndex;
        private readonly ILinearTransferFunction _errorToDriveMapping;
        
        private DiscreteTimeBoxedIntegralControl(int historyLength = 10)
        {
            _errorHistory = new float[historyLength];
        }
        
        public DiscreteTimeBoxedIntegralControl(float gain, int historyLength) : this(historyLength)
        {
            _errorToDriveMapping = new GainLinearTransferFunction(gain);
        }

        public void Reset()
        {
            for (var i = 0; i < _errorHistory.Length; i++)
            {
                _errorHistory[i] = 0f;
            }
        }

        public float GetValue(float error)
        {
            var current = _errorToDriveMapping.GetValue(error);

            StoreDriveIntegral(current);

            float result = 0;

            foreach (var f in _errorHistory)
            {
                result += f;
            }

            return MathHelpers.Clamp(result);
        }

        private void StoreDriveIntegral(float currentVal)
        {
            if (_errorHistoryPreviousIndex == _errorHistory.Length-1)
            {
                _errorHistoryPreviousIndex = 0;
            }
            else
            {
                _errorHistoryPreviousIndex++;
            }

            _errorHistory[_errorHistoryPreviousIndex] = currentVal;
        }

    }
}
