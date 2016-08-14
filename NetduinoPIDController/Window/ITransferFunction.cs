namespace NetduinoPIDController.Window
{
    public interface ITransferFunction
    {
        float GetValue(float error);
    }
}