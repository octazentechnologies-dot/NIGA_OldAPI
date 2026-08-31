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
    public class SubscriptionService : ISubscriptionService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public SubscriptionService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }

        public List<SubscriptionModel> GetSubscription(ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var subscriptionEntityList = (from subscriptionEntity in context.PackageEntryDetails
                                          select new SubscriptionModel
                                          {
                                              PackageDetailId = subscriptionEntity.PackageId,
                                              PackageId = subscriptionEntity.PackageId,
                                              DoctorId = subscriptionEntity.DoctorId,
                                              ActivationDate = subscriptionEntity.ActivationDate,
                                              ExpiryDate = subscriptionEntity.ExpiryDate,
                                              TransactionId = subscriptionEntity.TransactionId,
                                              OrderId = subscriptionEntity.OrderId,
                                              PaymentId = subscriptionEntity.PaymentId,
                                              IsActive = subscriptionEntity.IsActive
                                          }
                                           ).ToList();
                
                

            if (subscriptionEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Pathology not found";
            }
           
            return subscriptionEntityList;
        }

        public SubscriptionModel GetSubscriptionById(long packageDetailId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var subscriptionEntity = context.PackageEntryDetails.Where(x => x.PackageDetailId == packageDetailId).FirstOrDefault();
            if (subscriptionEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Subscription not found";
            }
            return new SubscriptionModel
            {
                PackageDetailId = subscriptionEntity.PackageId,
                PackageId = subscriptionEntity.PackageId,
                DoctorId = subscriptionEntity.DoctorId,
                ActivationDate = subscriptionEntity.ActivationDate,
                ExpiryDate = subscriptionEntity.ExpiryDate,
                TransactionId = subscriptionEntity.TransactionId,
                OrderId = subscriptionEntity.OrderId,
                PaymentId = subscriptionEntity.PaymentId,
                IsActive = subscriptionEntity.IsActive
            };
       
        }

        //public string SaveSubscription(SubscriptionModel subscriptionModel,int userId, ref ErrorResponseModel errorResponseModel)
        //{
        //    string Message = "";
        //    DateTime? expireDate=DateTime.Now; 

        //    var planDetail = context.PackageMaster.Where(x => x.PackageId == subscriptionModel.PackageId).FirstOrDefault();
        //    if (planDetail != null)
        //    {
        //        expireDate = Convert.ToDateTime(subscriptionModel.ActivationDate).AddDays(planDetail.ValidityInDays);
        //    }


        //    if (subscriptionModel.PackageDetailId == 0)
        //    {
        //        PackageEntryDetails subscriptionEntity = new PackageEntryDetails();
        //        subscriptionEntity.PackageId = subscriptionModel.PackageId;
        //        subscriptionEntity.DoctorId = subscriptionModel.DoctorId;
        //        subscriptionEntity.ActivationDate = subscriptionModel.ActivationDate;
        //        subscriptionEntity.ExpiryDate = expireDate;
        //        subscriptionEntity.TransactionId = subscriptionModel.TransactionId;
        //        subscriptionEntity.OrderId = subscriptionModel.OrderId;
        //        subscriptionEntity.PaymentId = subscriptionModel.PaymentId;
        //        subscriptionEntity.IsActive = true;
        //        subscriptionEntity.CreatedBy = userId;
        //        subscriptionEntity.CreatedDate = DateTime.Now;

        //        context.PackageEntryDetails.Add(subscriptionEntity);
        //        context.SaveChanges();
        //        Message = "Subscription Saved Successfully";
        //    }
        //    else
        //    {
        //        var subscriptionEntity = context.PackageEntryDetails.FirstOrDefault(x => x.PackageDetailId == subscriptionModel.PackageDetailId);
        //        if (subscriptionEntity != null)
        //        {
        //            subscriptionEntity.PackageId = subscriptionModel.PackageId;
        //            subscriptionEntity.DoctorId = subscriptionModel.DoctorId;
        //            subscriptionEntity.ActivationDate = subscriptionModel.ActivationDate;
        //            subscriptionEntity.ExpiryDate = expireDate;
        //            subscriptionEntity.TransactionId = subscriptionModel.TransactionId;
        //            subscriptionEntity.OrderId = subscriptionModel.OrderId;
        //            subscriptionEntity.PaymentId = subscriptionModel.PaymentId;
        //            subscriptionEntity.IsActive = true;

        //            context.SaveChanges();
        //            Message = "Subscription Updated Successfully";
        //        }
        //    }
        //    return Message;
        //}


        public string SaveSubscription(SubscriptionModel subscriptionModel, int userId, ref ErrorResponseModel errorResponseModel)
        {
            string message = string.Empty;

            try
            {
                DateTime? expireDate = DateTime.Now;

                var planDetail = context.PackageMaster.FirstOrDefault(x => x.PackageId == subscriptionModel.PackageId);
                if (planDetail != null)
                {
                    expireDate = Convert.ToDateTime(subscriptionModel.ActivationDate).AddDays(planDetail.ValidityInDays);
                }

                if (subscriptionModel.PackageDetailId == 0)
                {
                    // Insert new subscription
                    var subscriptionEntity = new PackageEntryDetails
                    {
                        PackageId = subscriptionModel.PackageId,
                        DoctorId = subscriptionModel.DoctorId,
                        ActivationDate = subscriptionModel.ActivationDate,
                        ExpiryDate = expireDate,
                        TransactionId = subscriptionModel.TransactionId,
                        OrderId = subscriptionModel.OrderId,
                        PaymentId = subscriptionModel.PaymentId,
                        IsActive = true,
                        CreatedBy = userId,
                        CreatedDate = DateTime.Now
                    };

                    context.PackageEntryDetails.Add(subscriptionEntity);
                    context.SaveChanges();

                    message = "Subscription saved successfully.";
                }
                else
                {
                    // Update existing subscription
                    var subscriptionEntity = context.PackageEntryDetails.FirstOrDefault(x => x.PackageDetailId == subscriptionModel.PackageDetailId);
                    if (subscriptionEntity != null)
                    {
                        subscriptionEntity.PackageId = subscriptionModel.PackageId;
                        subscriptionEntity.DoctorId = subscriptionModel.DoctorId;
                        subscriptionEntity.ActivationDate = subscriptionModel.ActivationDate;
                        subscriptionEntity.ExpiryDate = expireDate;
                        subscriptionEntity.TransactionId = subscriptionModel.TransactionId;
                        subscriptionEntity.OrderId = subscriptionModel.OrderId;
                        subscriptionEntity.PaymentId = subscriptionModel.PaymentId;
                        subscriptionEntity.IsActive = true;

                        context.SaveChanges();

                        message = "Subscription updated successfully.";
                    }
                    else
                    {
                        errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                        //errorResponseModel.ErrorMessage = "Subscription record not found.";
                        message = "Failed: Subscription not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                errorResponseModel.StatusCode = HttpStatusCode.InternalServerError;
                //errorResponseModel.ErrorMessage = "An error occurred while saving the subscription.";
                //errorResponseModel.ExceptionMessage = ex.Message;

                // Optional: Log the detailed exception
                // LogError(ex); // replace with your logger if available

                message = "Error: Unable to process the subscription.";
            }

            return message;
        }

    }
}
