using Microsoft.Extensions.Options;
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
    public class EnquiryDetailService : IEnquiryDetailService
    {

        NIGACentrumContext context;
        EmailSenderService emailSenderService = new EmailSenderService();

        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public EnquiryDetailService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }

        /// <summary>
        /// Interface is used to deactivate EnquiryDetail.
        /// </summary>
        /// <param name="enquiryId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteEnquiryDetail(long enquiryId, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var enquiryEntity = context.EnquiryDetails.FirstOrDefault(x => x.EnquiryId == enquiryId);
            if (enquiryEntity != null)
            {
                enquiryEntity.EnquiryStatus = false;
                context.SaveChanges();
                Message = "Enquiry Detail Deleted Successfully";
            }
            return Message;
        }

        /// <summary>
        /// interface for getting all the EnquiryDetails
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<EnquiryDetailModel> GetAllEnquiryDetails(ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var enquiryList = new List<EnquiryDetailModel>();
            var enquiryEntity = context.EnquiryDetails.Where(x => x.EnquiryStatus == true).ToList();
            if (enquiryEntity.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Enquiry Details not found";
            }
            enquiryEntity.ForEach(item =>
            {
                enquiryList.Add(new EnquiryDetailModel
                {
                    EnquiryId = item.EnquiryId,
                    EnquiryName = item.EnquiryName,
                    EnquiryDate = item.EnquiryDate,
                    EmailId = item.EmailId,
                    MobileNo = item.MobileNo,
                    EnquiryDetails1 = item.EnquiryDetails1,
                    EnquiryStatus = item.EnquiryStatus,
                  
                });
            });
            return enquiryList;
        }

        /// <summary>
        /// Method is used for to get EnquiryDetail by enquiryId
        /// </summary>
        /// <param name="enquiryId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public EnquiryDetailModel GetEnquiryDetailById(long enquiryId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var enquiryEntity = context.EnquiryDetails.Where(x => x.EnquiryId == enquiryId).FirstOrDefault();
            if (enquiryEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Enquiry Details not found";
            }
            return new EnquiryDetailModel
            {
                EnquiryId = enquiryEntity.EnquiryId,
                EnquiryName = enquiryEntity.EnquiryName,
                EnquiryDate = enquiryEntity.EnquiryDate,
                EmailId = enquiryEntity.EmailId,
                MobileNo = enquiryEntity.MobileNo,
                EnquiryDetails1 = enquiryEntity.EnquiryDetails1,
                EnquiryStatus = enquiryEntity.EnquiryStatus,
            };
        }

        /// <summary>
        /// Interface is used to save/update EnquiryDetails
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string SaveEnquiryDetail(EnquiryDetailModel model, SmtpSettingsModel smtpSettingsModel, ref ErrorResponseModel errorResponseModel)
            {
                string message = "";
                if (model.EnquiryId == 0)
                {
                    EnquiryDetails details = new EnquiryDetails();
                    details.EnquiryId = model.EnquiryId;
                    details.EnquiryName = model.EnquiryName;
                    details.EnquiryDate = model.EnquiryDate;
                    details.EmailId = model.EmailId;
                    details.MobileNo = model.MobileNo;
                    details.EnquiryDetails1 = model.EnquiryDetails1;
                    details.EnquiryStatus = true;
                    context.EnquiryDetails.Add(details);
                ////Send mail to user////
                try
                {
                    //Send encrypted user id to mail//
                    StringBuilder strBody = new StringBuilder();
                    strBody.Append("<body>");
                    strBody.Append("<P>EnquiryName" + ":"+ model.EnquiryName);
                    strBody.Append("<P>EnquiryDate" + ":" + (model.EnquiryDate).ToString());
                    strBody.Append("<P>MobileNo" + ":" + model.MobileNo);
                    strBody.Append("<P>EnquiryDetails1" + ":" + model.EnquiryDetails1);
                    strBody.Append("</body>");
                    var emailSenderModel = new EmailSenderModel();
                    emailSenderModel.ToAddress = smtpSettingsModel.from;
                    emailSenderModel.Body = strBody.ToString();
                    emailSenderModel.isHtml = true;
                    emailSenderModel.Subject = "Enquiry Details";
                    emailSenderModel.sentStatus = emailSenderService.SendMail(emailSenderModel, smtpSettingsModel);
                }
                catch (Exception ex)
                {

                }

                 context.SaveChanges();
                 message = "Enquiry Details saved Successfully";
                   


                }
                else
                {
                    var details = context.EnquiryDetails.FirstOrDefault(x => x.EnquiryId == model.EnquiryId);
                    if (details != null)
                    {
                        details.EnquiryId = model.EnquiryId;
                        details.EnquiryName = model.EnquiryName;
                        details.EnquiryDate = model.EnquiryDate;
                        details.EmailId = model.EmailId;
                        details.MobileNo = model.MobileNo;
                        details.EnquiryDetails1 = model.EnquiryDetails1;
                        details.EnquiryStatus = true;
                        context.EnquiryDetails.Add(details);   
                        context.SaveChanges();
                        message = "Enquiry Details Update Successfully";
                    }
                } 
                return message;
        }
    }
}
