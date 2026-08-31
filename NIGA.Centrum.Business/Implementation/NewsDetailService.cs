using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Entity.DataModels;
using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace NIGA.Centrum.Business.Implementation
{
    public class NewsDetailService : INewsDetailService
    {
        NIGACentrumContext context;
        private ConfigurationModel _configuration;

        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public NewsDetailService(NIGACentrumContext centrumContext, IOptions<ConfigurationModel> hostName)
        {
            context = centrumContext;
            this._configuration = hostName.Value;

        }

        /// <summary>
        /// interface for getting all the newsdetails
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<NewDetailModel> GetAllNewsDetails(ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var newsModelList = new List<NewDetailModel>();
            var newsEntityList =(from news in context.NewsDetails join category in context.NewsCategory
                                 on news.NewsCategoryId equals category.NewsCategoryId
                                 where news.IsActive==true
                                 select new
                                 {
                                     news.NewsId,
                                     news.NewsCategoryId,
                                     news.NewsDate,
                                     news.NewsHeading,
                                     news.NewsSubHeading,
                                     news.NewsImage1,
                                     news.NewsImage2,
                                     news.NewsImage3,
                                     news.NewsImage4,
                                     news.EnteredBy,
                                     news.EnteredDate,
                                     category.NewsCategory1,
                                     news.NewsContent,
                                 }
                                 ).ToList();
                
               

            if (newsEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "News Details not found";
            }


            newsEntityList.ForEach(item =>
            {
              
                newsModelList.Add(new NewDetailModel
                {
                    NewsId = item.NewsId,
                    NewsDate = item.NewsDate.HasValue ? item.NewsDate.Value.ToString("dd/MM/yyyy") : string.Empty,
                    // NewsDate = item.NewsDate,
                    NewsHeading = item.NewsHeading,
                    NewsSubHeading = item.NewsSubHeading,
                    NewsCategoryId = item.NewsCategoryId,
                    NewsContent= item.NewsContent,
                    NewsImage1 =  item.NewsImage1,
                    NewsImage2 = item.NewsImage2,   
                    NewsImage3 =  item.NewsImage3,
                    NewsImage4 =  item.NewsImage4,
                    EnteredBy = item.EnteredBy,
                    EnteredDate = item.EnteredDate,
                    NewsCategory1 = item.NewsCategory1,
                });
            });
            return newsModelList;
        }

        /// <summary>
        /// Method is used for to get newsdetails by newsId
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
       public NewDetailModel1 GetNewsDetailsbyId(long newsId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var newsEntity = (from news in context.NewsDetails
                              join category in context.NewsCategory
                              on news.NewsCategoryId equals category.NewsCategoryId
                              where news.NewsId == newsId
                              select new
                              {
                                  news.NewsId,
                                  news.NewsCategoryId,
                                  news.NewsDate,
                                  news.NewsHeading,
                                  news.NewsSubHeading,
                                  news.NewsImage1,
                                  news.NewsImage2,
                                  news.NewsImage3,
                                  news.NewsImage4,
                                  news.EnteredBy,
                                  news.EnteredDate,
                                  category.NewsCategory1,
                                  news.NewsContent,
                                  news.IsActive
                              }).FirstOrDefault();

            if (newsEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "News Details not found";
            }
            return new NewDetailModel1
            {
                NewsId = newsEntity.NewsId,
                //NewsDate = newsEntity.NewsDate.HasValue ? newsEntity.NewsDate.Value.ToString("dd/MM/yyyy") : string.Empty,
                 NewsDate = newsEntity.NewsDate,
                NewsHeading = newsEntity.NewsHeading,
                NewsSubHeading = newsEntity.NewsSubHeading,
                NewsCategoryId = newsEntity.NewsCategoryId,
                NewsContent = newsEntity.NewsContent,
                NewsImage1= newsEntity.NewsImage1,
                NewsImage2= newsEntity.NewsImage2,
                NewsImage3=  newsEntity.NewsImage3,
                NewsImage4= newsEntity.NewsImage4,
                EnteredBy=newsEntity.EnteredBy,
                EnteredDate =newsEntity.EnteredDate,
                NewsCategory1 = newsEntity.NewsCategory1,
                IsActive = newsEntity.IsActive,
            };
        }


        /// <summary>
        /// Interface is used to save newsdetails
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string SaveNewsDetails(NewDetailModel1 model, ref ErrorResponseModel errorResponseModel)
        {
            
            string message = "";
            if(model.NewsId == 0)
            {
                NewsDetails details= new NewsDetails();
                details.NewsId=model.NewsId;
                details.NewsDate=model.NewsDate;
                details.NewsHeading=model.NewsHeading;
                details.NewsSubHeading=model.NewsSubHeading;
                details.NewsContent=model.NewsContent;
                details.NewsImage1 = model.NewsImage1;
                details.NewsImage2 = model.NewsImage2;
                details.NewsImage3 = model.NewsImage3;
                details.NewsImage4 = model.NewsImage4;
                details.NewsCategoryId = model.NewsCategoryId;
                //details.NewsImage1 = UploadnewsImage(model.images.NewsImage1);
                //details.NewsImage2 = UploadnewsImage(model.images.NewsImage2);
                //details.NewsImage3 = UploadnewsImage(model.images.NewsImage3);
                //details.NewsImage4 = UploadnewsImage(model.images.NewsImage4);
                details.EnteredBy=model.EnteredBy;
                details.EnteredDate=DateTime.Now;
                details.IsActive = model.IsActive;
                context.NewsDetails.Add(details);
                context.SaveChanges();
                message = "News Details saved Successfully";
            }


            else
            {
                var details = context.NewsDetails.FirstOrDefault(x => x.NewsId == model.NewsId);
                if (details != null)
                {
                    details.NewsId = model.NewsId;
                    details.NewsDate = model.NewsDate;
                    details.NewsHeading = model.NewsHeading;
                    details.NewsSubHeading = model.NewsSubHeading;
                    details.NewsCategoryId=model.NewsCategoryId;
                    details.NewsContent = model.NewsContent;
                    details.NewsImage1 = model.NewsImage1;
                    details.NewsImage2 = model.NewsImage2;
                    details.NewsImage3 = model.NewsImage3;
                    details.NewsImage4 = model.NewsImage4;
                    details.EnteredBy = model.EnteredBy;
                    details.EnteredDate = DateTime.Now;
                    details.IsActive = model.IsActive;
                    context.SaveChanges();
                    message = "News Details Update Successfully";
                }
            }
            return message;
        }

        public string UploadnewsImage(IFormFile file)

        {
            string filePathtoSave = "";
          //var existingUser = context.NewsDetails.Where(x => x.NewsId == newsId).FirstOrDefault();
            var serverPath = Directory.GetCurrentDirectory() + "/Resources/NewsImages/";
            string extn = System.IO.Path.GetExtension(file.FileName);
            var filePath = Path.Combine(serverPath + file.FileName);
            new FileInfo(filePath).Directory?.Create();

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyToAsync(stream);
            }
            filePathtoSave = "/NewsImages/" + file.FileName;
            return filePathtoSave;

        }


        /// <summary>
        /// service is used to deactivate newsdetails.
        /// </summary>
        /// <param name="newsId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public string DeleteNewsDetails(int newsId, ref ErrorResponseModel errorResponseModel)
        {
            string Message = "";
            var newsEntity = context.NewsDetails.FirstOrDefault(x => x.NewsId == newsId);
            if (newsEntity != null)
            {
                newsEntity.IsActive =false;
                context.SaveChanges();
                Message = " News Details Delete Successfully";
            }
            return Message;
        }



                 /// <summary>
                /// service is used to get newsdetails by newscategoryId.
                /// </summary>
                /// <param name="newsCategoryId"></param>
                /// <param name="errorResponseModel"></param>
                /// <returns></returns>
        public List<NewDetailModel> GetNewsDetailsbyCategoryId(long newsCategoryId, ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var newsModelList = new List<NewDetailModel>();
            var newsEntityList = (from news in context.NewsDetails
                                  join category in context.NewsCategory
                                 on news.NewsCategoryId equals category.NewsCategoryId
                                  where news.NewsCategoryId == newsCategoryId && news.IsActive==true
                                  select new
                                  {
                                      news.NewsId,
                                      news.NewsCategoryId,
                                      news.NewsDate,
                                      news.NewsHeading,
                                      news.NewsSubHeading,
                                      news.NewsImage1,
                                      news.NewsImage2,
                                      news.NewsImage3,
                                      news.NewsImage4,
                                      news.EnteredBy,
                                      news.EnteredDate,
                                      category.NewsCategory1,
                                      news.NewsContent,
                                      news.IsActive
                                  }
                                 ).ToList();



            if (newsEntityList.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "News Details not found";
                return newsModelList;
            }


            newsEntityList.ForEach(item =>
            {

                newsModelList.Add(new NewDetailModel
                {
                    NewsId = item.NewsId,
                    NewsDate = item.NewsDate.HasValue ? item.NewsDate.Value.ToString("dd/MM/yyyy") : string.Empty,
                    // NewsDate = item.NewsDate,
                    NewsHeading = item.NewsHeading,
                    NewsSubHeading = item.NewsSubHeading,
                    NewsCategoryId = item.NewsCategoryId,
                    NewsContent = item.NewsContent,
                    NewsImage1 = item.NewsImage1,
                    NewsImage2 = item.NewsImage2,
                    NewsImage3 = item.NewsImage3,
                    NewsImage4 = item.NewsImage4,
                    EnteredBy = item.EnteredBy,
                    EnteredDate = item.EnteredDate,
                    NewsCategory1 = item.NewsCategory1,
                    IsActive = item.IsActive,
                });
            });
            return newsModelList;
        }
    }
}
