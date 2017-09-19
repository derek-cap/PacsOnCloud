using Dicom.Log;
using Dicom.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel
{
    public class MyDicomServer
    {
        private IDicomServer _dicomServer;
        private Logger _logger;

        public MyDicomServer(int port, Logger logger)
        {
            _logger = logger;
            _dicomServer = DicomServer.Create<CStoreSCPProvider>(port);
        }

        public void Run()
        {
            _logger?.Info($"C-Store server is running.");
            _logger?.Info(_dicomServer.IsListening.ToString());
        }
    }
}
