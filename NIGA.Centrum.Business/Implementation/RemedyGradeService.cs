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
    /// This is implementation  for the remedy grade operations 
    /// </summary>
   public class RemedyGradeService : IRemedyGradeService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public RemedyGradeService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }

        /// <summary>
        /// Methood to get remedy grade by RemedyGradeId
        /// </summary>
        /// <param name="gradeId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public RemedyGradeModel GetRemedyGradeById(long gradeId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var remedygradeEntity = context.RemedyGradeMaster.FirstOrDefault(x => x.GradeId == gradeId && !x.DeleteStatus);
            if (remedygradeEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Remedy grade not found";
            }
            return new RemedyGradeModel
            {
                GradeId = remedygradeEntity.GradeId,
                GradeNo = remedygradeEntity.GradeNo,
                Description = remedygradeEntity.Description,
                FontName = remedygradeEntity.FontName,
                EnteredDate = remedygradeEntity.EnteredDate,
                FontStyle = remedygradeEntity.FontStyle,
                FontColor = remedygradeEntity.FontColor,
                EnteredBy = remedygradeEntity.EnteredBy,
                ChangedBy = remedygradeEntity.ChangedBy,
                ChangedDate = remedygradeEntity.ChangedDate,
                DeleteStatus = remedygradeEntity.DeleteStatus,
            };
        }

        /// <summary>
        /// Method to get all the Remedygrades
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<RemedyGradeModel> GetRemedyGrades(ref ErrorResponseModel errorResponseModel)
        {
            var remedygradeModelList = new List<RemedyGradeModel>();
            errorResponseModel = new ErrorResponseModel();
            var remedygradeEntityList = context.RemedyGradeMaster.Where(x => x.DeleteStatus == false).ToList();
            if (remedygradeEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Remedy Grade not found";
            }

            remedygradeEntityList.ForEach(item =>
            {
                remedygradeModelList.Add(new RemedyGradeModel
                {
                    GradeId = item.GradeId,
                    GradeNo = item.GradeNo,
                    Description = item.Description,
                    FontName = item.FontName,
                    EnteredDate = item.EnteredDate,
                    FontStyle = item.FontStyle,
                    FontColor = item.FontColor,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus,
                });
            });
            return remedygradeModelList;
        }

        /// <summary>
        /// Method implementation for saving new Remedy Grade
        /// </summary>
        /// <param name="remedyGradeModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string SaveRemedyGrade(RemedyGradeModel remedyGradeModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            if (remedyGradeModel.GradeId == 0)
            {
                RemedyGradeMaster remedyGradeEntity = new RemedyGradeMaster();
                remedyGradeEntity.GradeNo = remedyGradeModel.GradeNo;
                remedyGradeEntity.Description = remedyGradeModel.Description;
                remedyGradeEntity.FontName = remedyGradeModel.FontName;
                remedyGradeEntity.FontStyle = remedyGradeModel.FontStyle;
                remedyGradeEntity.FontColor = remedyGradeModel.FontColor;
                remedyGradeEntity.EnteredBy = remedyGradeModel.EnteredBy;
                remedyGradeEntity.EnteredDate = DateTime.Now;
                context.RemedyGradeMaster.Add(remedyGradeEntity);
                context.SaveChanges();
                Message = "Remedy Grade Saved Successfully";
            }
            else
            {
                var remedyGradeEntity = context.RemedyGradeMaster.FirstOrDefault(x => x.GradeId == remedyGradeModel.GradeId);
                if (remedyGradeEntity !=  null)
                {

                    remedyGradeEntity.GradeNo = remedyGradeModel.GradeNo;
                    remedyGradeEntity.Description = remedyGradeModel.Description;
                    remedyGradeEntity.FontName = remedyGradeModel.FontName;
                    remedyGradeEntity.FontStyle = remedyGradeModel.FontStyle;
                    remedyGradeEntity.FontColor = remedyGradeModel.FontColor;
                    remedyGradeEntity.ChangedBy = remedyGradeModel.EnteredBy;
                    remedyGradeEntity.ChangedDate = DateTime.Now;
                    context.SaveChanges();
                    Message = "Remedy Grade Updated Successfully";
                }
            }
            return Message;
        }


        /// <summary>
        /// Method is used for delete Remedy Grade.
        /// </summary>
        /// <param name="remedyGradeModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteRemedyGrade(RemedyGradeModel remedyGradeModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var remedyGradeEntity = context.RemedyGradeMaster.FirstOrDefault(x => x.GradeId == remedyGradeModel.GradeId);
            if (remedyGradeEntity != null)
            {
                remedyGradeEntity.DeleteStatus = remedyGradeModel.DeleteStatus;
                remedyGradeEntity.ChangedBy = remedyGradeModel.EnteredBy;
                remedyGradeEntity.ChangedDate = DateTime.Now;
                context.SaveChanges();
                Message = "Remedy Grade Deleted Successfully";
            }
            return Message;
        }
    }
}
