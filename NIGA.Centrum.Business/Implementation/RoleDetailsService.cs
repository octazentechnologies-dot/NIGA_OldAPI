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
    /// This is implementation  for the roleDetails operations 
    /// </summary>
    public class RoleDetailsService : IRoleDetailsService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public RoleDetailsService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }

        /// <summary>
        /// Methood to get role details by RecordId
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public RoleDetailsModel GetRoleDetailsById(long recordId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var roleDetailsEntity = context.RoleDetails.Include(x => x.Role).Include(x => x.Menu).FirstOrDefault(x => x.RecordId == recordId && x.IsDelete==false);
            if (roleDetailsEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Role Details not found";
            }
            return new RoleDetailsModel
            {
                RecordId = roleDetailsEntity.RecordId,
                RoleId = roleDetailsEntity.RoleId,
                RoleName = roleDetailsEntity.Role.RoleName,
                MenuId = roleDetailsEntity.MenuId,
                MenuName = roleDetailsEntity.Menu.MenuName,
                IsView = roleDetailsEntity.IsView,
                IsAdd = roleDetailsEntity.IsAdd,
                IsModify = roleDetailsEntity.IsModify,
                IsDelete = roleDetailsEntity.IsDelete,
            };
        }

        /// <summary>
        /// Method to get all the roleDetails
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<RoleDetailsModel> GetRoleDetails(ref ErrorResponseModel errorResponseModel)
        {
            var roleDetailsModelList = new List<RoleDetailsModel>();
            errorResponseModel = new ErrorResponseModel();
            var roleDetailsEntityList = context.RoleDetails.Where(x => x.IsDelete == false).Include(x => x.Role).Include(x => x.Menu).ToList();
            if (roleDetailsEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Role Details not found";
            }

            roleDetailsEntityList.ForEach(item =>
            {
                roleDetailsModelList.Add(new RoleDetailsModel
                {
                    RecordId = item.RecordId,
                    RoleId = item.RoleId,
                    RoleName = item.Role.RoleName,
                    MenuId = item.MenuId,
                    MenuName = item.Menu.MenuName,
                    IsView = item.IsView,
                    IsAdd = item.IsAdd,
                    IsModify = item.IsModify,
                    IsDelete = item.IsDelete,
                });
            });
            return roleDetailsModelList;
        }

        /// <summary>
        /// Method implementation for saving new roleDetails
        /// </summary>
        /// <param name="roleDetailsModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string SaveRoleDetails(RoleDetailsModel roleDetailsModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            if (roleDetailsModel.RecordId == 0)
            {
                RoleDetails roleDetailsEntity = new RoleDetails();

                roleDetailsEntity.RoleId = roleDetailsModel.RoleId;
                roleDetailsEntity.MenuId = roleDetailsModel.MenuId;
                roleDetailsEntity.IsView = roleDetailsModel.IsView;
                roleDetailsEntity.IsAdd = roleDetailsModel.IsAdd;
                roleDetailsEntity.IsModify = roleDetailsModel.IsModify;
                roleDetailsEntity.IsDelete = roleDetailsModel.IsDelete;
                context.RoleDetails.Add(roleDetailsEntity);
                context.SaveChanges();
                Message = "RoleDetails Saved Successfully";
            }
            else
            {
                var roleDetailsEntity = context.RoleDetails.FirstOrDefault(x => x.RecordId == roleDetailsModel.RecordId);
                if (roleDetailsEntity != null)
                {
                    roleDetailsEntity.RoleId = roleDetailsModel.RoleId;
                    roleDetailsEntity.MenuId = roleDetailsModel.MenuId;
                    roleDetailsEntity.IsView = roleDetailsModel.IsView;
                    roleDetailsEntity.IsAdd = roleDetailsModel.IsAdd;
                    roleDetailsEntity.IsModify = roleDetailsModel.IsModify;
                    roleDetailsEntity.IsDelete = roleDetailsModel.IsDelete;
                    context.SaveChanges();
                    Message = "RoleDetails Updated Successfully";
                }
            }
            return Message;
        }

        /// <summary>
        /// Method is used for delete RoleDetails.
        /// </summary>
        /// <param name="roleDetailsModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteRoleDetails(RoleDetailsModel roleDetailsModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var roleDetailsEntity = context.RoleDetails.FirstOrDefault(x => x.RecordId == roleDetailsModel.RecordId);
            if (roleDetailsEntity != null)
            {
                roleDetailsEntity.IsDelete = roleDetailsModel.IsDelete;
                context.SaveChanges();
                Message = "RoleDetails Deleted Successfully";
            }
            return Message;
        }


    }
}
