using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Telerik.JustMock;
using NetduinoPIDController;
using NetduinoPIDController.IOControls;
using NetduinoPIDController.Window;
using NetduinoPIDController.FeedbackControls;
using Telerik.JustMock.Helpers;
using System.Threading;

namespace NetduinoPIDController.Tests
{
    [TestFixture]
    public class NetduinoPIDControllerControllerTests
    {
        NetduinoPIDController.PIDController _sut;
        List<float> outputLog;

        [SetUp]
        public void Setup()
        {
            outputLog = new List<float>();
        }

        [Test]
        public void Test1()
        {
            var mockInputControl = Mock.Create<IInputControl>();
            mockInputControl.Arrange(x => x.ReadValue()).Returns(1);

            var mockOutputControl = Mock.Create<IOutputControl>();
            mockOutputControl.Arrange(x => x.DriveOutput(Arg.AnyFloat)).DoInstead((float x) => outputLog.Add(x));

            PIDControllerParameters pidParams = new PIDControllerParameters();
            pidParams.InputControl = mockInputControl;
            pidParams.OutputControl = mockOutputControl;
            pidParams.SetPoint = 1;
            pidParams.Frequency = 1;
            pidParams.FeedbackControls = Helpers.ControlHelpers.CreatePID(1, 0, 0);

            _sut = new PIDController(pidParams);

            _sut.Enable();
            Thread.Sleep(1001);
            _sut.Disable();

            Assert.AreEqual(outputLog[0], 0);
        }
    }
}
