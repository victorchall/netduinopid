namespace NetduinoPIDController.FeedbackControls
{
    public interface IFeedbackControl
    {
        /// <summary>
        /// Get feedback value, iterating if applicable
        /// </summary>
        /// <param name="error">Target minus actual</param>
        /// <returns>Amount to add to feedback control</returns>
        float GetValue(float error);

        /// <summary>
        /// Reset any stored history in controller.
        /// </summary>
        void Reset();
    }
}
