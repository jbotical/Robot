using System;
using SecretLabs.NETMF.Hardware;
using Microsoft.SPOT.Hardware;

namespace NetduinoPlusWebServer
{
    public class ServoOrg : IDisposable
    {
        /// <summary>
        /// PWM handle
        /// </summary>
        private static PWM _servoLeft;
        private static PWM _servoCenter;
        private static PWM _servoRight;

        /// <summary>
        /// Timings range
        /// </summary>
        private int[] range = new int[2];

        /// <summary>
        /// Set servo inversion
        /// </summary>
        public bool inverted = false;


        public Servo(Cpu.Pin pin)
        {
            if (pin == Cpu.Pin.GPIO_Pin9)
                ServoLeft(pin);

            if (pin == Cpu.Pin.GPIO_Pin5)
                ServoCenter(pin);

            if (pin == Cpu.Pin.GPIO_Pin6)
                ServoRight(pin);
            
        }

        public void Dispose()
        {
            disengageLeft();
            _servoLeft.Dispose();
            disengageCenter();
            _servoCenter.Dispose();
            disengageRight();
            _servoRight.Dispose();
        }
        
        /// <summary>
        /// Create the PWM pin, set it low and configure timings
        /// </summary>
        /// <param name="pin"></param>
        public void ServoLeft(Cpu.Pin pin)
        {

            if (_servoLeft == null)
            {
                // Init the PWM pin
                _servoLeft = new PWM((Cpu.Pin)pin);
            }

            _servoLeft.SetDutyCycle(0);

            // Typical settings
            range[0] = 1000;
            range[1] = 2000;

        }

        public void DisposeLeft()
        {
            disengageLeft();
            _servoLeft.Dispose();
        }

        /// <summary>
        /// Allow the user to set cutom timings
        /// </summary>
        /// <param name="fullLeft"></param>
        /// <param name="fullRight"></param>
        public void setRangeLeft(int fullLeft, int fullRight)
        {
            range[1] = fullLeft;
            range[0] = fullRight;
        }

        /// <summary>
        /// Disengage the servo. 
        /// The servo motor will stop trying to maintain an angle
        /// </summary>
        public void disengageLeft()
        {
            // See what the Netduino team say about this... 
            _servoLeft.SetDutyCycle(0);
        }

        /// <summary>
        /// Set the servo degree
        /// </summary>
        public double DegreeLeft
        {
            set
            {
                /// Range checks
                if (value > 180)
                    value = 180;

                if (value < 0)
                    value = 0;

                // Are we inverted?
                if (inverted)
                    value = 180 - value;

                // Set the pulse
                _servoLeft.SetPulse(20000, (uint)map((long)value, 0, 180, range[0], range[1]));
            }
        }

        ///////////////////////////////////////////////////////////////////


        /// <summary>
        /// Create the PWM pin, set it low and configure timings
        /// </summary>
        /// <param name="pin"></param>
        public void ServoCenter(Cpu.Pin pin)
        {

            if (_servoCenter == null)
            {
                // Init the PWM pin
                _servoCenter = new PWM((Cpu.Pin)pin);
            }

            _servoCenter.SetDutyCycle(0);

            // Typical settings
            range[0] = 1000;
            range[1] = 2000;
        }

        public void DisposeCenter()
        {
            disengageCenter();
            _servoCenter.Dispose();
        }

        /// <summary>
        /// Allow the user to set cutom timings
        /// </summary>
        /// <param name="fullLeft"></param>
        /// <param name="fullRight"></param>
        public void setRangeCenter(int fullLeft, int fullRight)
        {
            range[1] = fullLeft;
            range[0] = fullRight;
        }

        /// <summary>
        /// Disengage the servo. 
        /// The servo motor will stop trying to maintain an angle
        /// </summary>
        public void disengageCenter()
        {
            // See what the Netduino team say about this... 
            _servoCenter.SetDutyCycle(0);
        }

        /// <summary>
        /// Set the servo degree
        /// </summary>
        public double DegreeCenter
        {
            set
            {
                /// Range checks
                if (value > 180)
                    value = 180;

                if (value < 0)
                    value = 0;

                // Are we inverted?
                if (inverted)
                    value = 180 - value;

                // Set the pulse
                _servoCenter.SetPulse(20000, (uint)map((long)value, 0, 180, range[0], range[1]));
            }
        }

        ////////////////////////////////////////////////////////////////


        /// <summary>
        /// Create the PWM pin, set it low and configure timings
        /// </summary>
        /// <param name="pin"></param>
        public void ServoRight(Cpu.Pin pin)
        {

            if (_servoRight == null)
            {
                // Init the PWM pin
                _servoRight = new PWM((Cpu.Pin)pin);
            }

            _servoRight.SetDutyCycle(0);

            // Typical settings
            range[0] = 1000;
            range[1] = 2000;
        }

        public void DisposeRight()
        {
            disengageRight();
            _servoRight.Dispose();
        }

        /// <summary>
        /// Allow the user to set cutom timings
        /// </summary>
        /// <param name="fullLeft"></param>
        /// <param name="fullRight"></param>
        public void setRangeRight(int fullLeft, int fullRight)
        {
            range[1] = fullLeft;
            range[0] = fullRight;
        }

        /// <summary>
        /// Disengage the servo. 
        /// The servo motor will stop trying to maintain an angle
        /// </summary>
        public void disengageRight()
        {
            // See what the Netduino team say about this... 
            _servoRight.SetDutyCycle(0);
        }

        /// <summary>
        /// Set the servo degree
        /// </summary>
        public double DegreeRight
        {
            set
            {
                /// Range checks
                if (value > 180)
                    value = 180;

                if (value < 0)
                    value = 0;

                // Are we inverted?
                if (inverted)
                    value = 180 - value;

                // Set the pulse
                _servoRight.SetPulse(20000, (uint)map((long)value, 0, 180, range[0], range[1]));
            }
        }

        ///////////////////////////

        /// <summary>
        /// Used internally to map a value of one scale to another
        /// </summary>
        /// <param name="x"></param>
        /// <param name="in_min"></param>
        /// <param name="in_max"></param>
        /// <param name="out_min"></param>
        /// <param name="out_max"></param>
        /// <returns></returns>
        private long map(long x, long in_min, long in_max, long out_min, long out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }
    }
}
