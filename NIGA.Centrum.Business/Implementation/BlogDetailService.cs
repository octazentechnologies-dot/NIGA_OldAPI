using Microsoft.Extensions.Options;
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
    /// This is implementation for the BlogDetail operations 
    /// </summary>
    public class BlogDetailService : IBlogDetailService
    {

        NIGACentrumContext context;
        private ConfigurationModel _configuration;

        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public BlogDetailService(NIGACentrumContext centrumContext, IOptions<ConfigurationModel> hostName)
        {
            context = centrumContext;
            this._configuration = hostName.Value;

        }

        /// <summary>
        /// Interface is used to deactivate Blogdetail.
        /// </summary>
        /// <param name="blogId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteBlogDetail(long blogId, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var blogEntity = context.BlogDetails.FirstOrDefault(x => x.BlogId == blogId);
            if (blogEntity != null)
            {
                blogEntity.IsActive=false;
                context.SaveChanges();
                Message = "BlogDetail Deleted Successfully";
            }
            return Message;
        }

        /// <summary>
        /// interface for getting all the Blogdetail
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<BlogDetailModel> GetAllBlogDetail(ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var blogList=new List<BlogDetailModel>();
            var blogEntity=context.BlogDetails.Where(x=>x.IsActive==true).ToList();
            if(blogEntity.Count==0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Blog Details not found";
            }
            blogEntity.ForEach(item =>
            {
                blogList.Add(new BlogDetailModel
                {
                    BlogId= item.BlogId,
                    BlogHead= item.BlogHead,
                    BlogSubHead= item.BlogSubHead,
                    //BlogDate= item.BlogDate,
                   BlogDate= item.BlogDate.HasValue ? item.BlogDate.Value.ToString("dd/MM/yyyy") : string.Empty,
                    BlogImage1= item.BlogImage1,
                    BlogImage2=  item.BlogImage2,
                    BlogDetails1 = item.BlogDetails1,
                    IsActive=item.IsActive,
                    EnteredBy=item.EnteredBy,
                    EnteredDate=item.EnteredDate,
                    ChangedBy=item.ChangedBy,
                    ChangedDate=item.ChangedDate,
                });
            });
            return blogList;
        }

        /// <summary>
        /// Method is used for to get Blogdetail by blogId
        /// </summary>
        /// <param name="blogId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public BlogDetailModel1 GetBlogDetailById(long blogId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var blogEntity = context.BlogDetails.Where(x => x.BlogId == blogId).FirstOrDefault();
            if(blogEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Blog Details not found";
            }
            return new BlogDetailModel1
            {
                BlogId = blogEntity.BlogId,
                BlogHead = blogEntity.BlogHead,
                BlogSubHead = blogEntity.BlogSubHead,
                BlogDate = blogEntity.BlogDate,
             //   BlogDate = blogEntity.BlogDate.HasValue ? blogEntity.BlogDate.Value.ToString("dd/MM/yyyy") : string.Empty,
                BlogImage1 = blogEntity.BlogImage1,
                BlogImage2 =  blogEntity.BlogImage2,
                BlogDetails1 = blogEntity.BlogDetails1,
                IsActive = blogEntity.IsActive,
                EnteredBy = blogEntity.EnteredBy,
                EnteredDate = blogEntity.EnteredDate,
                ChangedBy = blogEntity.ChangedBy,
                ChangedDate = blogEntity.ChangedDate,
            };
        }

        /// <summary>
        /// Interface is used to save/update Blogdetail
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string SaveBlogDetail(BlogDetailModel1 model, ref ErrorResponseModel errorResponseModel)
        {
            string message = "";
            if (model.BlogId == 0)
            {
                BlogDetails details = new BlogDetails();
                details.BlogId = model.BlogId;
                details.BlogHead = model.BlogHead;
                details.BlogSubHead = model.BlogSubHead;
                details.BlogDate = model.BlogDate;
                details.BlogImage1 = model.BlogImage1;
                details.BlogImage2 = model.BlogImage2;
                details.BlogDetails1 = model.BlogDetails1;
                details.EnteredBy = model.EnteredBy;
                details.EnteredDate = DateTime.Now;
                details.IsActive = true;
                context.BlogDetails.Add(details);
                context.SaveChanges();
                message = "Blog Details saved Successfully";
            }
            else
            {
                var details = context.BlogDetails.FirstOrDefault(x => x.BlogId == model.BlogId);
                if (details != null)
                {
                    details.BlogId = model.BlogId;
                    details.BlogHead = model.BlogHead;
                    details.BlogSubHead = model.BlogSubHead;
                    details.BlogDate = model.BlogDate;
                    details.BlogImage1 = model.BlogImage1;
                    details.BlogImage2 = model.BlogImage2;
                    details.BlogDetails1 = model.BlogDetails1;
                  //  details.ChangedBy = model.ChangedBy;
                    details.ChangedDate = DateTime.Now;
                    details.IsActive =true;
                    context.SaveChanges();
                    message = "Blog Details Update Successfully";
                }
            }
            return message;
        }
    }
}
