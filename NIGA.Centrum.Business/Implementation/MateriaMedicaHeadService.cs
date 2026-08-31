using NIGA.Centrum.Business.Interface;
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
    /// This is implementation for the MateriaMedicaHead operations 
    /// </summary>
    

    public class MateriaMedicaHeadService : IMateriaMedicaHeadMasterService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public MateriaMedicaHeadService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }

        /// <summary>
        /// Methood to get MateriaMedicaHead by MateriaMedicaHeadId
        /// </summary>
        /// <param name="remedyId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public MateriaMedicaHeadMasterModel GetMateriaMedicaHeadById(long materiamedicaheadId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var materiamedicaheadEntity = context.MateriaMedicaHeadMaster.FirstOrDefault(x => x.MateriaMedicaHeadId == materiamedicaheadId && x.IsDeleted==false);
            if (materiamedicaheadEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "MateriaMedicaHead not found";
            }
            return new MateriaMedicaHeadMasterModel
            {
                MateriaMedicaHeadId = materiamedicaheadEntity.MateriaMedicaHeadId,
                MateriaMedicaHeadName = materiamedicaheadEntity.MateriaMedicaHeadName,
                IsSection = materiamedicaheadEntity.IsSection,
                Description = materiamedicaheadEntity.Description,
                SeqNo = materiamedicaheadEntity.SeqNo,
                AuthorId = materiamedicaheadEntity.AuthorId,
                IsDeleted=materiamedicaheadEntity.IsDeleted,
                DifferentialMM=materiamedicaheadEntity.DifferentialMm
               
            };
        }
        /// <summary>
        /// Method to get all the MateriaMedicaHead
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>

        public List<MateriaMedicaHeadMasterModel1> GetMateriaMedicaHead(ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var materiaMedicaheadEntityList = (from materiaMedicaHead in context.MateriaMedicaHeadMaster
                                               join auth in context.AuthorMaster on materiaMedicaHead.AuthorId equals auth.AuthorId
                                               where materiaMedicaHead.IsDeleted==false
                                               select new MateriaMedicaHeadMasterModel1
                                               {
                                                   MateriaMedicaHeadId = materiaMedicaHead.MateriaMedicaHeadId,
                                                   AuthorId = materiaMedicaHead.AuthorId,
                                                   MateriaMedicaHeadName = materiaMedicaHead.MateriaMedicaHeadName,
                                                   Description = materiaMedicaHead.Description,
                                                   IsSection = materiaMedicaHead.IsSection,
                                                   SeqNo = materiaMedicaHead.SeqNo,
                                                   AuthorName = auth.AuthorName,
                                                   DifferentialMM= materiaMedicaHead.DifferentialMm
                                               }).ToList();
            if (materiaMedicaheadEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "MateriaMedicaHead not found";
            }
            
            return materiaMedicaheadEntityList;
        }

        /// <summary>
        /// Method implementation for saving new MateriaMedicaHead
        /// </summary>
        /// <param name="materiamedicaheadModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>

        public string SaveMateriaMedicaHead(MateriaMedicaHeadMasterModel materiamedicaheadModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            if (materiamedicaheadModel.MateriaMedicaHeadId == 0)
            {
                MateriaMedicaHeadMaster materiamedicaheadEntity = new MateriaMedicaHeadMaster();
                materiamedicaheadEntity.AuthorId = materiamedicaheadModel.AuthorId;
                materiamedicaheadEntity.MateriaMedicaHeadName = materiamedicaheadModel.MateriaMedicaHeadName;
                materiamedicaheadEntity.Description = materiamedicaheadModel.Description;
                materiamedicaheadEntity.IsSection = materiamedicaheadModel.IsSection;
                materiamedicaheadEntity.SeqNo = materiamedicaheadModel.SeqNo;
                materiamedicaheadEntity.IsDeleted = materiamedicaheadModel.IsDeleted;
                materiamedicaheadEntity.DifferentialMm = false;
                context.MateriaMedicaHeadMaster.Add(materiamedicaheadEntity);
                context.SaveChanges();
                Message = "Materia Medica Head Saved Successfully";
            }
            else
            {
                var materiamedicaheadEntity = context.MateriaMedicaHeadMaster.FirstOrDefault(x => x.MateriaMedicaHeadId == materiamedicaheadModel.MateriaMedicaHeadId);
                if (materiamedicaheadEntity != null)
                {

                    materiamedicaheadEntity.AuthorId = materiamedicaheadModel.AuthorId;
                    materiamedicaheadEntity.MateriaMedicaHeadName = materiamedicaheadModel.MateriaMedicaHeadName;
                    materiamedicaheadEntity.Description = materiamedicaheadModel.Description;
                    materiamedicaheadEntity.IsSection = materiamedicaheadModel.IsSection;
                    materiamedicaheadEntity.SeqNo = materiamedicaheadModel.SeqNo;
                    materiamedicaheadEntity.IsDeleted = materiamedicaheadModel.IsDeleted;
                    materiamedicaheadEntity.DifferentialMm = materiamedicaheadModel.DifferentialMM;
                    context.SaveChanges();
                    Message = "Materia Medica Head Updated Successfully";
                }
            }
            return Message;
        }

        /// <summary>
        /// Method is used for delete MateriaMedicaHead.
        /// </summary>
        /// <param name="materiamedicaheadModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteMateriaMedicaHead(MateriaMedicaHeadMasterModel materiamedicaheadModel, ref ErrorResponseModel errorResponseModel)
        {
            //string Message = "";
            //var materiamedicadetailEntity = context.MateriaMedicaDetail.FirstOrDefault(x => x.MateriaMedicaId == materiamedicamodel.MateriaMedicaId);


            //if (materiamedicadetailEntity != null)
            //{
            //    context.Remove(materiamedicadetailEntity);
            //    context.SaveChanges();
            //    // Message = "MateriaMedica Deleted Successfully";
            //}



            string Message = "";
            //var materiamedicamasterEntity = context.MateriaMedicaMaster.FirstOrDefault(x => x.MateriaMedicaHeadId == materiamedicaheadModel.MateriaMedicaHeadId);
            //if (materiamedicamasterEntity != null)
            //{
            //    context.Remove(materiamedicamasterEntity);
            //    context.SaveChanges();
            //   // Message = "MateriaMedicaHead Deleted Successfully";
            //}


            string Message2 = "";
            var materiamedicaheadEntity = context.MateriaMedicaHeadMaster.FirstOrDefault(x => x.MateriaMedicaHeadId == materiamedicaheadModel.MateriaMedicaHeadId);
            if (materiamedicaheadEntity != null)
            {
                materiamedicaheadEntity.IsDeleted = true;
                //context.Remove(materiamedicaheadEntity);
                context.SaveChanges();
                Message = "Materia Medica Head Deleted Successfully";
            }
            return Message;
        }
        public List<MateriaMedicaHeadMasterModel> GetMateriaMedicaHeadByAuthorId(long authorId, ref ErrorResponseModel errorResponseModel)
       {
            var materiamedicaheadModel = new List<MateriaMedicaHeadMasterModel>();
            errorResponseModel = new ErrorResponseModel();
            var materiamedicaheadEntity = (from materiamedicahead in context.MateriaMedicaHeadMaster
                                  join authorMaster in context.AuthorMaster
                                  on materiamedicahead.AuthorId equals authorMaster.AuthorId
                                  where materiamedicahead.AuthorId == authorId && materiamedicahead.IsDeleted== false
                                  select new
                                  {
                                      materiamedicahead.MateriaMedicaHeadId,
                                      materiamedicahead.MateriaMedicaHeadName,
                                      materiamedicahead.Description,
                                      materiamedicahead.IsSection,
                                      materiamedicahead.SeqNo,
                                      

                                     }).ToList();
            if (materiamedicaheadEntity.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "MateriaMedicaHead not found";
            }


            materiamedicaheadEntity.ForEach(item =>
            {
                materiamedicaheadModel.Add(new MateriaMedicaHeadMasterModel
                {
                    MateriaMedicaHeadId = Convert.ToInt32(item.MateriaMedicaHeadId),
                    MateriaMedicaHeadName = item.MateriaMedicaHeadName,
                    Description = item.Description,
                    IsSection =item.IsSection,
                    SeqNo =item.SeqNo,
                   
                });

            });

            return materiamedicaheadModel;
       }

        public string UpdateDifferentialMateriaMedicadDefaultStatus(int materiaMedicaHeadId, bool differentialMMDefaultStatus)
        {
            string Message = "Fail to update differential materia Medica default status";
            var materiamedicamasterEntity = context.MateriaMedicaHeadMaster.FirstOrDefault(x => x.MateriaMedicaHeadId == materiaMedicaHeadId);
            if (materiamedicamasterEntity != null)
            {
                materiamedicamasterEntity.DifferentialMm = differentialMMDefaultStatus;
                context.SaveChanges();
                Message = "Update successfully differential materia Medica default status";
            }

            return Message;
        }
    }
}

