using System;
using Microsoft.SPOT;
using System.Threading;
using SecretLabs.NETMF.Hardware.Netduino;

namespace NetduinoPlusWebServer
{
    class ServoController
    {
        public static bool _run;

        public static Servo _leftServo;// = new Servo(Pins.GPIO_PIN_D9);
        
        public static Servo _centerServo;
        public static Servo _rightServo;//Servo _rightServo = new Servo(Pins.GPIO_PIN_D6);

        int _leftMin = 0;
        int _leftMax = 160;
        int _leftDefault = 10;
        int _centerMin = 10;
        int _centerMax = 160;
        int _centerDefault = 100;
        int _rightMin = 10;
        int _rightMax = 160;
        int _rightDefault = 40;
        
        int _leftIncSmall = 5;
        int _leftIncLarge = 15;
        int _centerIncSmall = 5;
        int _centerIncLarge = 15;
        int _rightIncSmall = 5;
        int _rightIncLarge = 15;

        int _leftLoc;
        int _centerLoc;
        int _rightLoc;

        public ServoController()
        {
            if (_leftServo == null)
            {
                _leftServo = new Servo(Pins.GPIO_PIN_D9);
                _leftLoc = _leftDefault;

            }

            if (_centerServo == null)
            {
                _centerServo = new Servo(Pins.GPIO_PIN_D5);
                _centerLoc = _centerDefault;
            }

            if (_rightServo == null)
            {
                _rightServo = new Servo(Pins.GPIO_PIN_D6);
                _rightLoc = _rightDefault;
            }

           
            _run = true;
        }

        public int GetLeftLoc()
        {
            return _leftLoc;
        }

        public int GetCenterLoc()
        {
            return _centerLoc;
        }

        public int GetRightLoc()
        {
            return _rightLoc;
        }


        public void StopServos()
        {
            _run = false;
        }

        public void LeftPlus()
        {
            var newLoc = _leftLoc + _leftIncSmall;
            _leftLoc = newLoc;

            if (newLoc > _leftMax)
            {
                newLoc = _leftMax;
            }
            else
            {
                newLoc += _leftIncSmall;
            }

            //_leftServo.DegreeLeft = newLoc;
            _leftServo.Degree = newLoc;
        }

        public void LeftMinus()
        {
            var newLoc = _leftLoc - _leftIncSmall;
            _leftLoc = newLoc;

            if (newLoc < _leftMin)
            {
                newLoc = _leftMin;
            }
            else
            {
                newLoc -= _leftIncSmall;
            }

            //_leftServo.DegreeLeft = newLoc;
            _leftServo.Degree = newLoc;

        }

        /// /////////////////////////////////////////////// CENTER //////////////////

        public void CenterPlus()
        {
            var newLoc = _centerLoc + _centerIncSmall;
            _centerLoc = newLoc;

            if (newLoc > _centerMax)
            {
                newLoc = _centerMax;
            }
            else
            {
                newLoc += _centerIncSmall;
            }

            _centerServo.Degree = newLoc;
        }

        public void CenterMinus()
        {
            var newLoc = _centerLoc - _centerIncSmall;
            _centerLoc = newLoc;

            if (newLoc < _centerMin)
            {
                newLoc = _centerMin;
            }
            else
            {
                newLoc -= _centerIncSmall;
            }

            _centerServo.Degree = newLoc;
        }

        public void RightPlus()
        {
            var newLoc = _rightLoc + _rightIncSmall;
            _rightLoc = newLoc;

            if (newLoc > _rightMax)
            {
                newLoc = _rightMax;
            }
            else
            {
                newLoc += _rightIncSmall;
            }

            _rightServo.Degree = newLoc;
        }

        public void RightMinus()
        {
            var newLoc = _rightLoc - _rightIncSmall;
            _rightLoc = newLoc;

            if (newLoc < _rightMin)
            {
                newLoc = _rightMin;
            }
            else
            {
                newLoc -= _rightIncSmall;
            }

            _rightServo.Degree = newLoc;
        }

        public void StartServos()
        {
            int minRotation = 20;
            int maxRotation = 120;

            int maxLeft = 80;
            int leftLocation = 0;

            //Servo leftServo = new Servo(Pins.GPIO_PIN_D9);
            //Servo centerServo = new Servo(Pins.GPIO_PIN_D5);
            //Servo rightServo = new Servo(Pins.GPIO_PIN_D6);

            while (_run)
            {
                for (int i = minRotation; i <= maxRotation; i++)
                {
                    if (!_run)
                    {
                        break;
                    }

                    if (i > maxLeft)
                    {
                        leftLocation = maxLeft;
                    }
                    else
                    {
                        leftLocation = i;
                    }


                    _leftServo.Degree = leftLocation;
                    _centerServo.Degree = i;
                    _rightServo.Degree = maxRotation - i;
                    Thread.Sleep(2);
                }

                for (int i = maxRotation; i >= minRotation; i--)
                {
                    if (!_run)
                    {
                        break;
                    }

                    if (i > maxLeft)
                    {
                        leftLocation = maxLeft;
                    }
                    else
                    {
                        leftLocation = i;
                    }

                    _leftServo.Degree = leftLocation;
                    _centerServo.Degree = i;
                    _rightServo.Degree = maxRotation - i;
                    Thread.Sleep(2);
                }
            }
        }


    }

       


}
