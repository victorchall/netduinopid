using System;
using System.Threading;
using Microsoft.SPOT.Hardware;
using NetduinoPIDController.FeedbackControls;
using NetduinoPIDController.IOControls;
using NetduinoPIDController.Window;

namespace NetduinoPIDController
{
    public class PIDController
    {
        private readonly PIDControllerParameters _pidParameters;
        private Thread _pidRun;
        private float _setPoint;

        private const int PIDFrequency = 1;

        /// <summary>
        /// Controller for PID temperature control system
        /// </summary>
        public PIDController(PIDControllerParameters pidParameters)
        {
            _pidParameters = pidParameters;
            _setPoint = pidParameters.SetPoint;
        }

        /// <summary>
        /// Set point for PID controller. 
        /// </summary>
        public float FahrenheitSetPoint
        {
            get
            {
                return _setPoint;
            }
            set
            {
                // When set point is reset, must restart feedback to avoid integral/derivative spike/bump issues. 
                if (_pidRun.ThreadState == ThreadState.Running)
                {
                    Disable();
                    _setPoint = value;
                    Enable();
                }
                else
                {
                    _setPoint = value;
                }

            }
        }
        
        public void Enable()
        {
            foreach (var control in _pidParameters.FeedbackControls)
            {
                control.Reset();
            }

            _pidRun = new Thread(Run);
        }

        public void Disable()
        {
            _pidRun.Abort();

            foreach (var control in _pidParameters.FeedbackControls)
            {
                control.Reset();
            }
        }


        private void Run()
        {
            var period = (1000 / PIDFrequency);
            while (true)
            {
                try
                {                    
                    Thread.Sleep(period);
                    var error = GetError();
                    var duty = GetControlsSummation(error);
                    _pidParameters.OutputControl.DriveOutput(duty);
                }
                catch
                { }
            }
        }

        private float GetError()
        {
            return FahrenheitSetPoint - _pidParameters.InputControl.ReadValue();
        }

        private float GetControlsSummation(float error)
        {
            float dc = 0;

            foreach(var control in _pidParameters.FeedbackControls)
            {
                dc += control.GetValue(error);
            }

            return dc;
        }
    }
}
