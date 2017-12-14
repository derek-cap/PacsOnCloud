using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PacsOnCloud
{
    public class ImageBuffer
    {
        // Recon series UID
        public string ReconUID { get; set; }

        // Image index in current recon 
        public int Index { get; set; }

        // Image data
        public Int16[] Data { get; set; }

        // Is last one in current recon?
        public bool IsLastOne { get; set; }

        // Is this buffer has no error?
        public bool IsValid { get; set; }

        // Error message, only when (IsValid == false)
        public string ErrorMessage { get; set; }
    }



    class DBService : IProgress<ImageBuffer>
    {
        public DBService(string name)
        {
            Console.WriteLine($"Create DBService with name: {name}");
        }

        public void Report(ImageBuffer value)
        {
            Thread.Sleep(2000);
            Console.WriteLine("Got image buffer");
        }
    }
}
