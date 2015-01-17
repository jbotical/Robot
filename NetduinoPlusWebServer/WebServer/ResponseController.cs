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

            var requestMethod = request.URL.Substring(1, request.URL.Length - 1);
            //ServoController servos = new ServoController();


            

            switch (requestMethod)
            {
                case "StartMovement":

                    new Thread(StartServo).Start();
                    _servos.StartServos();
                    break;

                case "StopAll":
                    _servos.StopServos();
                    break;

                case "LeftPlus":
                    new Thread(LeftPlus).Start();
                    _servos.LeftPlus();
                    break;

                case "LeftMinus":
                    new Thread(LeftMinus).Start();
                    _servos.LeftMinus();
                    break;

                case "CenterPlus":
                    new Thread(CenterPlus).Start();
                    _servos.CenterPlus();
                    break;

                case "CenterMinus":
                    new Thread(CenterMinus).Start();
                    _servos.CenterMinus();
                    break;

                case "RightPlus":
                    new Thread(RightPlus).Start();
                    _servos.RightPlus();
                    break;

                case "RightMinus":
                    new Thread(RightMinus).Start();
                    _servos.RightMinus();
                    break;
            }

            // TODO: class for a single activity request

            // TODO: class for HtmlElement to hold the parts of the Html stored in XML file

            //string id;

            //var filePath = "Homepage.xml";
            //Stream fileStream;

            //FileStream fileToStream = null;

            //fileToStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);


            //Stream fileStream = File.OpenRead("\\Views\\Homepage.xml");

            //if (fileStream != null)
            //{
            //    XmlTextReader reader = new XmlTextReader(fileStream);

            //    reader.Read();

            //    while (reader.Read())
            //    {
            //        if (reader.Name == "id")
            //        {
            //            reader.Read();
            //            var readerNodeType = reader.NodeType.ToString();

            //            if (readerNodeType == "String")
            //            {
            //                id = reader.Value;
            //                reader.Read();
            //            }
            //        }

            //    }

            //}




            //XmlDocument doc = new XmlDocument();
            //doc.Load("c:\\temp.xml");


            //foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            //{
            //    string text = node.InnerText; //or loop through its children as well
            //}

            
            // Use this for a really basic check that it's working

            // build the Html page contents
            // <-- HEADER start --> //
            var style = "<style>.btn {background: #3498db; background-image: -webkit-linear-gradient(top, #3498db, #2980b9);  background-image: -moz-linear-gradient(top, #3498db, #2980b9);  background-image: -ms-linear-gradient(top, #3498db, #2980b9);  background-image: -o-linear-gradient(top, #3498db, #2980b9);  background-image: linear-gradient(to bottom, #3498db, #2980b9);  -webkit-border-radius: 28;  -moz-border-radius: 28;  border-radius: 28px;  font-family: Arial;  color: #ffffff;  font-size: 20px;  padding: 10px 20px 10px 20px;  text-decoration: none;} .btn:hover {  background: #3cb0fd;  background-image: -webkit-linear-gradient(top, #3cb0fd, #3498db);  background-image: -moz-linear-gradient(top, #3cb0fd, #3498db);  background-image: -ms-linear-gradient(top, #3cb0fd, #3498db);  background-image: -o-linear-gradient(top, #3cb0fd, #3498db);  background-image: linear-gradient(to bottom, #3cb0fd, #3498db);  text-decoration: none;}</style>";

            responseHtml += String.Concat("<html>",
                "<head>",
                style,
                "</head>");


            // <-- HEADER end --> //

            // <-- BODY start --> //
            var startMovementButton = "<a href=\"http://165.168.1.101/StartMovement\"><button class=\"btn\">Start Movement</button></a>";
            var stopMovementButton = "<a href=\"http://165.168.1.101/StopAll\"><button class=\"btn\">Stop All Movement</button></a>";
            var resetSystemButton = "<a href=\"http://165.168.1.101/\"><button class=\"btn\">Reset</button></a>";
            var leftPlusButton = "<a href=\"http://165.168.1.101/LeftPlus\"><button class=\"btn\">Left +</button></a>";
            var leftMinusButton = "<a href=\"http://165.168.1.101/LeftMinus\"><button class=\"btn\">Left -</button></a>";
            var centerPlusButton = "<a href=\"http://165.168.1.101/CenterPlus\"><button class=\"btn\">Center +</button></a>";
            var centerMinusButton = "<a href=\"http://165.168.1.101/CenterMinus\"><button class=\"btn\">Center -</button></a>";
            var rightPlusButton = "<a href=\"http://165.168.1.101/RightPlus\"><button class=\"btn\">Right +</button></a>";
            var rightMinusButton = "<a href=\"http://165.168.1.101/RightMinus\"><button class=\"btn\">Right -</button></a>";

            var buildDate = "150117";
            //var leftLocationTextBox = "<form name=\"myForm\"><input type=\"text\" name=\"leftDegree\">";

            var requestClientIp = request.Client.ToString();
            var now = DateTime.Now.ToString();

            responseHtml += String.Concat(responseHtml, "<body>",
                "<br />",
                "<h1>Bot Control - Home</h1>",
                "<br/><br/>",
                startMovementButton,
                stopMovementButton,
                resetSystemButton,
                "<br/><br/>Left ",
                leftPlusButton,
                leftMinusButton,
                "<br/>Center ",
                centerPlusButton,
                centerMinusButton,
                "<br/>Right ",
                rightPlusButton,
                rightMinusButton,
                //leftLocationTextBox,
                "<br/><br/>",
                "<p>zzRequest from " + requestClientIp + " received at " + now,
                "</p><p>Method: " + requestMethod + "<br />URL: " + request.URL,
                "<br/><br/>",
                buildDate,
                "</p></body></html>");

         
            return responseHtml;
            // Send a file
            //TrySendFile(request);

        }

        

       
    }
}
