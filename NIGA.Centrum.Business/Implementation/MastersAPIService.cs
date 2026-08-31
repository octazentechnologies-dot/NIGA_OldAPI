using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Entity.DataModels;
using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;

namespace NIGA.Centrum.Business.Implementation
{
    /// <summary>
    /// This is implementation  for the master Get operations 
    /// </summary>
    public class MastersAPIService : IMastersAPIService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public MastersAPIService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }


        /// <summary>
        /// Method to get all the states
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<StateModel> GetStates(ref ErrorResponseModel errorResponseModel)
        {
            var stateModelList = new List<StateModel>();
            errorResponseModel = new ErrorResponseModel();
            var stateEntityList = context.StateMaster.ToList();
            if (stateEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "State not found";
            }
            stateEntityList.ForEach(item =>
            {
                stateModelList.Add(new StateModel
                {
                    StateId = item.StateId,
                    StateName = item.StateName,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus,
                    CountryId = item.CountryId
                });
            });
            return stateModelList;
        }

        /// <summary>
        /// Method to get all countries
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<CountryModel> GetCountries(ref ErrorResponseModel errorResponseModel)
        {
            var countryModelList = new List<CountryModel>();
            errorResponseModel = new ErrorResponseModel();
            var countryEntityList = context.CountryMaster.ToList();
            if (countryEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Country not found";
            }
            countryEntityList.ForEach(item =>
            {
                countryModelList.Add(new CountryModel
                {
                    CountryId = item.CountryId,
                    CountryName = item.CountryName,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus,
                });
            });

            return countryModelList;
        }



        /// <summary>
        /// Method to get all the genders
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<GenderModel> GetGenders(ref ErrorResponseModel errorResponseModel)
        {
            var genderModelList = new List<GenderModel>();
            errorResponseModel = new ErrorResponseModel();
            var genderEntityList = context.GenderMaster.ToList();
            if (genderEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Gender not found";
            }

            genderEntityList.ForEach(item =>
            {
                genderModelList.Add(new GenderModel
                {
                    GenderId = item.GenderId,
                    GenderName = item.GenderName,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus,
                });
            });
            return genderModelList;
        }

        /// <summary>
        /// Method to get all the packages
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<PackageModel> GetPackages(ref ErrorResponseModel errorResponseModel)
        {
            var packageModelList = new List<PackageModel>();
            errorResponseModel = new ErrorResponseModel();
            var packageEntityList = context.PackageMaster.ToList();
            if (packageEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Package not found";
            }

            packageEntityList.ForEach(item =>
            {
                packageModelList.Add(new PackageModel
                {
                    PackageId = item.PackageId,
                    PackageName = item.PackageName,
                    CaseCount = item.CaseCount,
                    ValidityInDays = item.ValidityInDays,
                    Amount = item.Amount,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus,
                });
            });
            return packageModelList;
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
            var qualificationEntityList = context.QualificationMaster.ToList();
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
        /// Method to get all the diagnosisgroup
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<DiagnosisGroupModel> GetDiagnosisGroups(ref ErrorResponseModel errorResponseModel)
        {
            var diagnosisgroupModelList = new List<DiagnosisGroupModel>();
            errorResponseModel = new ErrorResponseModel();
            var diagnosisgroupEntityList = context.DiagnosisGroupMaster.ToList();
            if (diagnosisgroupEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Diagnosis Group not found";
            }

            diagnosisgroupEntityList.ForEach(item =>
            {
                diagnosisgroupModelList.Add(new DiagnosisGroupModel
                {
                    DiagnosisGroupId = item.DiagnosisGroupId,
                    DiagnosisGroupName = item.DiagnosisGroupName,
                    Description = item.Description,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus,
                });
            });
            return diagnosisgroupModelList;
        }

        /// <summary>
        /// Method to get all the diagnosis
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<DiagnosisModel> GetDiagnosis(ref ErrorResponseModel errorResponseModel)
        {
            var diagnosisModelList = new List<DiagnosisModel>();
            errorResponseModel = new ErrorResponseModel();
            var diagnosisEntityList = context.DiagnosisMaster.ToList();
            if (diagnosisEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Diagnosis not found";
            }

            diagnosisEntityList.ForEach(item =>
            {
                diagnosisModelList.Add(new DiagnosisModel
                {
                    DiagnosisId = item.DiagnosisId,
                    DiagnosisGroupId = item.DiagnosisGroupId,
                    DiagnosisName = item.DiagnosisName,
                    DiagnosisNameAlias = item.DiagnosisNameAlias,
                    Description = item.Description,
                    //SectionId = item.SectionId,
                   // SubSectionId = item.SubSectionId,
                    Keywords = item.Keywords,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus,
                });
            });
            return diagnosisModelList;
        }

        /// <summary>
        /// Method to get all the sections
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<SectionModel> GetSections(ref ErrorResponseModel errorResponseModel)
        {
            var sectionModelList = new List<SectionModel>();
            errorResponseModel = new ErrorResponseModel();
            var sectionEntityList = context.SectionMaster.Where(x=>x.DeleteStatus==false).ToList();
            if (sectionEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Section not found";
            }

            sectionEntityList.ForEach(item =>
            {
                sectionModelList.Add(new SectionModel
                {
                    SectionId = item.SectionId,
                    SectionName = item.SectionName,
                    SectionAlias = item.SectionAlias,
                    Description = item.Description,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus,
                    ParentSubSectionID = null

                });
            });
            return sectionModelList;
        }

        /// <summary>
        /// Method to get all the subsections
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<SubSectionModel> GetSubSections(ref ErrorResponseModel errorResponseModel)
        {
            var subsectionModelList = new List<SubSectionModel>();
            errorResponseModel = new ErrorResponseModel();
            var subsectionEntityList = context.SubSectionMaster.ToList();
            if (subsectionEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "SubSection not found";
            }

            subsectionEntityList.ForEach(item =>
            {
                subsectionModelList.Add(new SubSectionModel
                {
                    SubSectionId = item.SubSectionId,
                    SectionId = item.SectionId,
                    ParentSubSectionId = item.ParentSubSectionId,
                    SubSectionName = item.SubSectionName,
                    SubSectionNameAlias = item.SubSectionNameAlias,
                    Description = item.Description,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus,
                });
            });
            return subsectionModelList;
        }
        /// <summary>
        /// Method to get all the subsections
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<SubSectionModel> GetSubsectionBySection(long sectionId, ref ErrorResponseModel errorResponseModel)
        {
            var subsectionModelList = new List<SubSectionModel>();
            errorResponseModel = new ErrorResponseModel();
            var subsectionEntityList = context.SubSectionMaster.Where(x => x.SectionId == sectionId && x.DeleteStatus == false).OrderBy(x => x.SubSectionName).ToList();
            if (subsectionEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "SubSection not found";
            }

            subsectionEntityList.ForEach(item =>
            {
                subsectionModelList.Add(new SubSectionModel
                {
                    SubSectionId = item.SubSectionId,
                    SectionId = item.SectionId,
                    ParentSubSectionId = item.ParentSubSectionId,
                    SubSectionName = item.SubSectionName,
                    SubSectionNameAlias = item.SubSectionNameAlias,
                    Description = item.Description,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus,
                });
            });
            return subsectionModelList;
        }

        /// <summary>
        /// Method to get all the remedies
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<RemedyModel> GetRemedies(RubricRemedyDetailsModel rubricRemedyDetailsModel, ref ErrorResponseModel errorResponseModel)
        {
            var remedyModelList = new List<RemedyModel>();
            errorResponseModel = new ErrorResponseModel();
            var remedies = context.RubricRemedyDetails.Where(x => x.SubSectionId == rubricRemedyDetailsModel.SubSectionId 
            && x.GradeId==rubricRemedyDetailsModel.GradeId && x.DeletedStatus==false).Select(x => x.RemedyId).ToList();
            //var remedyEntityList = context.RemedyMaster.ToList();

            var remedyEntityList = (from remedyMaster in context.RemedyMaster
                                    where !remedies.Contains(remedyMaster.RemedyId) && remedyMaster.DeleteStatus == false
                                    select remedyMaster).ToList();



            if (remedyEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Remedy not found";
            }

            remedyEntityList.ForEach(item =>
            {
                remedyModelList.Add(new RemedyModel
                {
                    RemedyId = item.RemedyId,
                    RemedyName = item.RemedyName,
                    RemedyAlias = item.RemedyAlias,
                    Description = item.Description,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus,
                });
            });
            return remedyModelList;
        }

        /// <summary>
        /// Method to get all the intensities
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<IntensityModel> GetIntensities(ref ErrorResponseModel errorResponseModel)
        {
            var intensityModelList = new List<IntensityModel>();
            errorResponseModel = new ErrorResponseModel();
            var intensityEntityList = context.IntensityMaster.ToList();
            if (intensityEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Intensity not found";
            }

            intensityEntityList.ForEach(item =>
            {
                intensityModelList.Add(new IntensityModel
                {
                    IntensityId = item.IntensityId,
                    IntensityNo = item.IntensityNo,
                    Description = item.Description,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus,
                });
            });
            return intensityModelList;
        }

        /// <summary>
        /// Method to get all the remedygrades
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
        /// Method for getting all the bodyparts
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<BodyPartModel> GetBodyParts(ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var bodypartModelList = new List<BodyPartModel>();
            var bodypartEntityList = context.BodyPartMaster.ToList();
            if (bodypartEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Body Part not found";
            }
            bodypartEntityList.ForEach(item =>
            {
                bodypartModelList.Add(new BodyPartModel
                {
                    BodyPartId = item.BodyPartId,
                    SectionId = item.SectionId,
                    BodyPartName = item.BodyPartName,
                    Description = item.Description,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus
                });
            });
            return bodypartModelList;
        }

        /// <summary>
        /// Method for getting all the partlocations
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<PartLocationModel> GetPartLocations(ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var partlocationModelList = new List<PartLocationModel>();
            var partlocationEntityList = context.PartLocationMaster.ToList();
            if (partlocationEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Part Location not found";
            }
            partlocationEntityList.ForEach(item =>
            {
                partlocationModelList.Add(new PartLocationModel
                {
                    PartLocationId = item.PartLocationId,
                    PartLocationName = item.PartLocationName,
                    Description = item.Description,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus
                });
            });
            return partlocationModelList;
        }

        /// <summary>
        /// Method for getting all the questionsections
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<QuestionSectionModel> GetQuestionSections(ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var questionsectionModelList = new List<QuestionSectionModel>();
            var questionsectionEntityList = context.QuestionSectionMaster.ToList();
            if (questionsectionEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Question Section not found";
            }
            questionsectionEntityList.ForEach(item =>
            {
                questionsectionModelList.Add(new QuestionSectionModel
                {
                    QuestionSectionId = item.QuestionSectionId,
                    QuestionSectionName = item.QuestionSectionName,
                    Description = item.Desciption,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus
                });
            });
            return questionsectionModelList;
        }


        /// <summary>
        /// Method implementaion for getting all the Chief Complaints
        /// </summary>
        /// <returns></returns>
        public List<CaseEntryChiefComplaintModel> getAllChiefComplaints(ref ErrorResponseModel errorResponseModel)
        {
            var listCaseEntryChiefComplaintModel = new List<CaseEntryChiefComplaintModel>();
            var listCaseEntryChiefComplaintEntity = context.CaseEntryChiefComplaint.Select(x => new { x.ChiefComplaintName }).Distinct().ToList();
            errorResponseModel = new ErrorResponseModel();
            if (listCaseEntryChiefComplaintEntity.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Chief Complaints not found";
            }

            listCaseEntryChiefComplaintEntity.ForEach(item =>
            {
                listCaseEntryChiefComplaintModel.Add(new CaseEntryChiefComplaintModel
                {
                    ChiefComplaintName = item.ChiefComplaintName
                });
            });
            return listCaseEntryChiefComplaintModel;
        }

        /// <summary>
        /// Method to get all the Clinical Questions
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<ClinicalQuestionsModel> GetClinicalQuestions(ref ErrorResponseModel errorResponseModel)
        {
            var clinicalquestionsModelList = new List<ClinicalQuestionsModel>();
            errorResponseModel = new ErrorResponseModel();
            var clinicalquestionsEntityList = context.ClinicalQuestions.Where(x => x.DeleteStatus == false).ToList();
            if (clinicalquestionsEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Clinical Questions not found";
            }

            clinicalquestionsEntityList.ForEach(item =>
            {
                clinicalquestionsModelList.Add(new ClinicalQuestionsModel
                {
                    QuestionsId = item.QuestionsId,
                    QuestionGroupId = item.QuestionGroupId,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus,
                });
            });
            return clinicalquestionsModelList;
        }

        /// <summary>
        /// Method to get all the Question Group
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<QuestionGroupModel> GetQuestionGroup(ref ErrorResponseModel errorResponseModel)
        {
            var questiongroupModelList = new List<QuestionGroupModel>();
            errorResponseModel = new ErrorResponseModel();
            var questiongroupEntityList = context.QuestionGroupMaster.Where(x => x.DeleteStatus == false).ToList();
            if (questiongroupEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Question Group not found";
            }

            questiongroupEntityList.ForEach(item =>
            {
                questiongroupModelList.Add(new QuestionGroupModel
                {
                    QuestionGroupId = item.QuestionGroupId,
                    QuestionGroupName = item.QuestionGroupName,
                    Description = item.Description,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus,
                });
            });
            return questiongroupModelList;
        }

        /// <summary>
        /// Method is used for get all the subsection by bodypartId
        /// </summary>
        /// <param name="bodyPartId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<SubSectionModel> GetSubSectionByBodyPart(long bodyPartId, ref ErrorResponseModel errorResponseModel)
        {
            var subsectionModelList = new List<SubSectionModel>();
            errorResponseModel = new ErrorResponseModel();
            var subSectionEntities = context.SubSectionMaster.Where(x => x.BodyPartId == bodyPartId).ToList();
            if (subSectionEntities.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Sub section not found";
            }
            subSectionEntities.ForEach(item =>
            {
                subsectionModelList.Add(new SubSectionModel
                {
                    SubSectionId = item.SubSectionId,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus,
                    Description = item.Description,
                    EnteredBy = item.EnteredBy,
                    EnteredDate = item.EnteredDate,
                    SectionId = item.SectionId,
                    ParentSubSectionId = item.ParentSubSectionId,
                    SubSectionName = item.SubSectionName,
                    SubSectionNameAlias = item.SubSectionNameAlias
                });
            });

            return subsectionModelList;
        }

        /// <summary>
        /// Method is used for get all the subsection by bodypartId
        /// </summary>
        /// <param name="bodyPartId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<SubSectionModel> GetSubSectionByBodyPart(string subSectionName, ref ErrorResponseModel errorResponseModel)
        {
            var subsectionModelList = new List<SubSectionModel>();
            errorResponseModel = new ErrorResponseModel();
            var subSectionEntities = context.SubSectionMaster.Where(x => x.SubSectionName.Contains(subSectionName)).ToList();
            if (subSectionEntities.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Sub section not found";
            }
            subSectionEntities.ForEach(item =>
            {
                subsectionModelList.Add(new SubSectionModel
                {
                    SubSectionId = item.SubSectionId,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus,
                    Description = item.Description,
                    EnteredBy = item.EnteredBy,
                    EnteredDate = item.EnteredDate,
                    SectionId = item.SectionId,
                    ParentSubSectionId = item.ParentSubSectionId,
                    SubSectionName = item.SubSectionName,
                    SubSectionNameAlias = item.SubSectionNameAlias
                });
            });


            return subsectionModelList;
        }



        public List<DoctorModel> GetDoctorList(ref ErrorResponseModel errorResponseModel)
        {
            var doctorModelList = new List<DoctorModel>();
            errorResponseModel = new ErrorResponseModel();
            var doctorEntityList = context.Doctor.Where(x => x.DeleteStatus == false).ToList();
            if (doctorEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "doctors not found";
            }
            doctorEntityList.ForEach(item =>
            {
                doctorModelList.Add(new DoctorModel
                {
                    FirstName = item.FirstName,
                    MiddleName = item.MiddleName,
                    LastName = item.LastName,
                    CasePaperValidity = item.CasePaperValidity,
                    City = item.City,
                    DoctorID = item.DoctorId,
                    EmailId = item.EmailId,
                    MobileNo = item.MobileNo,
                    PackageId = item.PackageId,
                    PassingCertNo = item.PassingCertNo,
                    PassingUniversity = item.PassingUniversity,
                    PermanantAddress = item.PermanantAddress,
                    QualificationID = item.QualificationId,
                    UserId = item.UserId,
                });
            });
            return doctorModelList;
        }

        /// <summary>
        /// Method to get the doctor by userId
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<DoctorModel> GetDoctorById(long userId, ref ErrorResponseModel errorResponseModel)
        {
            var doctorModelList = new List<DoctorModel>();
            errorResponseModel = new ErrorResponseModel();
            var doctorEntityList = context.Doctor.Where(x => x.UserId == userId && !x.DeleteStatus).ToList();
            if (doctorEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "doctors not found";
            }
            doctorEntityList.ForEach(item =>
            {
                doctorModelList.Add(new DoctorModel
                {
                    
                    FirstName = item.FirstName,
                    MiddleName = item.MiddleName,
                    LastName = item.LastName,
                    DoctorName = item.FirstName + " " + item.LastName,
                    CasePaperValidity = item.CasePaperValidity,
                    City = item.City,
                    DoctorID = item.DoctorId,
                    EmailId = item.EmailId,
                    MobileNo = item.MobileNo,
                    PackageId = item.PackageId,
                    PassingCertNo = item.PassingCertNo,
                    PassingUniversity = item.PassingUniversity,
                    PermanantAddress = item.PermanantAddress,
                    QualificationID = item.QualificationId,
                    UserId = item.UserId,
                });
            });
            return doctorModelList;
        }
        /// <summary>
        /// Method to get all the module master
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<ModuleMasterModel> GetModuleMaster(ref ErrorResponseModel errorResponseModel)
        {
            var moduleMasterModelList = new List<ModuleMasterModel>();
            errorResponseModel = new ErrorResponseModel();
            var moduleMasterEntityList = context.ModuleMaster.ToList();
            if (moduleMasterEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Section not found";
            }

            moduleMasterEntityList.ForEach(item =>
            {
                moduleMasterModelList.Add(new ModuleMasterModel
                {
                    ModuleId = item.ModuleId,
                    ModuleName = item.ModuleName,
                    ModuleMarathiName = item.ModuleMarathiName,
                    ModuleIcon = item.ModuleIcon,
                    ModuleAreaName = item.ModuleAreaName,
                    Seqno = item.Seqno,
                    IsDirectNode = item.IsDirectNode,
                    ActionName = item.ActionName,
                    ControllerName = item.ControllerName,
                    ModuleUrl = item.ModuleUrl,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus,
                });
            });
            return moduleMasterModelList;
        }

        /// <summary>
        /// Method to get all the firm details
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<FirmDetailsModel> GetFirmDetails(ref ErrorResponseModel errorResponseModel)
        {
            var firmDetailsModelList = new List<FirmDetailsModel>();
            errorResponseModel = new ErrorResponseModel();
            var firmDtailsEntityList = context.FirmDetails.ToList();
            if (firmDtailsEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Firm Details not found";
            }

            firmDtailsEntityList.ForEach(item =>
            {
                firmDetailsModelList.Add(new FirmDetailsModel
                {
                    FirmId = item.FirmId,
                    FirmName = item.FirmName,
                    FirmNameMarathi = item.FirmNameMarathi,
                    FirmRegNumber = item.FirmRegNumber,
                    FirmRegDate = item.FirmRegDate,
                    FirmBranchName = item.FirmBranchName,
                    FirmBranchNameMarathi = item.FirmBranchNameMarathi,
                    FirmOfficeAddress = item.FirmOfficeAddress,
                    FirmOfficeAddressMarathi = item.FirmOfficeAddressMarathi,
                    FirmLogo = item.FirmLogo,
                    FirmPhoneNumber = item.FirmPhoneNumber,
                    FirmFaxNumber = item.FirmFaxNumber,
                    FirmEmailIid = item.FirmEmailIid,
                    MailPassword = item.MailPassword,
                    IsFederation = item.IsFederation,
                    FirmConnectionPath = item.FirmConnectionPath,
                    LanguageIds = item.LanguageIds,
                    ParentFirmId = item.ParentFirmId,
                    ModuleIds = item.ModuleIds,
                    UserLimit = item.UserLimit,
                    IsNeedToBeSingleTerminalLogin = item.IsNeedToBeSingleTerminalLogin,
                    DatabaseBackupPath = item.DatabaseBackupPath,
                    IsDateOverlap = item.IsDateOverlap,
                    ApplicationLockDate = item.ApplicationLockDate,
                    IsLockApplication = item.IsLockApplication,
                    IsSyncStaring = item.IsSyncStaring,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus,
                });
            });
            return firmDetailsModelList;
        }


        ///// <summary>
        ///// Method to get menu by roles
        ///// </summary>
        ///// <param name="errorResponseModel"></param>
        ///// <returns></returns>
        //public List<MenuMasterModel> GetMenuByRole(long userId, ref ErrorResponseModel errorResponseModel)
        //{
        //    List<MenuMasterModel> menuModelList = new List<MenuMasterModel>();
        //    errorResponseModel = new ErrorResponseModel();
        //    int roleid = context.UserMaster.FirstOrDefault(x => x.UserId == userId).RoleId;
        //    var menuEntityList = context.RoleDetails.Where(x => x.RoleId == roleid).Include(x => x.Menu).ToList();
        //    if (menuEntityList.Count == 0)
        //    {
        //        errorResponseModel.StatusCode = HttpStatusCode.NotFound;
        //        errorResponseModel.Message = "menu not found";
        //    }
        //    menuEntityList.ForEach(item =>
        //    {
        //        menuModelList.Add(new MenuMasterModel
        //        {
        //            MenuId = item.MenuId,
        //            MenuName = item.Menu.MenuName,
        //        });
        //    });
        //    return menuModelList;
        //}



        /// <summary>
        /// Method is used for get all the subsection by bodypartId
        /// </summary>
        /// <param name="bodyPartId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public PaginationResult GetSubSectionBySearch(string queryString, NigaParameters nigaParameters, ref ErrorResponseModel errorResponseModel)
        {
            var subsectionModelList = new List<SubSectionModel>();
            errorResponseModel = new ErrorResponseModel();
            var pageNumber = (nigaParameters.PageNumber <= 0) ? 1 : nigaParameters.PageNumber;
            var pageSize = 10;
            var totalRecords = 0.0;
            var totalPages = 0.0;
            var skip = 0;

           
            if (!String.IsNullOrEmpty(queryString))
            {
                subsectionModelList = (from subsectionMaster in context.SubSectionMaster
                                       where subsectionMaster.SubSectionName.ToLower().Contains(queryString.ToLower()) && subsectionMaster.DeleteStatus == false
                                       select new SubSectionModel{ 
                                            SubSectionName=  subsectionMaster.SubSectionName,
                                            SubSectionId=  subsectionMaster.SubSectionId,
                                       }).ToList();
            }
            else
            {
                //totalRecords = context.Customermerchantmappers.Count(x => x.MerchantId == customerSearchModel.MerchantID);
                //totalPages = Math.Ceiling((double)totalRecords / pageSize);
                //skip = (pageNumber - 1) * pageSize;
            }

            if (subsectionModelList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Sub section not found";
            }
            totalRecords = subsectionModelList.Count;
            totalPages = Math.Ceiling((double)totalRecords / pageSize);
            skip = (pageNumber - 1) * pageSize;


            var result = new PaginationResult();
            result.TotalPageCount = totalPages;
            result.TotalCount = totalRecords;
            result.ResultObject=subsectionModelList.Skip(skip).Take(pageSize);
            return result;
        }


        /// <summary>
        /// Method to get all the subsections
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public PaginationResult GetSubsectionBySectionWithPagination(int sectionId,string queryString, NigaParameters nigaParameters, ref ErrorResponseModel errorResponseModel)
        {
            var subsectionModelList = new List<SubSectionViewModel>();
            errorResponseModel = new ErrorResponseModel();

            var pageNumber = (nigaParameters.PageNumber <= 0) ? 1 : nigaParameters.PageNumber;
            var pageSize = 10;
            var totalRecords = 0.0;
            var totalPages = 0.0;
            var skip = 0;


            if (!String.IsNullOrEmpty(queryString))
            {
                subsectionModelList = (from subsectionMaster in context.SubSectionMaster
                                       where subsectionMaster.SubSectionName.ToLower().Contains(queryString.ToLower()) && subsectionMaster.DeleteStatus == false
                                       select new SubSectionViewModel
                                       {
                                           SubSectionId = subsectionMaster.SubSectionId,
                                           SectionId = subsectionMaster.SectionId,
                                           ParentSubSectionId = subsectionMaster.ParentSubSectionId,
                                           SubSectionName = subsectionMaster.SubSectionName,
                                           SubSectionNameAlias = subsectionMaster.SubSectionNameAlias,
                                           Description = subsectionMaster.Description,
                                       }).ToList();
            }
            else
            {
                subsectionModelList = (from subsectionMaster in context.SubSectionMaster
                                       where subsectionMaster.SectionId==sectionId && subsectionMaster.DeleteStatus == false
                                       select new SubSectionViewModel
                                       {
                                           SubSectionId = subsectionMaster.SubSectionId,
                                           SectionId = subsectionMaster.SectionId,
                                           ParentSubSectionId = subsectionMaster.ParentSubSectionId,
                                           SubSectionName = subsectionMaster.SubSectionName,
                                           SubSectionNameAlias = subsectionMaster.SubSectionNameAlias,
                                           Description = subsectionMaster.Description,
                                       }).ToList();
            }

            if (subsectionModelList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Sub section not found";
            }
            totalRecords = subsectionModelList.Count;
            totalPages = Math.Ceiling((double)totalRecords / pageSize);
            skip = (pageNumber - 1) * pageSize;


            var result = new PaginationResult();
            result.TotalPageCount = totalPages;
            result.TotalCount = totalRecords;
            result.ResultObject = subsectionModelList.Skip(skip).Take(pageSize);
            return result;
        }

    }
}
