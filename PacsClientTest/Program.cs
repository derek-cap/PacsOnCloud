using Dicom.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PacsClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is client running...");
            try
            {
                int port = 12345;
                var client = new DicomClient();
                for (int i = 0; i < 10; i++)
                {
                    client.AddRequest(new DicomCStoreRequest(@"D:\test.dcm"));
                }
                Task.Run(() =>
                {
                    try
                    {
                        client.SendAsync("127.0.0.1", port, false, "SCU", "STORESCP");             // Alt 1
                        Console.WriteLine("Send complete");
                        client.Release();
                        Thread.Sleep(5000);
                        client.AddRequest(new DicomCStoreRequest(@"D:\test.dcm"));
                        client.SendAsync("127.0.0.1", port, false, "SCU", "STORESCP");             // Alt 1
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                });
               
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
