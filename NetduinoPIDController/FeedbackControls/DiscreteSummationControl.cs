using System;
using Microsoft.SPOT;
using NetduinoPIDController.Window;

namespace NetduinoPIDController.FeedbackControls
{
    /// <summary>
    /// Accumulates drive values over iterations in discrete time
    /// </summary>
    public class DiscreteSummationControl : IFeedbackControl
    {
        private double _driveIntegral = 0;
        private readonly float _floor;
        private readonly float _ceiling;
        private readonly ILinearTransferFunction _errorToDriveLinearTransferFunction;

        public DiscreteSummationControl(float floor, float ceiling, ILinearTransferFunction errorToDriveLinearTransferFunction)
        {
            _errorToDriveLinearTransferFunction = errorToDriveLinearTransferFunction;
            _floor = floor;
            _ceiling = ceiling;
        }

        public DiscreteSummationControl(float floor, float ceiling, float gain)
        {
            _errorToDriveLinearTransferFunction = new GainLinearTransferFunction(gain);
            _floor = floor;
            _ceiling = ceiling;
        }
        
        public DiscreteSummationControl(float gain)
        {
            _errorToDriveLinearTransferFunction = new GainLinearTransferFunction(gain);
            _floor = float.MinValue;
            _ceiling = float.MaxValue;
        }

        public void Reset()
        {
            _driveIntegral = 0;
        }

        public float GetValue(float error)
        {
            _driveIntegral += _errorToDriveLinearTransferFunction.GetValue(error);

            _driveIntegral = System.Math.Max(_driveIntegral, _floor);
            _driveIntegral = System.Math.Min(_driveIntegral, _ceiling);

            return (float)_driveIntegral;
        }
    }
}
