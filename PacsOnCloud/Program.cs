using DataModel;
using Dicom.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacsOnCloud
{
    class Program
    {
        static void Main(string[] args)
        {
            MyDicomServer server;

            try
            {
                int port = 12345;

                server = new MyDicomServer(port, new ConsoleLogger());
                server.Run();

                var client = new DicomClient();
                client.AddRequest(new DicomCStoreRequest(@"D:\test.dcm"));
                Task.Run(() =>
                {
                    try
                    {
                        client.SendAsync("127.0.0.1", port, false, "SCU", "STORESCP");             // Alt 1
                        Console.WriteLine("Send complete");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                });
            //    await client.SendAsync("127.0.0.1", 12345, false, "SCU", "ANY-SCP");  // Alt 2
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }

    class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
