using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    /// <summary>
    /// Interface used for Author related operations
    /// </summary>
    public interface IAuthorService
    {
        /// <summary>
        /// Method is used for to get Author by AuthorId
        /// </summary>
        /// <param name="authorId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        AuthorMasterModel GetAuthorById(long authorId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// interface for getting all the Author
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<AuthorMasterModel> GetAuthor(ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to save Author
        /// </summary>
        /// <param name="authormasterModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string SaveAuthor(AuthorMasterModel authormasterModel, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// Interface is used to deactivate Author.
        /// </summary>
        /// <param name="authormasterModel"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        string DeleteAuthor(AuthorMasterModel authormasterModel, ref ErrorResponseModel errorResponseModel);



        /// <summary>
        /// interface for getting all the Author for Repertory
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<AuthorMasterModel> GetAuthorforRepertory(ref ErrorResponseModel errorResponseModel);
    }
}
