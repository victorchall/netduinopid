using NetduinoPIDController.TimeService;
using NetduinoPIDController.Window;

namespace NetduinoPIDController.FeedbackControls
{
    /// <summary>
    /// Provides
    /// </summary>
    public class DifferenceControl : IFeedbackControl
    {
        private readonly ITransferFunction _transferFunction;
        private float _previousError;

        /// <summary>
        /// DerivativeControl with unbounded drive using discrete time
        /// </summary>
        /// <param name="gain">slope of drive over 1st derivative of error</param>
        public DifferenceControl(float gain)
        {
            _transferFunction = new GainTransferFunction(gain);
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
            var deltaError = error - _previousError;

            _previousError = error;

            return _transferFunction.GetValue(deltaError);
        }
    }
}
