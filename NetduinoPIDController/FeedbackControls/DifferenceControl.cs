using NetduinoPIDController.TimeService;
using NetduinoPIDController.Window;

namespace NetduinoPIDController.FeedbackControls
{
    public class DifferenceControl : IFeedbackControl
    {
        private readonly ILinearTransferFunction _linearTransferFunction;
        private float _previousError;

        public DifferenceControl(float gain)
        {
            _linearTransferFunction = new GainLinearTransferFunction(gain);
        }

        public void Reset()
        {
            _previousError = 0;
        }
        
        public float GetValue(float error)
        {
            var deltaError = error - _previousError;

            _previousError = error;

            return _linearTransferFunction.GetValue(deltaError);
        }
    }
}
