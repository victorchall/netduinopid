namespace NetduinoPIDController.Window
{
    public class GainTransferFunction : ITransferFunction
    {
        private readonly float _gain;

        public GainTransferFunction(float gain)
        {
            _gain = gain;
        }

        public float GetValue(float error)
        {
            return error * _gain;
        }       
    }
}