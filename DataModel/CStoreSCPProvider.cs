using Dicom.Network;
using System;
using System.Collections.Generic;
using System.Text;
using Dicom.Log;
using Dicom;
using System.IO;
using System.Threading;

namespace DataModel
{
    public class CStoreSCPProvider : DicomService, IDicomServiceProvider, IDicomCStoreProvider
    {
        private static string _storagePath = @"D:\DICOM";
        private static int _index = 0;
        private static readonly object _mutex = new object();

        private static DicomTransferSyntax[] AcceptedTransferSyntaxes = new DicomTransferSyntax[]
                                                                                {
                                                                                    DicomTransferSyntax.ExplicitVRLittleEndian,
                                                                                    DicomTransferSyntax.ExplicitVRBigEndian,
                                                                                    DicomTransferSyntax.ImplicitVRLittleEndian
                                                                                };

        private static DicomTransferSyntax[] AcceptedImageTransferSyntaxes = new DicomTransferSyntax[]
                                                                                 {
                                                                                         // Lossless
                                                                                         DicomTransferSyntax.JPEGLSLossless,
                                                                                         DicomTransferSyntax.JPEG2000Lossless,
                                                                                         DicomTransferSyntax.JPEGProcess14SV1,
                                                                                         DicomTransferSyntax.JPEGProcess14,
                                                                                         DicomTransferSyntax.RLELossless,

                                                                                         // Lossy
                                                                                         DicomTransferSyntax.JPEGLSNearLossless,
                                                                                         DicomTransferSyntax.JPEG2000Lossy,
                                                                                         DicomTransferSyntax.JPEGProcess1,
                                                                                         DicomTransferSyntax.JPEGProcess2_4,

                                                                                         // Uncompressed
                                                                                         DicomTransferSyntax.ExplicitVRLittleEndian,
                                                                                         DicomTransferSyntax.ExplicitVRBigEndian,
                                                                                         DicomTransferSyntax.ImplicitVRLittleEndian
                                                                                 };

        public CStoreSCPProvider(INetworkStream stream, Encoding fallbackEncoding, Logger log)
            : base(stream, fallbackEncoding, log)
        {
        }

        public DicomCEchoResponse OnCEchoRequest(DicomCEchoRequest request)
        {
            return new DicomCEchoResponse(request, DicomStatus.Success);
        }

        public void OnConnectionClosed(Exception exception)
        {
        }

        public DicomCStoreResponse OnCStoreRequest(DicomCStoreRequest request)
        {
            lock (_mutex)
            {
                _index++;
            }

            var studyUid = request.Dataset.Get<string>(DicomTag.StudyInstanceUID);
            var instUid = request.SOPInstanceUID.UID;

            if (!Directory.Exists(_storagePath))
                Directory.CreateDirectory(_storagePath);

            string path = _storagePath + "\\" + _index  + ".dcm";
            request.File.Save(path);

      //      Thread.Sleep(2000);
      //      Logger.Info($"Got cstore request: {instUid}");
            return new DicomCStoreResponse(request, DicomStatus.Success);
        }

        public void OnCStoreRequestException(string tempFileName, Exception e)
        {
   //         throw new NotImplementedException();
        }

        public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
   //         throw new NotImplementedException();
        }

        public void OnReceiveAssociationReleaseRequest()
        {
            SendAssociationReleaseResponse();
        }

        public void OnReceiveAssociationRequest(DicomAssociation association)
        {
            if (association.CalledAE != "STORESCP")
            {
                SendAssociationReject(
                    DicomRejectResult.Permanent,
                    DicomRejectSource.ServiceUser,
                    DicomRejectReason.CalledAENotRecognized);
                return;
            }

            foreach (var pc in association.PresentationContexts)
            {
                //if (pc.AbstractSyntax == DicomUID.Verification)
                //    pc.AcceptTransferSyntaxes(AcceptedTransferSyntaxes);
                //else if (pc.AbstractSyntax.StorageCategory != DicomStorageCategory.None)
                //    pc.AcceptTransferSyntaxes(AcceptedImageTransferSyntaxes);
                if (pc.AbstractSyntax == DicomUID.Verification)
                    pc.SetResult(DicomPresentationContextResult.Accept);
                if (pc.AbstractSyntax == DicomUID.CTImageStorage)
                    pc.SetResult(DicomPresentationContextResult.Accept);
            }

            SendAssociationAccept(association);
        }
    }
}
