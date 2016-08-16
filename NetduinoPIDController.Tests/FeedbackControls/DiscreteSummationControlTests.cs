using NetduinoPIDController.FeedbackControls;
using NUnit.Framework;

namespace NetduinoPIDController.Tests.FeedbackControls
{
    [TestFixture]
    public class DiscreteSummationControlTests
    {
        [Test, TestCaseSource(typeof(TestHelpers), nameof(TestHelpers.TestValues))]
        public void DiscreteSummationControl_IncrementScalesWithGain(float testGain)
        {
            var sumControl = new DiscreteSummationControl(testGain);

            var result = sumControl.GetValue(1);
            
            Assert.That(result, Is.EqualTo(testGain).Within(TestHelpers.DesignPrecision));

            var result2 = sumControl.GetValue(1);

            Assert.That(result2, Is.EqualTo(testGain*2).Within(TestHelpers.DesignPrecision));
        }
    }
}