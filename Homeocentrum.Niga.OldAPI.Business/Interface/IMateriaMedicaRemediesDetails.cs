using Homeocentrum.Niga.OldAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homeocentrum.Niga.OldAPI.Business.Interface
{
    public interface IMateriaMedicaRemediesDetails
    {
        MateriaMedicaRemediesDetailsModel GetMateriaMedicaRemediesDetails(long remedyId, long authorId,  ref ErrorResponseModel errorResponseModel);

        List<MateriaMedicaRemediesDetailsModel> GetMateriaMedicaByRemedy(long remedyId, ref ErrorResponseModel errorResponseModel);

    }
}
