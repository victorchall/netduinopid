using System;
using Microsoft.SPOT;
using NetduinoPIDController.Window;
using NUnit.Framework;

namespace NetduinoPIDController.Tests.Window
{
    [TestFixture]
    public class TableLookupLinearTransferFunctionTests
    {
        [TestCaseSource(typeof(TestHelpers), nameof(TestHelpers.TestValues))]
        public void TableLookupLinearTransferFunction_GetsFirstPoint(float firstErrorPoint)
        {
            var expectedValue = -1;
            var elements = new[] { new WindowElement(firstErrorPoint, expectedValue), new WindowElement(NextGreaterFloat(firstErrorPoint), expectedValue+1) };
            var propControl = new TableLookupLinearTransferFunction(elements);

            var result = propControl.GetValue(firstErrorPoint);

            Assert.That(result, Is.EqualTo(expectedValue).Within(TestHelpers.DesignPrecision));
        }

        [TestCaseSource(typeof(TestHelpers), nameof(TestHelpers.TestValues))]
        public void TableLookupLinearTransferFunction_GetsSecondPoint(float secondErrorPoint)
        {
            var expectedValue = 1;
            
            var elements = new[] { new WindowElement(NextLesserFloat(secondErrorPoint), expectedValue-1), new WindowElement(secondErrorPoint, expectedValue) };
            var propControl = new TableLookupLinearTransferFunction(elements);
            
            var result = propControl.GetValue(secondErrorPoint);

            Assert.That(result, Is.EqualTo(expectedValue).Within(TestHelpers.DesignPrecision));
        }

        [Test]
        public void TableLookupLinearTransferFunction_InterpolatesBetweenPoints()
        {
            var elements = new[] { new WindowElement(-1, -1), new WindowElement(1, 1)};
            var propControl = new TableLookupLinearTransferFunction(elements);

            var result = propControl.GetValue(0);

            Assert.That(result, Is.EqualTo(0).Within(TestHelpers.DesignPrecision));
        }

        private static float NextGreaterFloat(float secondErrorPoint)
        {
            return NextFloat(secondErrorPoint, false);
        }

        private static float NextLesserFloat(float secondErrorPoint)
        {
            return NextFloat(secondErrorPoint, true);
        }

        private static float NextFloat(float secondErrorPoint, bool lesser)
        {
            int tmp = BitConverter.ToInt32(BitConverter.GetBytes(secondErrorPoint), 0);

            if (tmp >= 0 ^ lesser) tmp++;
            else tmp--;

            var firstErrorValue = BitConverter.ToSingle(BitConverter.GetBytes(tmp), 0);
            return firstErrorValue;
        }
    }
}