using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    public interface IMateriaMedicaRemediesDetails
    {
        MateriaMedicaRemediesDetailsModel GetMateriaMedicaRemediesDetails(long remedyId, long authorId,  ref ErrorResponseModel errorResponseModel);

    }
}
