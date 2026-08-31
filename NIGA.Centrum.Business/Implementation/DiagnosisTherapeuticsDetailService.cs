using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Entity.DataModels;
using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace NIGA.Centrum.Business.Implementation
{
    public class DiagnosisTherapeuticsDetailService : IDiagnosisTherapeuticsDetailService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public DiagnosisTherapeuticsDetailService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }

        public string DeleteDiagnosisTherapeuticsDetail(DiagnosisTherapeuticsDetailModel diagnosisTherapeuticsDetailModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var diagnosisTherapeuticsDetailEntity = context.DiagnosisTherapeuticsDetail.FirstOrDefault(x => x.DiagnosisTherapeuticsDetailId == diagnosisTherapeuticsDetailModel.DiagnosisTherapeuticsDetailId);
            if (diagnosisTherapeuticsDetailEntity != null)
            {
                diagnosisTherapeuticsDetailEntity.DeletedStatus = true;
                context.SaveChanges();
                Message = "DiagnosisSystem Deleted Successfully";
            }
            return Message;
        }

        public DiagnosisTherapeuticsDetailModel GetDiagnosisTherapeuticsDetailById(long diagnosisTherapeuticsDetailID, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var diagnosisTherapeuticsDetailEntity = (from dtd in context.DiagnosisTherapeuticsDetail
                                                     join ds in context.DiagnosisMaster on dtd.DiagnosisId equals ds.DiagnosisId
                                                     where dtd.DiagnosisTherapeuticsDetailId==diagnosisTherapeuticsDetailID && dtd.DeletedStatus==false
                                                     select new DiagnosisTherapeuticsDetailModel
                                                     {
                                                         DiagnosisTherapeuticsDetailId = dtd.DiagnosisTherapeuticsDetailId,
                                                         DiagnosisId = dtd.DiagnosisId,
                                                         DiagnosisTherapeuticsDetail1 = dtd.DiagnosisTherapeuticsDetail1,
                                                         DeletedStatus = (bool)dtd.DeletedStatus,
                                                         DiagnosisName=ds.DiagnosisName
                                                     }
                                                     ).FirstOrDefault();
                
                
                
            if (diagnosisTherapeuticsDetailEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "DiagnosisTherapeuticsDetail not found";
            }
            return diagnosisTherapeuticsDetailEntity;
        }

        public List<DiagnosisTherapeuticsDetailModel> GetDiagnosisTherapeuticsDetails(ref ErrorResponseModel errorResponseModel)
        {
            var diagnosisTherapeuticsDetailModelList = new List<DiagnosisTherapeuticsDetailModel>();
            errorResponseModel = new ErrorResponseModel();
            var diagnosisTherapeuticsDetailEntityList = (from dtd in context.DiagnosisTherapeuticsDetail
                                                         join ds in context.DiagnosisMaster on dtd.DiagnosisId equals ds.DiagnosisId
                                                         select new DiagnosisTherapeuticsDetailModel
                                                         {
                                                             DiagnosisTherapeuticsDetailId = dtd.DiagnosisTherapeuticsDetailId,
                                                             DiagnosisId = dtd.DiagnosisId,
                                                             DiagnosisTherapeuticsDetail1 = dtd.DiagnosisTherapeuticsDetail1,
                                                             DeletedStatus = (bool)dtd.DeletedStatus,
                                                             DiagnosisName = ds.DiagnosisName
                                                         }
                                                     ).ToList();
            if (diagnosisTherapeuticsDetailEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "DrugSystem not found";
            }
            return diagnosisTherapeuticsDetailEntityList;
        }

        public string SaveDiagnosisTherapeuticsDetail(DiagnosisTherapeuticsDetailModel diagnosisTherapeuticsDetailModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            if (diagnosisTherapeuticsDetailModel.DiagnosisTherapeuticsDetailId == 0)
            {
                DiagnosisTherapeuticsDetail diagnosisTherapeuticsDetailEntity = new DiagnosisTherapeuticsDetail();
                diagnosisTherapeuticsDetailEntity.DiagnosisId = diagnosisTherapeuticsDetailModel.DiagnosisId;
                diagnosisTherapeuticsDetailEntity.DiagnosisTherapeuticsDetail1 = diagnosisTherapeuticsDetailModel.DiagnosisTherapeuticsDetail1;
                diagnosisTherapeuticsDetailEntity.DeletedStatus = false;
                context.DiagnosisTherapeuticsDetail.Add(diagnosisTherapeuticsDetailEntity);
                context.SaveChanges();
                Message = " DiagnosisTherapeuticsDetail Saved Successfully";
            }
            else
            {
                var diagnosisTherapeuticsDetailEntity = context.DiagnosisTherapeuticsDetail.FirstOrDefault(x => x.DiagnosisTherapeuticsDetailId == diagnosisTherapeuticsDetailModel.DiagnosisTherapeuticsDetailId);
                if (diagnosisTherapeuticsDetailEntity != null)
                {

                    diagnosisTherapeuticsDetailEntity.DiagnosisId = diagnosisTherapeuticsDetailModel.DiagnosisId;
                    diagnosisTherapeuticsDetailEntity.DiagnosisTherapeuticsDetail1 = diagnosisTherapeuticsDetailModel.DiagnosisTherapeuticsDetail1;
                    diagnosisTherapeuticsDetailEntity.DeletedStatus = false;
                    context.SaveChanges();
                    Message = "DiagnosisTherapeuticsDetail Updated Successfully";
                }
            }
            return Message;
        }
    }
}
