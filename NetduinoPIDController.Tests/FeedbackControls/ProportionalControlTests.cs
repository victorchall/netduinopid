using NetduinoPIDController.FeedbackControls;
using NUnit.Framework;

namespace NetduinoPIDController.Tests.FeedbackControls
{
    [TestFixture]
    public class ProportionalControlTests
    {
        [Test, TestCaseSource(typeof(TestHelpers), nameof(TestHelpers.TestValues))]
        public void ProportionalControlByGain_ScalesWithGain(float testGain)
        {
            var propControl = new ProportionalControl(testGain);

            var result = propControl.GetValue(1);

            Assert.That(result, Is.EqualTo(testGain).Within(TestHelpers.DesignPrecision));
        }

        [Test, TestCaseSource(typeof(TestHelpers), nameof(TestHelpers.TestValues))]
        public void ProportionalControlByGain_ScalesWithError(float testError)
        {
            var propControl = new ProportionalControl(1);

            var result = propControl.GetValue(testError);

            Assert.That(result, Is.EqualTo(testError).Within(TestHelpers.DesignPrecision));
        }
    }
}