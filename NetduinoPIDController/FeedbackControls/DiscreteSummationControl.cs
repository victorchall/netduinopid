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
        private float _floor;
        private float _ceiling;
        private TransferFunction _window;

        /// <summary>
        /// IntegralControl with a min/max wind up and a given window
        /// </summary>
        /// <param name="floor">minimum accumualated drive value</param>
        /// <param name="ceiling">maximum accumualated drive value</param>
        /// <param name="windowElements">window used for each iteration</param>
        public DiscreteSummationControl(float floor, float ceiling, TransferFunction window)
        {
            _window = window;
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
            _window = WindowHelpers.GainWindow(gain, int.MaxValue);
            _floor = floor;
            _ceiling = ceiling;
        }

        /// <summary>
        /// IntegralControl with unbounded accumulation and gain
        /// </summary>
        /// <param name="floor"></param>
        /// <param name="ceiling"></param>
        /// <param name="gain"></param>
        public DiscreteSummationControl(float gain)
        {
            _window = WindowHelpers.GainWindow(gain, int.MaxValue);
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
        /// <param name="error">Error for window lookup</param>
        /// <returns>Accumulated error drive value</returns>
        public float GetValue(float error)
        {
            _driveIntegral += _window.GetValue(error);

            _driveIntegral = System.Math.Max(_driveIntegral, _floor);
            _driveIntegral = System.Math.Min(_driveIntegral, _ceiling);

            return (float)_driveIntegral;
        }
    }
}
