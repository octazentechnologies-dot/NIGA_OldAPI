using Homeocentrum.Niga.OldAPI.Business.Interface;
using Homeocentrum.Niga.OldAPI.Entity.DataModels;
using Homeocentrum.Niga.OldAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homeocentrum.Niga.OldAPI.Business.Implementation
{
    public class OtherSideEffectService : IOtherSideEffectService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public OtherSideEffectService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }



        /// <summary>
        /// Method is used for delete OtherSideEffect.
        /// </summary>
        /// <param name="otherSideEffectModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteOtherSideEffect(OtherSideEffectModel otherSideEffectModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var otherSideEffectEntity = context.OtherSideEffectMaster.FirstOrDefault(x => x.OtherSideEffectId == otherSideEffectModel.OtherSideEffectId);
            if (otherSideEffectEntity != null)
            {
                otherSideEffectEntity.DeleteStatus = true;
                context.SaveChanges();
                Message = "OtherSideEffect Deleted Successfully";
            }
            return Message;
        }
    }
}

