using Homeocentrum.Niga.OldAPI.Business.Interface;
using Homeocentrum.Niga.OldAPI.Entity.DataModels;
using Homeocentrum.Niga.OldAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Homeocentrum.Niga.OldAPI.Business.Implementation
{
    /// <summary>
    /// This is implementation  for the gender operations 
    /// </summary>
   public class GenderService : IGenderService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public GenderService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }

        /// <summary>
        /// Method to get gender by gender id
        /// </summary>
        /// <param name="genderId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public GenderModel GetGenderById(long genderId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var genderEntity = context.GenderMaster.FirstOrDefault(x => x.GenderId == genderId && !x.DeleteStatus);
            if (genderEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Gender not found";
            }
            return new GenderModel
            {
                GenderId = genderEntity.GenderId,
                GenderName = genderEntity.GenderName,
                EnteredDate = genderEntity.EnteredDate,
                EnteredBy = genderEntity.EnteredBy,
                ChangedBy = genderEntity.ChangedBy,
                ChangedDate = genderEntity.ChangedDate,
                DeleteStatus = genderEntity.DeleteStatus,
            };
        }
    }
}
