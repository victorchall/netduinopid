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
        private readonly ITransferFunction _errorToDriveTransferFunction;

        /// <summary>
        /// IntegralControl with a min/max wind up and a given errorToDriveTransferFunction
        /// </summary>
        /// <param name="floor">minimum accumualated drive value</param>
        /// <param name="ceiling">maximum accumualated drive value</param>
        /// <param name="errorToDriveTransferFunction">defines how error relates to immediate drive value internal to controller</param>
        public DiscreteSummationControl(float floor, float ceiling, ITransferFunction errorToDriveTransferFunction)
        {
            _errorToDriveTransferFunction = errorToDriveTransferFunction;
            _floor = floor;
            _ceiling = ceiling;
        }

        /// <summary>
        /// IntegralControl with limits on wind up and a gain value.
        /// </summary>
        /// <param name="floor">Integral wind up min value, min returned by GetValue</param>
        /// <param name="ceiling">Integral wind up max value, max returned by GetValue</param>
        /// <param name="gain">slope of drive value over error</param>
        public DiscreteSummationControl(float floor, float ceiling, float gain)
        {
            _errorToDriveTransferFunction = new GainTransferFunction(gain);
            _floor = floor;
            _ceiling = ceiling;
        }

        /// <summary>
        /// IntegralControl with unbounded accumulation and gain
        /// </summary>
        /// <param name="gain"></param>
        public DiscreteSummationControl(float gain)
        {
            _errorToDriveTransferFunction = new GainTransferFunction(gain);
            _floor = float.MinValue;
            _ceiling = float.MaxValue;
        }

        public void Reset()
        {
            _driveIntegral = 0;
        }

        /// <summary>
        /// Return the accumulated drive value, adding new error drive before return
        /// </summary>
        /// <param name="error">Error for errorToDriveTransferFunction lookup</param>
        /// <returns>Accumulated error drive value</returns>
        public float GetValue(float error)
        {
            _driveIntegral += _errorToDriveTransferFunction.GetValue(error);

            _driveIntegral = System.Math.Max(_driveIntegral, _floor);
            _driveIntegral = System.Math.Min(_driveIntegral, _ceiling);

            return (float)_driveIntegral;
        }
    }
}
