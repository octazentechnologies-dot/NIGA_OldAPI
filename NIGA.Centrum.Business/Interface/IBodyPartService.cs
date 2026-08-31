using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    /// <summary>
    /// Interface used for body part related operations
    /// </summary>
   public interface IBodyPartService
    {
        /// <summary>
        /// Method is used for to get bodypart by bodypartId
        /// </summary>
        /// <param name="bodyPartId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        BodyPartModel GetBodyPartById(long bodyPartId, ref ErrorResponseModel errorResponseModel);
       

        /// <summary>
        /// interface for getting all the bodyparts
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<BodyPartModel> GetBodyParts(ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to save BodyPart
        /// </summary>
        /// <param name="bodypartModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SaveBodyPart(BodyPartModel bodypartModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to deactivate bodypart.
        /// </summary>
        /// <param name="bodypartModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteBodyPart(BodyPartModel bodypartModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// interface for getting all the bodyparts by section
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<BodyPartModel> GetBodyPartBySection(long sectionId,ref ErrorResponseModel errorResponseModel);
    }
}
