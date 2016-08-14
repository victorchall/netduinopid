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
        private Thread _pidRun;
        private float _setPoint;
        private IInputControl _input;
        private IOutputControl _output;
        private IFeedbackControl[] _controls;

        private int _pidFrequency = 1;
        
        /// <summary>
        /// Controller for PID temperature control system
        /// </summary>
        /// <param name="temperatureInput">Channel where temperature sensor is connected</param>
        public PIDController(PIDControllerParameters pidParameters)
        {
            _input = pidParameters.InputControl;
            _output = pidParameters.OutputControl;
            _setPoint = pidParameters.SetPoint;
            _controls = pidParameters.FeedbackControls;
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
            foreach (var control in _controls)
            {
                control.Reset();
            }

            _pidRun = new Thread(new ThreadStart(Run));
        }

        public void Disable()
        {
            _pidRun.Abort();

            foreach (var control in _controls)
            {
                control.Reset();
            }
        }


        private void Run()
        {
            var period = (1000 / _pidFrequency);
            while (true)
            {
                try
                {                    
                    Thread.Sleep(period);
                    var error = GetError();
                    var duty = GetControlsSummation(error);
                    _output.DriveOutput(duty);
                }
                catch
                { }
            }
        }

        private float GetError()
        {
            return FahrenheitSetPoint - _input.ReadValue();
        }

        private float GetControlsSummation(float error)
        {
            float dc = 0;

            foreach(var control in _controls)
            {
                dc += control.GetValue(error);
            }

            return dc;
        }
    }
}
