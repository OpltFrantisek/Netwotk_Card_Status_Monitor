using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Timers;

namespace Monitor
{
    class Program
    {

        static bool first = false;
        static void Main(string[] args)
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 10000;
           

            Console.WriteLine("Zmackni \'q\' pro ukonceni.");
           
            bool networkUp = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            NetworkInterface[] networkCards = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();

            if (!File.Exists("log.txt"))
            {
                
                first = true;
            }           
            aTimer.Enabled = true;
            while (Console.Read() != 'q') ;
        }
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            using (StreamWriter log = File.AppendText("log.txt"))
            {  
                NetworkInterface[] networkCards = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
                var sb = new StringBuilder();
                if (first)
                {
                        sb.Append("Cas;");
                        foreach (var network in networkCards)
                        {
                            sb.Append(network.Name + ";");
                        }

                        log.WriteLine(sb.ToString());
                    first = false;
                }
              sb = new StringBuilder();
               
                sb.Append(DateTime.Now+";");
                foreach (var network in networkCards)
                {
                    sb.Append(network.OperationalStatus + ";");
                }
                log.WriteLine(sb.ToString());

            }
              
        }
    }
}
