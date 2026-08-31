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
    /// <summary>
    /// This is implementation  for the bodypart operations 
    /// </summary>
    public class DiagnosisSystemService : IDiagnosisSystemService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public DiagnosisSystemService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }

        public string DeleteDiagnosisSystem(DiagnosisSystemModel diagnosissystemModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var diagnosissystemEntity = context.DiagnosisSystem.FirstOrDefault(x => x.DiagnosisSystemId == diagnosissystemModel.DiagnosisSystemId);
            if (diagnosissystemEntity != null)
            {
                diagnosissystemEntity.IsActive = true;
                context.SaveChanges();
                Message = "DiagnosisSystem Deleted Successfully";
            }
            return Message;
        }

        public List<DiagnosisSystemModel> GetDiagnosisSystem(ref ErrorResponseModel errorResponseModel)
        {
            var diagnosisSystemModelList = new List<DiagnosisSystemModel>();
            errorResponseModel = new ErrorResponseModel();
            var diagnosisSystemEntityList = context.DiagnosisSystem.Where(x => x.IsActive == false).ToList();
            if (diagnosisSystemEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "DrugSystem not found";
            }

            diagnosisSystemEntityList.ForEach(item =>
            {
                diagnosisSystemModelList.Add(new DiagnosisSystemModel
                {
                    DiagnosisSystemId = item.DiagnosisSystemId,
                    DiagnosisSystemName = item.DiagnosisSystemName,
                    Description = item.Description,

                });
            });
            return diagnosisSystemModelList;
        }

        public DiagnosisSystemModel GetDiagnosisSystemById(long diagnosisSystemId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var diagnosisSystemEntity = context.DiagnosisSystem.FirstOrDefault(x => x.DiagnosisSystemId == diagnosisSystemId && x.IsActive == false);
            if (diagnosisSystemEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "DiagnosisSystem not found";
            }
            return new DiagnosisSystemModel
            {
                DiagnosisSystemId = diagnosisSystemEntity.DiagnosisSystemId,
                DiagnosisSystemName = diagnosisSystemEntity.DiagnosisSystemName,
                Description = diagnosisSystemEntity.Description,
                IsActive = (bool)diagnosisSystemEntity.IsActive
            };
        }

        public string SaveDiagnosisSystem(DiagnosisSystemModel diagnosissystemModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            if (diagnosissystemModel.DiagnosisSystemId == 0)
            {
                DiagnosisSystem diagnosissystemEntity = new DiagnosisSystem();
                diagnosissystemEntity.DiagnosisSystemName = diagnosissystemModel.DiagnosisSystemName;
                diagnosissystemEntity.Description = diagnosissystemModel.Description;
                diagnosissystemEntity.IsActive = false;
                context.DiagnosisSystem.Add(diagnosissystemEntity);
                context.SaveChanges();
                Message = " DiagnosisSystem Saved Successfully";
            }
            else
            {
                var diagnosissystemEntity = context.DiagnosisSystem.FirstOrDefault(x => x.DiagnosisSystemId == diagnosissystemModel.DiagnosisSystemId);
                if (diagnosissystemEntity != null)
                {

                    diagnosissystemEntity.DiagnosisSystemName = diagnosissystemModel.DiagnosisSystemName;
                    diagnosissystemEntity.Description = diagnosissystemModel.Description;
                    diagnosissystemEntity.IsActive = false;

                    context.SaveChanges();
                    Message = "DiagnosisSystem Updated Successfully";
                }
            }
            return Message;
        }

        
    }
}
