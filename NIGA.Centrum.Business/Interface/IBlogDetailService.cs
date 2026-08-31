using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    /// <summary>
    /// Interface used for Blogdetail related operations
    /// </summary>
    public interface IBlogDetailService
    {
        /// <summary>
        /// Method is used for to get Blogdetail by blogId
        /// </summary>
        /// <param name="blogId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        BlogDetailModel1 GetBlogDetailById(long blogId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// interface for getting all the Blogdetail
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<BlogDetailModel> GetAllBlogDetail(ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to save/update Blogdetail
        /// </summary>
        /// <param name="model"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SaveBlogDetail(BlogDetailModel1 model, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to deactivate Blogdetail.
        /// </summary>
        /// <param name="blogId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteBlogDetail(long blogId, ref ErrorResponseModel errorResponseModel);

    }
}
