using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Entity.DataModels;
using NIGA.Centrum.Model;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;

namespace NIGA.Centrum.Business.Implementation
{
    public class DropdownListService:IDropdownListService
    {

        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public DropdownListService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }
        public List<ThermalModel> GetAllThermalDDL()
        {
            List<ThermalModel> thermalsDDL = new List<ThermalModel>();

            thermalsDDL = (from thermalMaster in context.ThermalMaster
                           where thermalMaster.DeleteStatus==false
                           select new ThermalModel
                           {
                               ThermalId = thermalMaster.ThermalId,
                               ThermalName = thermalMaster.ThermalName
                           }
                            ).ToList();

            return thermalsDDL;
        }

        public List<ThermalModel> GetHeadingbyAuthorDDL(int authorId)
        {
            List<ThermalModel> thermalsDDL = new List<ThermalModel>();

            thermalsDDL = (from thermalMaster in context.ThermalMaster
                           where thermalMaster.DeleteStatus == false
                           select new ThermalModel
                           {
                               ThermalId = thermalMaster.ThermalId,
                               ThermalName = thermalMaster.ThermalName
                           }
                            ).ToList();

            return thermalsDDL;
        }

        public List<AuthorMasterModel> GetAuthorforMateriaMedica()
        {
            var authorModelList = new List<AuthorMasterModel>();
            var authorEntityList = context.AuthorMaster.Where(x => x.IsDeleted == false && x.IsForRepertory == false).ToList();

            authorEntityList.ForEach(item =>
            {
                authorModelList.Add(new AuthorMasterModel
                {
                    AuthorId = item.AuthorId,
                    AuthorName = item.AuthorName,
                    AuthorAlias = item.AuthorAlias,
                });
            });
            return authorModelList;
        }

        public List<PatientLabTestModel> GetPatientLabTestDDl()
        {

           var patientLabTestDDl = (from patientLabTest in context.PatientLabTestMaster
                           where patientLabTest.DeleteStatus == false
                           select new PatientLabTestModel
                           {
                               PatientLabTestId = patientLabTest.PatientLabTestId,
                               LabTestName = patientLabTest.LabTestName,
                               Description = patientLabTest.Description,
                           }
                            ).ToList();

            return patientLabTestDDl;
        }

        public List<QuestionGroupModelDDL> GetQuestionGroupDDL(ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();

            var questionGroupList = (from questionGroup in context.QuestionGroupMaster
                                     where questionGroup.DeleteStatus == false
                                     select new QuestionGroupModelDDL
                                     {
                                         QuestionGroupId = questionGroup.QuestionGroupId,
                                         QuestionGroupName = questionGroup.QuestionGroupName,
                                     }
                               ).ToList();

            if (questionGroupList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Question Group not found";
            }

            return questionGroupList;
        }

        public List<QuestionSectionModelDDL> GetQuestionSectionsDDL(ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();

            var questionSectionList = (from questionSection in context.QuestionSectionMaster
                                       where questionSection.DeleteStatus == false
                                       select new QuestionSectionModelDDL
                                       {
                                           QuestionSectionId = questionSection.QuestionSectionId,
                                           QuestionSectionName = questionSection.QuestionSectionName,
                                       }
                               ).ToList();

            if (questionSectionList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Question Section not found";
            }

            return questionSectionList;
        }

        public List<QuestionSubGroupModelDDL> GetQuestionSubGroupDDL(ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();

            var questionSectionList = (from questionSubGroup in context.QuestionSubgroup
                                       where questionSubGroup.DeleteStatus == false
                                       select new QuestionSubGroupModelDDL
                                       {
                                           QuestionSubgroupId = questionSubGroup.QuestionSubgroupId,
                                           QuestionSubgroup1 = questionSubGroup.QuestionSubgroup1,
                                       }
                              ).ToList();

            if (questionSectionList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "QuestionSubGroup Not Found";
            }

            return questionSectionList;
        }

        public List<BodyPartDDLModel> GetBodyPartDDL(int sectionId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();

            var bodyPartList = (from bodyPart in context.BodyPartMaster
                                where bodyPart.DeleteStatus == false && bodyPart.SectionId == sectionId
                                select new BodyPartDDLModel
                                {
                                    BodyPartId = bodyPart.BodyPartId,
                                    BodyPartName = bodyPart.BodyPartName,
                                }
                              ).ToList();

            if (bodyPartList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Body Part Not Found";
            }

            return bodyPartList;
        }

        public List<QuestionSubGroupModelDDL> GetSubQuestionGroupByQGIDQSIDDDL(int questionGroupId, int questionSectionId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();

            var questionSubGroupList = (from questionSection in context.QuestionSectionMaster
                                        join questionGroup in context.QuestionGroupMaster on questionSection.QuestionSectionId equals questionGroup.QuestionSectionId
                                        join questionSubGroup in context.QuestionSubgroup on questionGroup.QuestionGroupId equals questionSubGroup.QuestionGroupId
                                        where questionSection.QuestionSectionId == questionSectionId
                                        && questionGroup.QuestionGroupId == questionGroupId
                                        && questionSubGroup.DeleteStatus == false
                                        && questionSection.DeleteStatus == false && questionGroup.DeleteStatus == false
                                        select new QuestionSubGroupModelDDL
                                        {
                                            QuestionSubgroupId = questionSubGroup.QuestionSubgroupId,
                                            QuestionSubgroup1 = questionSubGroup.QuestionSubgroup1,
                                        }
                              ).ToList();

            if (questionSubGroupList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Question SubGroup Not Found";
            }
            else
            {
                var subGroupIds = questionSubGroupList.Select(x => x.QuestionSubgroupId).ToList();
                var sectionLinks = context.QuestionSubgroupSection
                    .Where(x => subGroupIds.Contains(x.QuestionSubgroupId) && !x.DeleteStatus)
                    .Select(x => new { x.QuestionSubgroupId, x.SectionId })
                    .ToList();

                foreach (var sg in questionSubGroupList)
                {
                    sg.SectionIds = sectionLinks
                        .Where(x => x.QuestionSubgroupId == sg.QuestionSubgroupId)
                        .Select(x => x.SectionId)
                        .Distinct()
                        .ToList();
                }
            }

            return questionSubGroupList;
        }

        /// <summary>
        /// Method to get all the subsections
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<SubSectionDDLModel> GetSubsectionBySection(long sectionId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var subsectionEntityList =(from subSection in context.SubSectionMaster
                                       where subSection.SectionId==sectionId && subSection.DeleteStatus==false
                                       orderby subSection.SubSectionName
                                       select new SubSectionDDLModel
                                       {
                                           SubSectionId = subSection.SubSectionId,
                                           SubSectionName = subSection.SubSectionName,
                                           MainParentSubsection = (bool)subSection.MainParentSubsection
                                       }).ToList();
            if (subsectionEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "SubSection not found";
            }
            return subsectionEntityList;
        }

       
    }
}
