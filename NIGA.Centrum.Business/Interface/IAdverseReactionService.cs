using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    /// <summary>
    /// Interface used for Adverse Reaction related operations
    /// </summary>
    public interface IAdverseReactionService
    {
        ///// <summary>
        ///// Method is used for to get adverseReaction by adverseReactionID
        ///// </summary>
        ///// <param name="adverseReactionId"></param>
        ///// <param name="errorResponseModel"></param>
        ///// <returns></returns>
        //AdverseReactionModel GetAdverseReactionnById(long adverseReactionId, ref ErrorResponseModel errorResponseModel);

        ///// <summary>
        ///// Method is used for get all the adverseReaction
        ///// </summary>
        ///// <param name=""></param>
        ///// <returns></returns>
        //List<AdverseReactionModel> GetAdverseReaction(ref ErrorResponseModel errorResponseModel);

        ///// <summary>
        ///// Interface is used to save adverseReaction
        ///// </summary>
        ///// <param name="adverseReactionModel"></param>
        ///// <param name="errorResponseModel"></param>
        ///// <returns></returns>
        //string SaveAdverseReaction(AdverseReactionModel adverseReactionModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to deactivate adverseReaction.
        /// </summary>
        /// <param name="adverseReactionModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteAdverseReaction(AdverseReactionModel adverseReactionModel, ref ErrorResponseModel errorResponseModel);
    }
}
