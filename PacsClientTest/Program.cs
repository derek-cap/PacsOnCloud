using DataModel;
using Dicom;
using Dicom.Imaging;
using Dicom.Log;
using Dicom.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PacsClientTest
{
    class Program
    {
        static int index = 0;

        static void TestCStoreService()
        {
            string ipAddress = "127.0.0.1";
            int port = 801;
            string calledAE = "MinfoundSCP";

            var client = new DicomClient();

            string path = @"C:\DATAPART2\FMIDICMFiles";
            DirectoryInfo info = new DirectoryInfo(path);
            Action<FileInfo> action = f =>
            {
                client.AddRequest(new DicomCStoreRequest(f.FullName));
                index++;
                if (index % 100 == 0)
                {
                    client.Send(ipAddress, port, false, "SCU", calledAE);
                }
            };
            AlldDo(info, action);

            Console.WriteLine(index);
        }

        static void AlldDo(DirectoryInfo info, Action<FileInfo> action)
        {
            if (index > 1000)
                return;
            foreach (var f in info.GetFiles())
            {
                action(f);
            }
            foreach (var item in info.GetDirectories())
            {
                AlldDo(item, action);
            }
        }

        //static void PrintTest()
        //{
        //    var stopwatch = new System.Diagnostics.Stopwatch();
        //    stopwatch.Start();

        //    var printJob = new PrintJob("DICOM PRINT JOB")
        //    {
        //        RemoteAddress = "localhost",
        //        RemotePort = 8000,
        //        CallingAE = "PRINTSCU",
        //        CalledAE = "PRINTSCP"
        //    };

        //    //greyscale
        //    var greyscaleImg = new DicomImage(@"Data\1.3.51.5155.1353.20020423.1100947.1.0.0.dcm");
        //    using (var bitmap = greyscaleImg.RenderImage().As<Bitmap>())
        //    {
        //        printJob.StartFilmBox("STANDARD\\1,1", "PORTRAIT", "A4");
        //        printJob.FilmSession.IsColor = false; //set to true to print in color
        //        printJob.AddImage(bitmap, 0);
        //        printJob.EndFilmBox();
        //    }

        //    //color
        //    var colorImg = new DicomImage(@"Data\US-RGB-8-epicard.dcm");
        //    using (var bitmap = colorImg.RenderImage().As<Bitmap>())
        //    {
        //        printJob.StartFilmBox("STANDARD\\1,1", "PORTRAIT", "A4");
        //        printJob.FilmSession.IsColor = true; //set to true to print in color
        //        printJob.AddImage(bitmap, 0);
        //        printJob.EndFilmBox();
        //    }

        //    printJob.Print();

        //    stopwatch.Stop();
        //    Console.WriteLine();
        //    Console.WriteLine(stopwatch.Elapsed);
        //}



        static void Main(string[] args)
        {
            //LogManager.SetImplementation(ConsoleLogManager.Instance);
            //Logger logger = LogManager.GetLogger("");


            Console.WriteLine("This is client running...");
            try
            {

                //string ipAddress = "10.10.21.98";
                string ipAddress = "10.10.22.119";
                int port = 801;
                //     string calledAE = "DS5302";
                string calledAE = "MinfoundSCP";

                //     MyDicomServer server = new MyDicomServer(port, logger);

                var client = new DicomClient();
                client.NegotiateAsyncOps();
                var quest = new DicomCEchoRequest();
                quest.OnResponseReceived += (rq, rsp) =>
                {
                    Console.WriteLine("----------------------------" + rsp.Status);
                };
                client.Send(ipAddress, port, false, "ABD", calledAE);

                //var cfind = DicomCFindRequest.CreateWorklistQuery();
                //cfind.OnResponseReceived += (rq, rsp) =>
                //{
                //    Console.WriteLine("PatientAge:{0} PatientName:{1}", rsp.Dataset.Get<string>(DicomTag.PatientAge), rsp.Dataset.Get<string>(DicomTag.PatientName));
                //};
                //var client = new DicomClient();
                //client.AddRequest(cfind);
                //client.Send(ipAddress, port, false, "CC", "FindSCP");

           //     TestCStoreService();
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
