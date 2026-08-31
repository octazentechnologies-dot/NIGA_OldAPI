using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Common;
using NIGA.Centrum.Entity.DataModels;
using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace NIGA.Centrum.Business.Implementation
{
    /// <summary>
    /// This is implementation for the MateriaMedicaDetails operations 
    /// </summary>

    public class MateriaMedicaDeatailsService : IMateriaMedicaDetailService
    {

        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public MateriaMedicaDeatailsService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }

        /// <summary>
        /// Methood to get MateriaMedicaDetails by MateriaMedicaDetailId
        /// </summary>
        /// <param name="materiamedicadetailId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public MateriaMedicaDetailModel GetMateriaMedicaDetailsById(long materiamedicadetailId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel= new ErrorResponseModel();
            var materiamedicadetailEntity = context.MateriaMedicaDetail.FirstOrDefault(x => x.MatriaMedicaDetailId == materiamedicadetailId);
            if(materiamedicadetailEntity != null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "MateriaMedicaDetails not found";
            }
            return new MateriaMedicaDetailModel
            {
                MatriaMedicaDetailId = materiamedicadetailEntity.MatriaMedicaDetailId,
                MateriaMedicaId = materiamedicadetailEntity.MateriaMedicaId,
                MateriaMedicaDetail1 = materiamedicadetailEntity.MateriaMedicaDetail1,
                SeqNo = materiamedicadetailEntity.SeqNo
            };
        }

        /// <summary>
        /// Method to get all the MateriaMedicaHead
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
         public List<MateriaMedicaDetailModel> GetMateriaMedicaDetails(ref ErrorResponseModel errorResponseModel)
          {
            var materiamedicadetailList = new List<MateriaMedicaDetailModel>();
            errorResponseModel=new ErrorResponseModel();
            var materiamedicadetailEntity = context.MateriaMedicaDetail.ToList();
            if (materiamedicadetailEntity.Count == 0)
            {
                errorResponseModel.StatusCode= HttpStatusCode.NotFound;
                errorResponseModel.Message = "MateriMedicaDetails Not Found";
            }
            materiamedicadetailEntity.ForEach(item =>
            {
                materiamedicadetailList.Add(new MateriaMedicaDetailModel
                {
                    MatriaMedicaDetailId=item.MatriaMedicaDetailId,
                    MateriaMedicaId=item.MateriaMedicaId,
                    MateriaMedicaDetail1=item.MateriaMedicaDetail1,
                    SeqNo=item.SeqNo,
                });
            });
            return materiamedicadetailList;
          }

        /// <summary>
        /// Method implementation for saving new MateriaMedicaDetails
        /// </summary>
        /// <param name="materiamedicadetailmodel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string SaveMateriaMedicaDetails(MateriaMedicaDetailModel materiamedicadetailmodel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            if (materiamedicadetailmodel.MatriaMedicaDetailId == 0)
            {
                MateriaMedicaDetail materiamedicadetailEntity = new MateriaMedicaDetail();
               
                materiamedicadetailEntity.MateriaMedicaId = materiamedicadetailmodel.MateriaMedicaId;
                materiamedicadetailEntity.MateriaMedicaDetail1 = materiamedicadetailmodel.MateriaMedicaDetail1;
                materiamedicadetailEntity.SeqNo = materiamedicadetailmodel.SeqNo;
                context.MateriaMedicaDetail.Add(materiamedicadetailEntity);
                context.SaveChanges();
                Message = "MateriMedicaDetails Added Successfully";
            }

            else
            {
                var materiamedicadetailEntity = context.MateriaMedicaDetail.FirstOrDefault(x => x.MatriaMedicaDetailId == materiamedicadetailmodel.MatriaMedicaDetailId);
                if (materiamedicadetailEntity != null)
                {
                  
                    materiamedicadetailEntity.MateriaMedicaId = materiamedicadetailmodel.MateriaMedicaId;
                    materiamedicadetailEntity.MateriaMedicaDetail1 = materiamedicadetailmodel.MateriaMedicaDetail1;
                    materiamedicadetailEntity.SeqNo = materiamedicadetailmodel.SeqNo;
                    context.SaveChanges();
                    Message = "MateriaMedicaDetails Updated Successfully";
                }
            }
            return Message;
        }

        /// <summary>
        /// Method is used for delete MateriaMedicaDetails.
        /// </summary>
        /// <param name="materiamedicadetailmodel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteMateriaMedicaDetails(MateriaMedicaDetailModel materiamedicadetailmodel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var materiamedicadetailEntity = context.MateriaMedicaDetail.FirstOrDefault(x => x.MatriaMedicaDetailId == materiamedicadetailmodel.MatriaMedicaDetailId);
            if (materiamedicadetailEntity != null)
            {
                context.Remove(materiamedicadetailEntity);
                context.SaveChanges();
                Message = "MateriaMedicaDetails Deleted Successfully";
            }
            return Message;
        }

        /// </summary>
        /// <param name="materiamedicaId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<MateriaMedicaDetailModel> GetMateriaMedicaDetail(long materiamedicaId, ref ErrorResponseModel errorResponseModel)
        {
            var materiamedicaModel = new List<MateriaMedicaDetailModel>();
            errorResponseModel = new ErrorResponseModel();
            var materiamedicadetailEntity = (from materiamedicadetail in context.MateriaMedicaDetail
                                       join materiamedicaMaster in context.MateriaMedicaMaster
                                       on materiamedicadetail.MateriaMedicaId equals materiamedicaMaster.MateriaMedicaId
                                       where materiamedicadetail.MateriaMedicaId == materiamedicaId
                                       select new
                                       {
                                           materiamedicadetail.MatriaMedicaDetailId,
                                           materiamedicadetail.MateriaMedicaId,
                                           materiamedicadetail.MateriaMedicaDetail1,
                                           materiamedicadetail.SeqNo,
                                        }).ToList();
            if (materiamedicadetailEntity.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "MateriaMedica not found";
            }
            materiamedicadetailEntity.ForEach(item =>
            {
                materiamedicaModel.Add(new MateriaMedicaDetailModel
                {
                    MatriaMedicaDetailId=Convert.ToInt32(item.MatriaMedicaDetailId),
                    MateriaMedicaId=Convert.ToInt32(item.MateriaMedicaId),
                    MateriaMedicaDetail1=item.MateriaMedicaDetail1,
                    SeqNo = item.SeqNo,
                    
                });

            });

            return materiamedicaModel;
        }
    }
}
