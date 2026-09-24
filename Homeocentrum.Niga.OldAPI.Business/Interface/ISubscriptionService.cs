using Homeocentrum.Niga.OldAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homeocentrum.Niga.OldAPI.Business.Interface
{
    public interface ISubscriptionService
    {
        List<SubscriptionModel> GetSubscription(ref ErrorResponseModel errorResponseModel);

        SubscriptionModel GetSubscriptionById(long packageDetailId, ref ErrorResponseModel errorResponseModel);

        string SaveSubscription(SubscriptionModel subscriptionModel, int userId, ref ErrorResponseModel errorResponseModel);
    }
}
