namespace NetduinoPIDController.Window
{
    public class GainLinearTransferFunction : ILinearTransferFunction
    {
        private readonly float _gain;

        public GainLinearTransferFunction(float gain)
        {
            _gain = gain;
        }

        public float GetValue(float error)
        {
            return error * _gain;
        }       
    }
}