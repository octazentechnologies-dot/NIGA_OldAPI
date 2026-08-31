using Microsoft.EntityFrameworkCore;
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
    /// This is implementation  for the roleMaster operations 
    /// </summary>
    public class RoleMasterService : IRoleMasterService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public RoleMasterService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }

        /// <summary>
        /// Methood to get role by roleid
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public RoleMasterModel GetRoleById(long roleId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var roleMasterEntity = context.RoleMaster.FirstOrDefault(x => x.RoleId == roleId && !x.DeleteStatus);
            if (roleMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Role not found";
            }
            return new RoleMasterModel
            {
                RoleId = roleMasterEntity.RoleId,
                RoleName = roleMasterEntity.RoleName,
                FirmIds = roleMasterEntity.FirmIds,
                EnteredDate = roleMasterEntity.EnteredDate,
                EnteredBy = roleMasterEntity.EnteredBy,
                ChangedBy = roleMasterEntity.ChangedBy,
                ChangedDate = roleMasterEntity.ChangedDate,
                DeleteStatus = roleMasterEntity.DeleteStatus,
            };
        }

        /// <summary>
        /// Method to get all the roleMaster
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<RoleMasterModel> GetRoleMaster(ref ErrorResponseModel errorResponseModel)
        {
            var roleMasterModelList = new List<RoleMasterModel>();
            errorResponseModel = new ErrorResponseModel();
            var roleMasterEntityList = context.RoleMaster.Where(x => x.DeleteStatus == false).ToList();
            if (roleMasterEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Role Master not found";
            }

            roleMasterEntityList.ForEach(item =>
            {
                roleMasterModelList.Add(new RoleMasterModel
                {
                    RoleId = item.RoleId,
                    RoleName = item.RoleName,
                    FirmIds = item.FirmIds,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus,
                });
            });
            return roleMasterModelList;
        }

        /// <summary>
        /// Method implementation for saving new roleMaster
        /// </summary>
        /// <param name="roleMasterModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string SaveRoleMaster(RoleMasterModel roleMasterModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            if (roleMasterModel.RoleId == 0)
            {
                RoleMaster roleMasterEntity = new RoleMaster();

                roleMasterEntity.RoleName = roleMasterModel.RoleName;
                roleMasterEntity.FirmIds = roleMasterModel.FirmIds;
                roleMasterEntity.EnteredBy = roleMasterModel.EnteredBy;
                roleMasterEntity.EnteredDate = DateTime.Now;
                context.RoleMaster.Add(roleMasterEntity);
                context.SaveChanges();
                Message = "RoleMaster Saved Successfully";
            }
            else
            {
                var roleMasterEntity = context.RoleMaster.FirstOrDefault(x => x.RoleId == roleMasterModel.RoleId);
                if (roleMasterEntity != null)
                {
                    roleMasterEntity.RoleName = roleMasterModel.RoleName;
                    roleMasterEntity.FirmIds = roleMasterModel.FirmIds;
                    roleMasterEntity.ChangedBy = roleMasterModel.EnteredBy;
                    roleMasterEntity.ChangedDate = DateTime.Now;
                    context.SaveChanges();
                    Message = "RoleMaster Updated Successfully";
                }
            }
            return Message;
        }

        /// <summary>
        /// Method is used for delete RoleMaster.
        /// </summary>
        /// <param name="roleMasterModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteRoleMaster(RoleMasterModel roleMasterModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var roleMasterEntity = context.RoleMaster.FirstOrDefault(x => x.RoleId == roleMasterModel.RoleId);
            if (roleMasterEntity != null)
            {
                roleMasterEntity.DeleteStatus = roleMasterModel.DeleteStatus;
                roleMasterEntity.ChangedBy = roleMasterModel.EnteredBy;
                roleMasterEntity.ChangedDate = DateTime.Now;
                context.SaveChanges();
                Message = "RoleMaster Deleted Successfully";
            }
            return Message;
        }

    }
}
