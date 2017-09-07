using Dicom.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel
{
    public class MyDicomServer
    {
        private IDicomServer _dicomServer;
        private ILogger _logger;

        public MyDicomServer(int port, ILogger logger)
        {
            _logger = logger;
            _dicomServer = DicomServer.Create<CStoreSCPProvider>(port);
        }

        public void Run()
        {
            _logger?.Log($"C-Store server is running.");
            _logger.Log(_dicomServer.IsListening.ToString());
        }
    }
}
