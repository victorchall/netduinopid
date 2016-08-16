using NetduinoPIDController.FeedbackControls;
using NUnit.Framework;

namespace NetduinoPIDController.Tests.FeedbackControls
{
    [TestFixture]
    public class DifferenceControlTests
    {
        [Test, TestCaseSource(typeof(TestHelpers), nameof(TestHelpers.TestValues))]
        public void DifferenceControl_IncrementScalesWithGain(float testGain)
        {
            var diffControl = new DifferenceControl(testGain);

            diffControl.GetValue(0);
            var result = diffControl.GetValue(1);

            Assert.That(result, Is.EqualTo(testGain).Within(TestHelpers.DesignPrecision));
        }
        
    }
}