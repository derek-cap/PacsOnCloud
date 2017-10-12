using DataModel;
using Dicom.Log;
using Dicom.Network;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PacsOnCloud
{
    class Program
    {      
        static void Main(string[] args)
        {
            MyDicomServer server;
            Console.WriteLine("This is server running...");
            try
            {
                int port = 12345;
                LogManager.SetImplementation(new MinLogManager("127.0.0.1"));
                Logger logger = LogManager.GetLogger("a");

                server = new MyDicomServer(port, logger);
                server.Run();

                string hostname = Dns.GetHostName();
                logger.Debug("Hostname: {hostname}", hostname);

                IPHostEntry localhost = Dns.GetHostEntry(hostname);
                IPAddress[] address = localhost.AddressList;
                IPAddress theOne = address.Where(t => t.AddressFamily == AddressFamily.InterNetwork).First();
                logger.Debug(theOne.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
