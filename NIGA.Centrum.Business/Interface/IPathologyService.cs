using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    public interface IPathologyService
    {

        /// <summary>
        /// Method is used for to get pathology by pathologyId
        /// </summary>
        /// <param name="pathologyId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        PathologyModel GetPathologyById(long pathologyId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// interface for getting all the pathology
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<PathologyModel> GetPathology(ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to save pathology
        /// </summary>
        /// <param name="pathologyModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SavePathology(PathologyModel pathologyModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to deactivate pathology.
        /// </summary>
        /// <param name="pathologyModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeletePathology(PathologyModel pathologyModel, ref ErrorResponseModel errorResponseModel);

    }
}
