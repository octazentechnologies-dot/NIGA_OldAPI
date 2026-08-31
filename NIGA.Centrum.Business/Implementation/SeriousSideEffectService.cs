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
    public class SeriousSideEffectService : ISeriousSideEffectService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public SeriousSideEffectService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }

        /// <summary>
        /// Method is used for delete SeriousSideEffect.
        /// </summary>
        /// <param name="seriousSideEffectModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteSeriousSideEffect(SeriousSideEffectModel seriousSideEffectModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var seriousSideEffectEntity = context.SeriousSideEffectMaster.FirstOrDefault(x => x.SeriousSideEffectId == seriousSideEffectModel.SeriousSideEffectId);
            if (seriousSideEffectEntity != null)
            {
                seriousSideEffectEntity.DeleteStatus = true;
                context.SaveChanges();
                Message = "SeriousSideEffect Deleted Successfully";
            }
            return Message;
        }
    }
}
