using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    /// <summary>
    /// Interface used for MateriaMedicaDetails related operations
    /// </summary>

    public interface IMateriaMedicaDetailService
    {
        /// <summary>
        /// Method is used for to get Materiamedica by materiamedicadetailId
        /// </summary>
        /// <param name="materiamedicadetailId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        MateriaMedicaDetailModel GetMateriaMedicaDetailsById(long materiamedicadetailId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// interface for getting all the MateriaMedicaDetails
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<MateriaMedicaDetailModel> GetMateriaMedicaDetails(ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to save MateriaMedicaDetails
        /// </summary>
        /// <param name="materiamedicadetailmodel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SaveMateriaMedicaDetails(MateriaMedicaDetailModel materiamedicadetailmodel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to deactivate MateriaMedicaDetails.
        /// </summary>
        /// <param name="materiamedicadetailmodel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteMateriaMedicaDetails(MateriaMedicaDetailModel materiamedicadetailmodel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// interface for getting all the MateriaMedicaDetails by AuMateriaMedicathor
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<MateriaMedicaDetailModel> GetMateriaMedicaDetail(long materiamedicaId, ref ErrorResponseModel errorResponseModel);


    }
}