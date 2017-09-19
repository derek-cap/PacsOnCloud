using DataModel;
using Dicom.Log;
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
            Console.WriteLine("This is server running...");
            try
            {
                int port = 12345;
                LogManager.SetImplementation(ConsoleLogManager.Instance);

                server = new MyDicomServer(port, LogManager.GetLogger("a"));
                server.Run();
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
