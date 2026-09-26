using Homeocentrum.Niga.OldAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homeocentrum.Niga.OldAPI.Business.Interface
{
    /// <summary>
    /// Interface used for gender related operations
    /// </summary>
   public interface IGenderService
    {
        /// <summary>
        /// Method is used for to get gender by genderId
        /// </summary>
        /// <param name="genderId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        GenderModel GetGenderById(long genderId, ref ErrorResponseModel errorResponseModel);
    }
}
