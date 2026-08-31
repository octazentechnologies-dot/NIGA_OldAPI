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
    /// This is implementation  for the qualification operations 
    /// </summary>
   public class QualificationService : IQualificationService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public QualificationService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }

        /// <summary>
        /// Methood to get qualification by QualificationId
        /// </summary>
        /// <param name="qualificationId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public QualificationModel GetQualificationById(long qualificationId, ref ErrorResponseModel errorResponseModel)
        {            
            errorResponseModel = new ErrorResponseModel();
            var qualificationEntity = context.QualificationMaster.FirstOrDefault(x => x.QualificationId == qualificationId && !x.DeleteStatus);
            if (qualificationEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Qualification not found";
            }
            return new QualificationModel
            {
                QualificationId = qualificationEntity.QualificationId,
                QualificationName = qualificationEntity.QualificationName,
                QualificationAlias = qualificationEntity.QualificationAlias,
                Description = qualificationEntity.Description,
                DegreeLevel = qualificationEntity.DegreeLevel,
                EnteredDate = qualificationEntity.EnteredDate,
                EnteredBy = qualificationEntity.EnteredBy,
                ChangedBy = qualificationEntity.ChangedBy,
                ChangedDate = qualificationEntity.ChangedDate,
                DeleteStatus = qualificationEntity.DeleteStatus,
            };
        }

        /// <summary>
        /// Method to get all the qualifications
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<QualificationModel> GetQualifications(ref ErrorResponseModel errorResponseModel)
        {
            var qualificationModelList = new List<QualificationModel>();
            errorResponseModel = new ErrorResponseModel();
            var qualificationEntityList = context.QualificationMaster.Where(x => x.DeleteStatus == false).ToList();
            if (qualificationEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Qualification not found";
            }

            qualificationEntityList.ForEach(item =>
            {
                qualificationModelList.Add(new QualificationModel
                {
                    QualificationId = item.QualificationId,
                    QualificationName = item.QualificationName,
                    QualificationAlias = item.QualificationAlias,
                    Description = item.Description,
                    DegreeLevel = item.DegreeLevel,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus,
                });
            });
            return qualificationModelList;
        }

        /// <summary>
        /// Method implementation for saving new Qualification
        /// </summary>
        /// <param name="qualificationModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string SaveQualification(QualificationModel qualificationModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            if (qualificationModel.QualificationId == 0)
            {
                QualificationMaster qualificationEntity = new QualificationMaster();
                qualificationEntity.QualificationName = qualificationModel.QualificationName;
                qualificationEntity.QualificationAlias = qualificationModel.QualificationAlias;
                qualificationEntity.Description = qualificationModel.Description;
                qualificationEntity.DegreeLevel = qualificationModel.DegreeLevel;
                qualificationEntity.EnteredBy = qualificationModel.EnteredBy;
                qualificationEntity.EnteredDate = DateTime.Now;
                context.QualificationMaster.Add(qualificationEntity);
                context.SaveChanges();
                Message = "Qualification Saved Successfully";
            }
            else
            {
                var qualificationEntity = context.QualificationMaster.FirstOrDefault(x => x.QualificationId == qualificationModel.QualificationId);
                if (qualificationEntity != null)
                {

                    qualificationEntity.QualificationName = qualificationModel.QualificationName;
                    qualificationEntity.QualificationAlias = qualificationModel.QualificationAlias;
                    qualificationEntity.Description = qualificationModel.Description;
                    qualificationEntity.DegreeLevel = qualificationModel.DegreeLevel;
                    qualificationEntity.ChangedBy = qualificationModel.EnteredBy;
                    qualificationEntity.ChangedDate = DateTime.Now;
                    context.SaveChanges();
                    Message = "Qualification Updated Successfully";
                }
            }
            return Message;
        }


        /// <summary>
        /// Method is used for delete Qualification.
        /// </summary>
        /// <param name="qualificationModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteQualification(QualificationModel qualificationModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var qualificationEntity = context.QualificationMaster.FirstOrDefault(x => x.QualificationId == qualificationModel.QualificationId);
            if (qualificationEntity != null)
            {
                qualificationEntity.DeleteStatus = qualificationModel.DeleteStatus;
                qualificationEntity.ChangedBy = qualificationModel.EnteredBy;
                qualificationEntity.ChangedDate = DateTime.Now;
                context.SaveChanges();
                Message = "Qualification Deleted Successfully";
            }
            return Message;
        }
    }
}
