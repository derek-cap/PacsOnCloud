using Dicom;
using Dicom.Log;
using Dicom.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel
{
    class CFindSCPProvider : DicomService, IDicomServiceProvider, IDicomCFindProvider, IDicomCEchoProvider
    {
        public CFindSCPProvider(INetworkStream stream, Encoding fallbackEncoding, Logger log)
            : base(stream, fallbackEncoding, log)
        {
        }

        public DicomCEchoResponse OnCEchoRequest(DicomCEchoRequest request)
        {
            return new DicomCEchoResponse(request, DicomStatus.Success);
        }

        public IEnumerable<DicomCFindResponse> OnCFindRequest(DicomCFindRequest request)
        {
            DicomStatus status = DicomStatus.Success;
            var queries = GetPatients();

            foreach (var item in queries)
            {
                DicomCFindResponse rs = new DicomCFindResponse(request, DicomStatus.Pending);
                rs.Dataset = item;
                yield return rs;
            }
            DicomCFindResponse rs2 = new DicomCFindResponse(request, DicomStatus.Success);
            rs2.Dataset = queries.LastOrDefault();
            yield return rs2;
        }

        private IEnumerable<DicomDataset> GetPatients()
        {
            for (int i = 0; i < 1002; i++)
            {
                DicomDataset ds = new DicomDataset();
                ds.Add(DicomTag.PatientID, i.ToString());
                ds.Add(DicomTag.PatientName, $"Name{i}");
                ds.Add(DicomTag.PatientAge, "18");
                yield return ds;
            }
        }

        public void OnConnectionClosed(Exception exception)
        {
        }

        public void OnCStoreRequestException(string tempFileName, Exception e)
        {
            //         throw new NotImplementedException();
            Logger?.Error(e.ToString());
        }

        public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason)
        {
            string message = $"CStore provider receive abort: Source {source}, Reason {reason}";
            Logger?.Warn(message);
            //         throw new NotImplementedException();
        }

        public void OnReceiveAssociationReleaseRequest()
        {
            SendAssociationReleaseResponse();
        }

        public void OnReceiveAssociationRequest(DicomAssociation association)
        {
            if (association.CalledAE != "FindSCP")
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
                if (pc.AbstractSyntax == DicomUID.ModalityWorklistInformationModelFIND)
                    pc.SetResult(DicomPresentationContextResult.Accept);
            }

            SendAssociationAccept(association);
        }
    }
}
