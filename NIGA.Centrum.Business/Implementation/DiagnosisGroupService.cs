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
    /// This is implementation  for the diagnosisgroup operations 
    /// </summary>
    public class DiagnosisGroupService : IDiagnosisGroupService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public DiagnosisGroupService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }


        /// <summary>
        /// Methood to get diagnosisgroup by DiagnosisGroupId
        /// </summary>
        /// <param name="diagnosisgroupId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public DiagnosisGroupModel GetDiagnosisGroupById(long diagnosisgroupId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var diagnosisgroupEntity = context.DiagnosisGroupMaster.FirstOrDefault(x => x.DiagnosisGroupId == diagnosisgroupId && !x.DeleteStatus);
            if (diagnosisgroupEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Diagnosis Group not found";
                return null;
            }
            return new DiagnosisGroupModel
            {
                DiagnosisGroupId = diagnosisgroupEntity.DiagnosisGroupId,
                DiagnosisGroupName = diagnosisgroupEntity.DiagnosisGroupName,
                Description = diagnosisgroupEntity.Description,
                EnteredDate = diagnosisgroupEntity.EnteredDate,
                EnteredBy = diagnosisgroupEntity.EnteredBy,
                ChangedBy = diagnosisgroupEntity.ChangedBy,
                ChangedDate = diagnosisgroupEntity.ChangedDate,
                DeleteStatus = diagnosisgroupEntity.DeleteStatus,
            };
        }

        public List<DiagnosisGroupModel> GetDiagnosisGroups( ref ErrorResponseModel errorResponseModel)
        {
            var diagnosisGroupModelList = new List<DiagnosisGroupModel>();
            errorResponseModel = new ErrorResponseModel();
            var diagnosisGroupEntityList = context.DiagnosisGroupMaster.Where(x => x.DeleteStatus == false).ToList();

            if (diagnosisGroupEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "State not found";
            }
            diagnosisGroupEntityList.ForEach(item =>
            {
                diagnosisGroupModelList.Add(new DiagnosisGroupModel
                {
                    DiagnosisGroupId = item.DiagnosisGroupId,
                    DiagnosisGroupName = item.DiagnosisGroupName,
                    Description = item.Description,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    EnteredBy = item.EnteredBy,
                    EnteredDate = item.EnteredDate,
                    DeleteStatus = item.DeleteStatus
                });
            });
            return diagnosisGroupModelList;
        }


        /// <summary>
        /// Method implementation for saving new DiagnosisGroup
        /// </summary>
        /// <param name="diagnosisGroupModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string SaveDiagnosisGroup(DiagnosisGroupModel diagnosisGroupModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            if (diagnosisGroupModel.DiagnosisGroupId == 0)
            {
                DiagnosisGroupMaster diagnosisGroupEntity = new DiagnosisGroupMaster();
                diagnosisGroupEntity.DiagnosisGroupName = diagnosisGroupModel.DiagnosisGroupName;
                diagnosisGroupEntity.Description = diagnosisGroupModel.Description;
                diagnosisGroupEntity.EnteredBy = diagnosisGroupModel.EnteredBy;
                diagnosisGroupEntity.EnteredDate = DateTime.Now;
                context.DiagnosisGroupMaster.Add(diagnosisGroupEntity);
                context.SaveChanges();
                Message = "Diagnosis Group Saved Successfully";
            }
            else
            {
                var diagnosisGroupEntity = context.DiagnosisGroupMaster.FirstOrDefault(x => x.DiagnosisGroupId == diagnosisGroupModel.DiagnosisGroupId);
                if (diagnosisGroupEntity != null)
                {
                    diagnosisGroupEntity.DiagnosisGroupName = diagnosisGroupModel.DiagnosisGroupName;
                    diagnosisGroupEntity.Description = diagnosisGroupModel.Description;
                    diagnosisGroupEntity.ChangedBy = diagnosisGroupModel.EnteredBy;
                    diagnosisGroupEntity.ChangedDate = DateTime.Now;
                    context.SaveChanges();
                    Message = "Diagnosis Group Updated Successfully";
                }
            }
            return Message;
        }


        /// <summary>
        /// Method is used for delete diagnosis group.
        /// </summary>
        /// <param name="diagnosisGroupModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteDiagnosisGroup(DiagnosisGroupModel diagnosisGroupModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var diagnosisGroupEntity = context.DiagnosisGroupMaster.FirstOrDefault(x => x.DiagnosisGroupId == diagnosisGroupModel.DiagnosisGroupId);
            if (diagnosisGroupEntity != null)
            {
                diagnosisGroupEntity.DeleteStatus = diagnosisGroupModel.DeleteStatus;
                diagnosisGroupEntity.ChangedBy = diagnosisGroupModel.EnteredBy;
                diagnosisGroupEntity.ChangedDate = DateTime.Now;
                context.SaveChanges();
                Message = "Diagnosis Group Deleted Successfully";
            }
            return Message;
        }

        public List<DiagnosisGroupViewModel> GetDiagnosisGroupViewModels(ref ErrorResponseModel errorResponseModel)
        {
            var diagnosisViewModelList = new List<DiagnosisGroupViewModel>();
            errorResponseModel = new ErrorResponseModel();
            var diagnosisGroupEntity = context.DiagnosisGroupMaster.Where(x => x.DeleteStatus == false).ToList();
            if (diagnosisGroupEntity.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Not Found";
            }
            foreach (var item in diagnosisGroupEntity)
            {
                DiagnosisGroupViewModel d = new DiagnosisGroupViewModel();
                var diagnosisList = new List<DiagnosisModel>();
                d.DiagnosisGroupId = item.DiagnosisGroupId;
                d.DiagnosisGroupName = item.DiagnosisGroupName;
                var diagnosisEntity = context.DiagnosisMaster.Where(x=>x.DiagnosisGroupId== item.DiagnosisGroupId).ToList();
                foreach (var entity in diagnosisEntity)
                {
                    DiagnosisModel dm = new DiagnosisModel();
                    dm.DiagnosisId = entity.DiagnosisId;
                    dm.DiagnosisName = entity.DiagnosisName;
                    diagnosisList.Add(dm);
                }
                d.listDiagnosisModel = diagnosisList;
                diagnosisViewModelList.Add(d);
            }
            return diagnosisViewModelList;
        }
    }
}
