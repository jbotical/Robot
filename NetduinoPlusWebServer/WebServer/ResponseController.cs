using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using System.IO;




namespace NetduinoPlusWebServer.WebServer
{
    class ResponseController
    {
        static private ServoController _servos;

        public ResponseController()
        {

            if (_servos == null)
            {
                _servos = new ServoController();

            }

        }

        public static void StartServo()
        {
            //ServoController servos = new ServoController();

            _servos.StartServos();

        }


        public static void LeftPlus()
        {
            _servos.LeftPlus();
        }

        public static void LeftMinus()
        {
            _servos.LeftMinus();
        }

        public static void CenterPlus()
        {
            _servos.CenterPlus();
        }

        public static void CenterMinus()
        {
            _servos.CenterMinus();
        }

        public static void RightPlus()
        {
            _servos.RightPlus();
        }

        public static void RightMinus()
        {
            _servos.RightMinus();
        }



        public string BuildHtml(Request request)
        {
            var responseHtml = "";

            var fullResponse = request.URL;
            var requestMethod = fullResponse.Substring(1, request.URL.Length - 1);

            var questionLoc = fullResponse.IndexOf("?");

            string[] urlData = fullResponse.Split('=');
            var parm = "";

            //if (urlData.Length > 1)
            //{
            //    parm = urlData[1];
            //}


            //if (questionLoc != -1)
            //{
            //    requestMethod = fullResponse.Substring(questionLoc, fullResponse.Length);

            //}
            //else
            //{
            //    requestMethod = fullResponse.Substring(1, questionLoc);
            //}

            var degreeInput = "";
            //if (questionLoc != -1)
            //    degreeInput = fullResponse.Substring(questionLoc, fullResponse.Length - questionLoc);

            //ServoController servos = new ServoController();

            const string startMovement = "StartMovement";
            const string stopMovement = "StopAll";
            const string resetSystem = "ResetSystem";
            const string leftPlus = "LeftPlus";
            const string leftMinus = "LeftMinus";
            const string centerPlus = "CenterPlus";
            const string centerMinus = "CenterMinus";
            const string rightPlus = "RightPlus";
            const string rightMinus = "RightMinus";


            switch (requestMethod)
            {
                case startMovement:

                    new Thread(StartServo).Start();
                    _servos.StartServos();
                    break;

                case stopMovement:
                    _servos.StopServos();
                    break;

                case resetSystem:
                    PowerState.RebootDevice(false);
                    break;

                case leftPlus:
                    new Thread(LeftPlus).Start();
                    _servos.LeftPlus();
                    break;

                case leftMinus:
                    new Thread(LeftMinus).Start();
                    _servos.LeftMinus();
                    break;

                case centerPlus:
                    new Thread(CenterPlus).Start();
                    _servos.CenterPlus();
                    break;

                case centerMinus:
                    new Thread(CenterMinus).Start();
                    _servos.CenterMinus();
                    break;

                case rightPlus:
                    new Thread(RightPlus).Start();
                    _servos.RightPlus();
                    break;

                case rightMinus:
                    new Thread(RightMinus).Start();
                    _servos.RightMinus();
                    break;
            }


            // build the Html page contents
            // <-- HEADER start --> //
            var style = "<style>.btn {background: #3498db; background-image: -webkit-linear-gradient(top, #3498db, #2980b9);  background-image: -moz-linear-gradient(top, #3498db, #2980b9);  background-image: -ms-linear-gradient(top, #3498db, #2980b9);  background-image: -o-linear-gradient(top, #3498db, #2980b9);  background-image: linear-gradient(to bottom, #3498db, #2980b9);  -webkit-border-radius: 28;  -moz-border-radius: 28;  border-radius: 28px;  font-family: Arial;  color: #ffffff;  font-size: 20px;  padding: 10px 20px 10px 20px;  text-decoration: none;} .btn:hover {  background: #3cb0fd;  background-image: -webkit-linear-gradient(top, #3cb0fd, #3498db);  background-image: -moz-linear-gradient(top, #3cb0fd, #3498db);  background-image: -ms-linear-gradient(top, #3cb0fd, #3498db);  background-image: -o-linear-gradient(top, #3cb0fd, #3498db);  background-image: linear-gradient(to bottom, #3cb0fd, #3498db);  text-decoration: none;}</style>";

            responseHtml += String.Concat("<html>",
                "<head>",
                style,
                "</head>");


            // <-- HEADER end --> //

            // <-- BODY start --> //
            var startMovementButton = BuildButton(startMovement, "Start Movement");
            var stopMovementButton = BuildButton(stopMovement, "Stop and Reset");
            var resetSystemButton = BuildButton(resetSystem, "Reset System");
            var leftPlusButton = BuildButton(leftPlus, "Left +");
            var leftMinusButton = BuildButton(leftMinus, "Left -");
            var centerPlusButton = BuildButton(centerPlus, "Center +");
            var centerMinusButton = BuildButton(centerMinus, "Center -");
            var rightPlusButton = BuildButton(rightPlus, "Right +");
            var rightMinusButton = BuildButton(rightMinus, "Right -");

            var buildDate = "150117";
            //var leftLocationTextBox = "<form name=\"myForm\"><input type=\"text\" name=\"leftDegree\">";
            var textBox = "<textarea></textarea>";

            var requestClientIp = request.Client.ToString();
            var now = DateTime.Now.ToString();

            responseHtml += String.Concat(responseHtml, "<body>",
                "<br />",
                "<h1>Bot Control - Home</h1>",
                "<br/><br/>",
                @"<table style=""width:100%>""",
                    "<tr>",
                        "<td>", startMovementButton, "</td>",
                        "<td>", stopMovementButton, "</td>",
                        "<td>", resetSystemButton, "</td>",
                    "</tr>",
                    "<tr>",
                        "<td>", leftPlusButton, "</td>",
                        "<td>", leftMinusButton, "</td>",
                        "<td>", textBox, "</td>",
                    "<tr>",
                        "<td>", centerPlusButton, "</td>",
                        "<td>", centerMinusButton, "</td>",
                    "</tr>",
                    "<tr>",
                        "<td>", rightPlusButton, "</td>",
                        "<td>", rightMinusButton, "</td>",
                //leftLocationTextBox,
                    "</tr>",
                "</table>",
                "<br/><br/>",
                "<p>zzRequest from " + requestClientIp + " received at " + now,
                "</p><p>Method: " + requestMethod + "<br />URL: " + request.URL,
                "<br/><br/>",
                buildDate,
                "<br/><br/>",
                "input: ", degreeInput,
                "<br/>",
                "url parm: ", parm,
                "<br/>",
                "left location: ", _servos.GetLeftLoc(),
                "<br/>",
                "center location: ", _servos.GetCenterLoc(),
                "<br/>",
                "right location: ", _servos.GetRightLoc(),
                "</p></body></html>");


            return responseHtml;
            // Send a file
            //TrySendFile(request);

        }

        private string BuildButton(string command, string buttonText)
        {
            return String.Concat("<a href=\"http://165.168.1.101/", command, "\"><button class=\"btn\">", buttonText, "</button></a>");
        }

    }
}
