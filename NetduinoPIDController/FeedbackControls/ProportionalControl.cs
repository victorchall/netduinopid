using System;
using Microsoft.SPOT;
using NetduinoPIDController.Window;

namespace NetduinoPIDController.FeedbackControls
{
    public class ProportionalControl : IFeedbackControl
    {
        private readonly ILinearTransferFunction _linearTransferFunction;

        public ProportionalControl(float gain)
        {
            _linearTransferFunction = new GainLinearTransferFunction(gain);
        }

        public ProportionalControl(TableLookupLinearTransferFunction linearTransferFunction)
        {
            if (linearTransferFunction == null) throw new ArgumentNullException(nameof(linearTransferFunction));
            _linearTransferFunction = linearTransferFunction;
        }

        public float GetValue(float error)
        {
            return _linearTransferFunction.GetValue(error);
        }

        public void Reset()
        {
            // No state to reset on proportional controller
        }

    }
}
