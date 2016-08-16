namespace NetduinoPIDController.FeedbackControls
{
    public interface IFeedbackControl
    {
        /// <summary>
        /// Get feedback value, iterating if applicable
        /// </summary>
        /// <param name="error">Target minus actual</param>
        /// <returns>Value to add to total feedback control</returns>
        float GetValue(float error);

        /// <summary>
        /// Reset any stored history or state in controller.
        /// </summary>
        void Reset();
    }
}
