namespace NetduinoPIDController.Window
{
    public interface ILinearTransferFunction
    {
        float GetValue(float error);
    }
}