using Homeocentrum.Niga.OldAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homeocentrum.Niga.OldAPI.Business.Interface
{
    public interface IRepertorizationPageService
    {
        List<MateriaMedicaHeadModel> GetMateriaMedicaHeadingbyAuthorId(int authorId);
        List<DifferentialMateriaMedicaListModel> GetDifferentialMateriaMedica(DifferentialMateriaMedica differentialMateriaMedica);
    }
}
