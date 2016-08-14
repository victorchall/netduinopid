using System;
using Microsoft.SPOT;
using NetduinoPIDController.Window;

namespace NetduinoPIDController.FeedbackControls
{
    public class ProportionalControl : IFeedbackControl
    {
        private readonly ITransferFunction _transferFunction;

        public ProportionalControl(float gain)
        {
            _transferFunction = new GainTransferFunction(gain);
        }

        public ProportionalControl(TableLookupTransferFunction transferFunction)
        {
            if (transferFunction == null) throw new ArgumentNullException(nameof(transferFunction));
            _transferFunction = transferFunction;
        }

        public float GetValue(float error)
        {
            return _transferFunction.GetValue(error);
        }

        public void Reset()
        {
            // No state to reset on proportional controller
        }

    }
}
