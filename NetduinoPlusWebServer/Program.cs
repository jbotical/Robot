using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using System.IO;
using NetduinoPlusWebServer.WebServer;

namespace NetduinoPlusWebServer
{
    public class Program
    {
        const string WebFolder = "\\SD\\Web";

        public static void Main()
        {
            // start the webserver
            Listener webServer = new Listener(RequestReceived);

            OutputPort led = new OutputPort(Pins.ONBOARD_LED, false);
            while (true)
            {
                // Blink LED to show we're still responsive
                led.Write(!led.Read());
                Thread.Sleep(500);
            }

        }

        public static void RequestReceived(Request request)
        {
            
            ResponseController rc = new ResponseController();

            request.SendResponse(rc.BuildHtml(request));

        }

        
        /// <summary>
        /// Look for a file on the SD card and send it back if it exists
        /// </summary>
        /// <param name="request"></param>
        private static void TrySendFile(Request request)
        {
            // Replace / with \
            string filePath = WebFolder + request.URL.Replace('/', '\\');

            if (File.Exists(filePath))
                request.SendFile(filePath);
            else
                request.Send404();
        }

    }
}
