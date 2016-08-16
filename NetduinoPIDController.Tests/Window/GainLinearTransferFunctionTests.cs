using NetduinoPIDController.Window;
using NUnit.Framework;

namespace NetduinoPIDController.Tests.Window
{
    [TestFixture]
    public class GainLinearTransferFunctionTests
    {
        [Test, TestCaseSource(typeof(TestHelpers), nameof(TestHelpers.TestValues))]
        public void GainLinearTransferFunction_ScalesWithGain(float testGain)
        {
            var propControl = new GainLinearTransferFunction(testGain);

            var result = propControl.GetValue(1);

            Assert.That(result, Is.EqualTo(testGain).Within(TestHelpers.DesignPrecision));
        }

        [Test, TestCaseSource(typeof(TestHelpers), nameof(TestHelpers.TestValues))]
        public void GainLinearTransferFunction_ScalesWithError(float error)
        {
            var propControl = new GainLinearTransferFunction(1);

            var result = propControl.GetValue(error);

            Assert.That(result, Is.EqualTo(error).Within(TestHelpers.DesignPrecision));
        }
    }
}