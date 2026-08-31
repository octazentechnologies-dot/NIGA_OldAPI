using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
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
    public class AuthorService : IAuthorService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public AuthorService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }

        /// <summary>
        /// Methood to get author by authorId
        /// </summary>
        /// <param name="authorId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>

        public AuthorMasterModel GetAuthorById(long authorId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var authorEntity = context.AuthorMaster.Where(x=>x.IsDeleted==false).FirstOrDefault(x => x.AuthorId == authorId);
            if (authorEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Author not found";
            }
            return new AuthorMasterModel
            {
                AuthorId = authorEntity.AuthorId,
                AuthorName = authorEntity.AuthorName,
                Description = authorEntity.Description,
                IsDeleted = authorEntity.IsDeleted,
                IsForRepertory = authorEntity.IsForRepertory,
                AuthorAlias = authorEntity.AuthorAlias,
            };
        }


        /// <summary>
        /// Method for getting all the authors
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<AuthorMasterModel> GetAuthor(ref ErrorResponseModel errorResponseModel)
        {
                errorResponseModel = new ErrorResponseModel();
                var authorModelList = new List<AuthorMasterModel>();
                var authorEntityList = context.AuthorMaster.Where(x => x.IsDeleted == false && x.IsForRepertory==false).ToList();

            if (authorEntityList.Count== 0)
                {
                    errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                    errorResponseModel.Message = "Author not found";
                }
                authorEntityList.ForEach(item =>
                {
                    authorModelList.Add(new AuthorMasterModel
                    {
                        AuthorId = item.AuthorId,
                        AuthorName = item.AuthorName,
                        Description = item.Description,
                        AuthorAlias=item.AuthorAlias,
                        IsForRepertory=item.IsForRepertory,
                       IsDeleted=item.IsDeleted,
                    });
                });
                return authorModelList;
            }

        /// <summary>
        /// Method implementation for saving new Authors
        /// </summary>
        /// <param name="authormasterModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string SaveAuthor(AuthorMasterModel authormasterModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            if (authormasterModel.AuthorId == 0)
            {
                AuthorMaster authorEntity = new AuthorMaster();
                authorEntity.AuthorId = authormasterModel.AuthorId;
                authorEntity.AuthorName = authormasterModel.AuthorName;
                authorEntity.Description = authormasterModel.Description;
                authorEntity.IsDeleted=authormasterModel.IsDeleted;
                authorEntity.IsForRepertory= authormasterModel.IsForRepertory;
                authorEntity.AuthorAlias = authormasterModel.AuthorAlias;
                context.AuthorMaster.Add(authorEntity);
                context.SaveChanges();
                Message = "Author Saved Successfully";
            }
            else
            {
                var authorEntity = context.AuthorMaster.FirstOrDefault(x => x.AuthorId == authormasterModel.AuthorId);
                if (authorEntity != null)
                {

                    
                    authorEntity.AuthorName = authormasterModel.AuthorName;
                    authorEntity.Description = authormasterModel.Description;
                    authorEntity.IsDeleted = authormasterModel.IsDeleted;
                    authorEntity.IsForRepertory = authormasterModel.IsForRepertory;
                    authorEntity.AuthorAlias = authormasterModel.AuthorAlias;
                    context.SaveChanges();
                    Message = "Author Updated Successfully";
                }
            }
            return Message;
        }

        /// <summary>
        /// Method is used for delete Author.
        /// </summary>
        /// <param name="authormasterModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>

        public string DeleteAuthor(AuthorMasterModel authormasterModel, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var authorEntity = context.AuthorMaster.FirstOrDefault(x => x.AuthorId == authormasterModel.AuthorId);
            if (authorEntity != null)
            {
                authorEntity.IsDeleted = true;
               // context.Remove(authorEntity);
                context.SaveChanges();
                Message = "Author Deleted Successfully";
                
            }
            return Message;
        }

        public List<AuthorMasterModel> GetAuthorforRepertory(ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var authorModelList = new List<AuthorMasterModel>();
            var authorEntityList = context.AuthorMaster.Where(x => x.IsDeleted == false && x.IsForRepertory==true).ToList();

            if (authorEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Author not found";
            }
            authorEntityList.ForEach(item =>
            {
                authorModelList.Add(new AuthorMasterModel
                {
                    AuthorId = item.AuthorId,
                    AuthorName = $"{item.AuthorAlias} {item.AuthorName} [{item.Description}]",
                    AuthorAlias = item.AuthorAlias,
                    Description=item.Description,
                   // AuthorNameAliasDescription = $"{item.AuthorAlias} {item.AuthorName} [{item.Description}]"

                });
            });
            return authorModelList;
        }
    }
}
