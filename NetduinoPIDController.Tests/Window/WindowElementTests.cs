using System;
using NetduinoPIDController.Window;
using NUnit.Framework;

namespace NetduinoPIDController.Tests.Window
{
    [TestFixture]
    public class WindowElementTests
    {
        [TestCase(-1f, -1f)]
        [TestCase(float.Epsilon, 0f)]
        [TestCase(-0f, 0f)]
        public void WindowElement_Constructor(float inValue, float outValue)
        {
            var element = new WindowElement(inValue, outValue);

            Assert.AreEqual(inValue, element.InValue);
            Assert.AreEqual(outValue, element.OutValue);
        }
    }
}