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
    /// This is implementation  for the menuMaster operations 
    /// </summary>
    public class MenuMasterService : IMenuMasterService
    {
        NIGACentrumContext _centrumContext;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public MenuMasterService(NIGACentrumContext centrumContext)
        {
            _centrumContext = centrumContext;
        }

        /// <summary>
        /// Methood to get menu by MenuId
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public MenuMasterModel GetMenuById(long menuId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var menuMasterEntity = _centrumContext.MenuMaster.Include(x => x.Module).FirstOrDefault(x => x.MenuId == menuId && !x.DeleteStatus);
            if (menuMasterEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "menu Master not found";
            }
            return new MenuMasterModel
            {
                MenuId = menuMasterEntity.MenuId,
                ModuleId = menuMasterEntity.ModuleId,
                ModuleName = menuMasterEntity.Module.ModuleName,
                MenuName = menuMasterEntity.MenuName,
                MenuNameMarathi = menuMasterEntity.MenuNameMarathi,
                MenuType = menuMasterEntity.MenuType,
                ParentMenuId = menuMasterEntity.ParentMenuId,
                MenuUrl = menuMasterEntity.MenuUrl,
                MenuIcon = menuMasterEntity.MenuIcon,
                ActionName = menuMasterEntity.ActionName,
                ControllerName = menuMasterEntity.ControllerName,
                Description = menuMasterEntity.Description,
                IsLeaf = menuMasterEntity.IsLeaf,
                ShowInMainMenu = menuMasterEntity.ShowInMainMenu,
                SeqNo = menuMasterEntity.SeqNo,
                FirmIds = menuMasterEntity.FirmIds,
                EnteredDate = menuMasterEntity.EnteredDate,
                EnteredBy = menuMasterEntity.EnteredBy,
                ChangedBy = menuMasterEntity.ChangedBy,
                ChangedDate = menuMasterEntity.ChangedDate,
                DeleteStatus = menuMasterEntity.DeleteStatus,
            };
        }


        /// <summary>
        /// Method to get all the menuMaster
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<MenuMasterModel> GetMenuMaster(ref ErrorResponseModel errorResponseModel)
        {
            var menuMasterModelList = new List<MenuMasterModel>();
            errorResponseModel = new ErrorResponseModel();
            var menuMasterEntityList = _centrumContext.MenuMaster.Where(x => x.DeleteStatus == false).Include(x => x.Module).ToList();
            if (menuMasterEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Menu Master not found";
            }

            menuMasterEntityList.ForEach(item =>
            {
                menuMasterModelList.Add(new MenuMasterModel
                {
                    MenuId = item.MenuId,
                    ModuleId = item.ModuleId,
                    ModuleName = item.Module.ModuleName,
                    MenuName = item.MenuName,
                    MenuNameMarathi = item.MenuNameMarathi,
                    MenuType = item.MenuType,
                    ParentMenuId = item.ParentMenuId,
                    MenuUrl = item.MenuUrl,
                    MenuIcon = item.MenuIcon,
                    ActionName = item.ActionName,
                    ControllerName = item.ControllerName,
                    Description = item.Description,
                    IsLeaf = item.IsLeaf,
                    ShowInMainMenu = item.ShowInMainMenu,
                    SeqNo = item.SeqNo,
                    FirmIds = item.FirmIds,
                    EnteredDate = item.EnteredDate,
                    EnteredBy = item.EnteredBy,
                    ChangedBy = item.ChangedBy,
                    ChangedDate = item.ChangedDate,
                    DeleteStatus = item.DeleteStatus,
                });
            });
            return menuMasterModelList;
        }

        /// <summary>
        /// Method implementation for saving new menuMaster
        /// </summary>
        /// <param name="menuMasterModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string SaveMenuMaster(MenuMasterModel menuMasterModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            if (menuMasterModel.MenuId == 0)
            {
                MenuMaster menuMasterEntity = new MenuMaster();
                menuMasterEntity.ModuleId = menuMasterModel.ModuleId;
                menuMasterEntity.MenuName = menuMasterModel.MenuName;
                menuMasterEntity.MenuNameMarathi = menuMasterModel.MenuNameMarathi;
                menuMasterEntity.MenuType = menuMasterModel.MenuType;
                menuMasterEntity.ParentMenuId = menuMasterModel.ParentMenuId;
                menuMasterEntity.MenuUrl = menuMasterModel.MenuUrl;
                menuMasterEntity.MenuIcon = menuMasterModel.MenuIcon;
                menuMasterEntity.ActionName = menuMasterModel.ActionName;
                menuMasterEntity.ControllerName = menuMasterModel.ControllerName;
                menuMasterEntity.Description = menuMasterModel.Description;
                menuMasterEntity.IsLeaf = menuMasterModel.IsLeaf;
                menuMasterEntity.ShowInMainMenu = menuMasterModel.ShowInMainMenu;
                menuMasterEntity.SeqNo = menuMasterModel.SeqNo;
                menuMasterEntity.FirmIds = menuMasterModel.FirmIds;
                menuMasterEntity.EnteredBy = menuMasterModel.EnteredBy;
                menuMasterEntity.EnteredDate = DateTime.Now;
                _centrumContext.MenuMaster.Add(menuMasterEntity);
                _centrumContext.SaveChanges();
                Message = "MenuMaster Saved Successfully";
            }
            else
            {
                var menuMasterEntity = _centrumContext.MenuMaster.FirstOrDefault(x => x.MenuId == menuMasterModel.MenuId);
                if (menuMasterEntity != null)
                {
                    menuMasterEntity.ModuleId = menuMasterModel.ModuleId;
                    menuMasterEntity.MenuName = menuMasterModel.MenuName;
                    menuMasterEntity.MenuNameMarathi = menuMasterModel.MenuNameMarathi;
                    menuMasterEntity.MenuType = menuMasterModel.MenuType;
                    menuMasterEntity.ParentMenuId = menuMasterModel.ParentMenuId;
                    menuMasterEntity.MenuUrl = menuMasterModel.MenuUrl;
                    menuMasterEntity.MenuIcon = menuMasterModel.MenuIcon;
                    menuMasterEntity.ActionName = menuMasterModel.ActionName;
                    menuMasterEntity.ControllerName = menuMasterModel.ControllerName;
                    menuMasterEntity.Description = menuMasterModel.Description;
                    menuMasterEntity.IsLeaf = menuMasterModel.IsLeaf;
                    menuMasterEntity.ShowInMainMenu = menuMasterModel.ShowInMainMenu;
                    menuMasterEntity.SeqNo = menuMasterModel.SeqNo;
                    menuMasterEntity.FirmIds = menuMasterModel.FirmIds;
                    menuMasterEntity.ChangedBy = menuMasterModel.EnteredBy;
                    menuMasterEntity.ChangedDate = DateTime.Now;
                    _centrumContext.SaveChanges();
                    Message = "MenuMaster Updated Successfully";
                }
            }
            return Message;
        }

        /// <summary>
        /// Method is used for delete MenuMaster.
        /// </summary>
        /// <param name="menuMasterModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteMenuMaster(MenuMasterModel menuMasterModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var menuMasterEntity = _centrumContext.MenuMaster.FirstOrDefault(x => x.MenuId == menuMasterModel.MenuId);
            if (menuMasterEntity != null)
            {
                menuMasterEntity.DeleteStatus = menuMasterModel.DeleteStatus;
                menuMasterEntity.ChangedBy = menuMasterModel.EnteredBy;
                menuMasterEntity.ChangedDate = DateTime.Now;
                _centrumContext.SaveChanges();
                Message = "MenuMaster Deleted Successfully";
            }
            return Message;
        }

        /// <summary>
        /// Method to get menu by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<MenuMasterResModel> GetMenuByUserId(long userId, int? firmIds, ref ErrorResponseModel errorResponseModel)
        {
            var menuMasterModelList = new List<MenuMasterResModel>();
            errorResponseModel = new ErrorResponseModel();
            if (firmIds != null)
            {
                var userMenuEntity = _centrumContext.UserDetails.Where(x => x.UserId == userId && x.FirmId == firmIds && x.IsDelete == false).ToList();
                foreach (var menu in userMenuEntity)
                {
                    var menuEntity = _centrumContext.MenuMaster.Where(x => x.MenuId == menu.MenuId).FirstOrDefault();
                    if (menuEntity == null)
                    {
                        return null;
                    }
                    var model = new MenuMasterResModel();
                    model.MenuId = menuEntity.MenuId;
                    model.ModuleId = menuEntity.ModuleId;
                    model.MenuName = menuEntity.MenuName;
                    model.MenuNameMarathi = menuEntity.MenuNameMarathi;
                    model.MenuType = menuEntity.MenuType;
                    model.ParentMenuId = menuEntity.ParentMenuId;
                    model.MenuUrl = menuEntity.MenuUrl;
                    model.MenuIcon = menuEntity.MenuIcon;
                    model.ActionName = menuEntity.ActionName;
                    model.ControllerName = menuEntity.ControllerName;
                    model.Description = menuEntity.Description;
                    model.IsLeaf = menuEntity.IsLeaf;
                    model.ShowInMainMenu = menuEntity.ShowInMainMenu;
                    model.SeqNo = menuEntity.SeqNo;
                    model.FirmIds = menuEntity.FirmIds;
                    model.IsView = menu.IsView;
                    model.IsAdd = menu.IsAdd;
                    model.IsModify = menu.IsModify;
                    menuMasterModelList.Add(model);
                }
            }
            else
            {
                var userMenuEntity = _centrumContext.UserDetails.Where(x => x.UserId == userId && x.IsDelete == false).ToList();
                foreach (var menu in userMenuEntity)
                {
                    var menuEntity = _centrumContext.MenuMaster.Where(x => x.MenuId == menu.MenuId).FirstOrDefault();
                    if (menuEntity == null)
                    {
                        return null;
                    }
                    var model = new MenuMasterResModel();
                    model.MenuId = menuEntity.MenuId;
                    model.ModuleId = menuEntity.ModuleId;
                    model.MenuName = menuEntity.MenuName;
                    model.MenuNameMarathi = menuEntity.MenuNameMarathi;
                    model.MenuType = menuEntity.MenuType;
                    model.ParentMenuId = menuEntity.ParentMenuId;
                    model.MenuUrl = menuEntity.MenuUrl;
                    model.MenuIcon = menuEntity.MenuIcon;
                    model.ActionName = menuEntity.ActionName;
                    model.ControllerName = menuEntity.ControllerName;
                    model.Description = menuEntity.Description;
                    model.IsLeaf = menuEntity.IsLeaf;
                    model.ShowInMainMenu = menuEntity.ShowInMainMenu;
                    model.SeqNo = menuEntity.SeqNo;
                    model.FirmIds = menuEntity.FirmIds;
                    model.IsView = menu.IsView;
                    model.IsAdd = menu.IsAdd;
                    model.IsModify = menu.IsModify;
                    menuMasterModelList.Add(model);
                }
            }
            return menuMasterModelList;
        }
    }
}
