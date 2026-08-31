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
    public class PatientLabTestService : IPatientLabTestService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Constructor
        /// </summary>
        public PatientLabTestService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }

        public List<PatientLabTestModel> GetPatientLabTests(ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var labTestMasterEntities = (from patientLabTest in context.PatientLabTestMaster
                                         where patientLabTest.DeleteStatus == false
                                         select new PatientLabTestModel
                                         {
                                             PatientLabTestId = patientLabTest.PatientLabTestId,
                                             LabTestName = patientLabTest.LabTestName,
                                             Description = patientLabTest.Description,
                                         }
                                         ).ToList();

            if (labTestMasterEntities.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "records not found";
            }

            return labTestMasterEntities;
        }


        public string DeletePatientLabTest(int patientLabTestId, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var labTestEntity = context.PatientLabTestMaster.FirstOrDefault(x => x.PatientLabTestId == patientLabTestId);
            if (labTestEntity != null)
            {
                labTestEntity.DeleteStatus = true;
                context.SaveChanges();
                Message = "Lab Test Deleted Successfully";
            }
            return Message;
        }

        public PatientLabTestModel GetPatientLabTestById(int patientLabTestId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var labTestEntity = context.PatientLabTestMaster.Where(x => x.DeleteStatus == false).FirstOrDefault(x => x.PatientLabTestId == patientLabTestId);
            if (labTestEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Lab test not found";
            }
            return new PatientLabTestModel
            {
                PatientLabTestId = labTestEntity.PatientLabTestId,
                LabTestName = labTestEntity.LabTestName,
                Description = labTestEntity.Description,
            };
        }

        public string AddEditPatientLabTest(PatientLabTestModel labTestModel, int userID, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            if (labTestModel.PatientLabTestId == 0)
            {
                PatientLabTestMaster labTestEntity = new PatientLabTestMaster();
                labTestEntity.PatientLabTestId = labTestModel.PatientLabTestId;
                labTestEntity.LabTestName = labTestModel.LabTestName;
                labTestEntity.Description = labTestModel.Description;
                labTestEntity.DeleteStatus = false;
                labTestEntity.EnteredBy = userID;
                labTestEntity.EnteredDate = DateTime.Now;
                context.PatientLabTestMaster.Add(labTestEntity);
                context.SaveChanges();
                Message = "Lab test saved successfully";
            }
            else
            {
                var labTestEntity = context.PatientLabTestMaster.FirstOrDefault(x => x.PatientLabTestId == labTestModel.PatientLabTestId);
                if (labTestEntity != null)
                {
                    labTestEntity.LabTestName = labTestModel.LabTestName;
                    labTestEntity.Description = labTestModel.Description;
                    labTestEntity.DeleteStatus = false;
                    labTestEntity.ChangedBy = userID;
                    labTestEntity.ChangedDate = DateTime.Now;
                    context.SaveChanges();
                    Message = "Lab test updated successfully";
                }
            }
            return Message;
        }
    }
}
