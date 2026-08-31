using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    /// <summary>
    /// Interface used for MateriaMedicaHead related operations
    /// </summary>
    public interface IMateriaMedicaHeadMasterService
    {
        /// <summary>
        /// Method is used for to get MateriaMedicaHead by MateriaMedicaHeadId
        /// </summary>
        /// <param name="materiamedicaheadId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        MateriaMedicaHeadMasterModel GetMateriaMedicaHeadById(long materiamedicaheadId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Method is used for get all the MateriaMedicaHead
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        List<MateriaMedicaHeadMasterModel1> GetMateriaMedicaHead(ref ErrorResponseModel errorResponseModel);

        /// <summary>remedyId
        /// Interface is used to save MateriaMedicaHead
        /// </summary>
        /// <param name="materiamedicaheadModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SaveMateriaMedicaHead(MateriaMedicaHeadMasterModel materiamedicaheadModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to deactivate MateriaMedicaHead.
        /// </summary>
        /// <param name="materiamedicaheadModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteMateriaMedicaHead(MateriaMedicaHeadMasterModel materiamedicaheadModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Get Remedies by AuthorId
        /// </summary>
        /// <param name="authorId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<MateriaMedicaHeadMasterModel> GetMateriaMedicaHeadByAuthorId(long authorId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// To update differential materia medica status
        /// </summary>
        /// <param name="materiaMedicaHeadId"></param>
        /// <param name="differentialMMDefaultStatus"></param>
        /// <returns></returns>
        string UpdateDifferentialMateriaMedicadDefaultStatus(int materiaMedicaHeadId, bool differentialMMDefaultStatus);
    }
}
