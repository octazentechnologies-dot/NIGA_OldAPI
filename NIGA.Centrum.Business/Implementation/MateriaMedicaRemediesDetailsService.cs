using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Entity.DataModels;
using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NIGA.Centrum.Business.Implementation
{
    public class MateriaMedicaRemediesDetailsService : IMateriaMedicaRemediesDetails
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public MateriaMedicaRemediesDetailsService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }


        public MateriaMedicaRemediesDetailsModel GetMateriaMedicaRemediesDetails(long remedyId, long authorId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var MateriaMedicaList = new List<MateriaMedicaRemediesDetailsModel>();
            var M1 = context.MateriaMedicaMaster.Where(x => x.AuthorId == authorId && x.RemedyId == remedyId).FirstOrDefault();
            var materiamedicaremediesEntity = (from remedydetail in context.MateriaMedicaDetail
                                               join materiaMedica in context.MateriaMedicaMaster
                                               on remedydetail.MateriaMedicaId equals materiaMedica.MateriaMedicaId
                                               join head in context.MateriaMedicaHeadMaster
                                               on materiaMedica.MateriaMedicaHeadId equals head.MateriaMedicaHeadId
                                               where materiaMedica.RemedyId==remedyId && materiaMedica.AuthorId==authorId
                                               && materiaMedica.IsDeleted == false
                                               select new
                                               {
                                                   head.MateriaMedicaHeadName,
                                                   head.MateriaMedicaHeadId,
                                                   remedydetail.MateriaMedicaDetail1,

                                               }).Distinct().ToList();

            if (materiamedicaremediesEntity.Count == 0)
            {
                errorResponseModel.StatusCode = System.Net.HttpStatusCode.NotFound;
                errorResponseModel.Message = "MateriaMedica Not Found";
            }
            
           List<MateriaMedicaRemediesDetailsModel1> lstMatMedicaDetails = new List<MateriaMedicaRemediesDetailsModel1>();

            materiamedicaremediesEntity.ForEach(item =>
            {
                MateriaMedicaRemediesDetailsModel1 modelValues = new MateriaMedicaRemediesDetailsModel1();
                
                modelValues.MateriaMedicaHeadId = item.MateriaMedicaHeadId;
                modelValues.MateriaMedicaHeadName = item.MateriaMedicaHeadName;
                modelValues.MateriaMedicaDetail1 = item.MateriaMedicaDetail1;
                lstMatMedicaDetails.Add(modelValues);
                               

               });
            
            MateriaMedicaRemediesDetailsModel modelInfo=new MateriaMedicaRemediesDetailsModel();
            modelInfo.RemedyId = Convert.ToInt32(remedyId);
            modelInfo.AuthorId = Convert.ToInt32(authorId);
            modelInfo.lstRemedy = lstMatMedicaDetails;

            return modelInfo;


        }
    }
}
