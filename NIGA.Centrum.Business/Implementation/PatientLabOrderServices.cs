using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Entity.DataModels;
using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace NIGA.Centrum.Business.Implementation
{
    /// <summary>
    /// Implementation for PatientLabOrder
    /// </summary>
    public class PatientLabOrderServices : IPatientLabOrderServices
    {
        NIGACentrumContext context;
        public PatientLabOrderServices(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }


        /// <summary>
        /// Mthod implementation implementaion for SavePatinetLabOrder
        /// </summary>
        /// <param name="patientLabOrderModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string SavePatinetLabOrder(PatientLabOrderModel patientLabOrderModel, ref ErrorResponseModel errorResponseModel)
        {
            string Mesaage = "";
            if (patientLabOrderModel.PatientOrderedTestId == 0)
            {
                var patientLabOrderEntity = new PatientLabOrder();
                patientLabOrderEntity.PatientId = patientLabOrderModel.PatientId;
                patientLabOrderEntity.PatientLabTestId = patientLabOrderModel.PatientLabTestId;
                patientLabOrderEntity.OrderDate = patientLabOrderModel.OrderDate;
                patientLabOrderEntity.LabName = patientLabOrderModel.LabName;
                patientLabOrderEntity.EnteredBy = patientLabOrderModel.EnteredBy;
                patientLabOrderEntity.EnteredBy = patientLabOrderModel.EnteredBy;
                patientLabOrderEntity.DeleteStatus = false;
                context.PatientLabOrder.Add(patientLabOrderEntity);
                context.SaveChanges();
                Mesaage = "Record saved successfully";

            }
            else
            {
                var patientLabOrderEntity = context.PatientLabOrder.FirstOrDefault(x => x.PatientOrderedTestId == patientLabOrderModel.PatientOrderedTestId);
                if (patientLabOrderEntity == null)
                {
                    Mesaage = "Not found";
                    errorResponseModel.Message = "Not found";
                    errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                }
                patientLabOrderEntity.PatientId = patientLabOrderModel.PatientId;
                patientLabOrderEntity.PatientLabTestId = patientLabOrderModel.PatientLabTestId;
                patientLabOrderEntity.OrderDate = patientLabOrderModel.OrderDate;
                patientLabOrderEntity.LabName = patientLabOrderModel.LabName;
                patientLabOrderEntity.EnteredBy = patientLabOrderModel.EnteredBy;
                patientLabOrderEntity.EnteredBy = patientLabOrderModel.EnteredBy;
                patientLabOrderEntity.DeleteStatus = false;
                context.SaveChanges();
                Mesaage = "Record updated successfully";

            }
            return Mesaage;
        }


        /// <summary>
        ///  Method implementation for GetAllPatinetLabOrder
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<PatientLabOrderModel> GetAllPatinetLabOrder(ref ErrorResponseModel errorResponseModel)
        {
            var patientLabOrderModelList = new List<PatientLabOrderModel>();
            errorResponseModel = new ErrorResponseModel();
            var patientLabOrderEntities = context.PatientLabOrder.Where(x => x.DeleteStatus == false).ToList();
            if (patientLabOrderEntities.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "records not found";
            }
            patientLabOrderEntities.ForEach(item => {
                patientLabOrderModelList.Add(new PatientLabOrderModel
                {
                    PatientOrderedTestId=item.PatientOrderedTestId,
                    LabName=item.LabName,
                    OrderDate=item.OrderDate,
                    PatientId=item.PatientId,
                    PatientLabTestId =item.PatientLabTestId                   
                });
            });
            return patientLabOrderModelList.OrderByDescending(x=>x.PatientOrderedTestId).ToList();
        }


        /// <summary>
        ///  Method implementation for GetAllPatinetLabOrder
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<PatientLabOrderModel> GetPatinetLabOrder(int PatientId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var patientLabOrderEntities =(from labOrder in context.PatientLabOrder
                                          join labTest in context.PatientLabTestMaster on labOrder.PatientLabTestId equals labTest.PatientLabTestId
                                          where labOrder.PatientId== PatientId && labOrder.DeleteStatus==false
                                          select new PatientLabOrderModel
                                          {
                                              PatientOrderedTestId = labOrder.PatientOrderedTestId,
                                              LabName = labOrder.LabName,
                                              OrderDate = labOrder.OrderDate,
                                              PatientId = labOrder.PatientId,
                                              PatientLabTestId = labOrder.PatientLabTestId,
                                              PatientLabTestName = labTest.LabTestName
                                          }
                                          ).ToList();
                
            if (patientLabOrderEntities.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "records not found";
            }
           
            return patientLabOrderEntities.OrderByDescending(x=>x.PatientOrderedTestId).ToList();
        }
    }
}
