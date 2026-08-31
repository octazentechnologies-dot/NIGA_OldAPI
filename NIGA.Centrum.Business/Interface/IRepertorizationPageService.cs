using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    public interface IRepertorizationPageService
    {
        List<MateriaMedicaHeadModel> GetMateriaMedicaHeadingbyAuthorId(int authorId);
        List<DifferentialMateriaMedicaListModel> GetDifferentialMateriaMedica(DifferentialMateriaMedica differentialMateriaMedica);
    }
}
