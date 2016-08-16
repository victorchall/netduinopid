using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetduinoPIDController;
using NetduinoPIDController.IOControls;

namespace NetduinoPIDController.Tests
{
    public static class TestHelpers
    {
        public const float DesignPrecision = 0.0000001f;

        public static float[] TestValues =
            {
                -99999999999999f,
                -1f,
                -float.Epsilon,
                -0f,
                0f,
                float.Epsilon,
                1f,
                99999999999999f
            };
            
        
    }
}
