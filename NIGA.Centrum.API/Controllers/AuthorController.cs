using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIGA.Centrum.Business.Implementation;
using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Entity.DataModels;
using NIGA.Centrum.Model;
using System;

namespace NIGA.Centrum.API.Controllers
{
    /// <summary>
    /// APIs for author entity 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorController : BaseAPIController
    {
        IAuthorService _authorService;
        /// <summary>
        /// Used to initialize controller and inject author service
        /// </summary>
        /// <param name="authorService"></param>
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        /// <summary>
        /// To get Author by authorID 
        /// </summary>
        /// <param name="authorId"></param>
        /// <returns></returns>
        [HttpGet("GetAuthorById/{authorId}")]
        [ProducesResponseType(typeof(AuthorMasterModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAuthorById(long authorId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var authorModel = _authorService.GetAuthorById(authorId, ref errorResponseModel);

                if (authorModel != null)
                {
                    return Ok(authorModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To Get all Authors
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(AuthorMasterModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAuthor()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var authorModelList = _authorService.GetAuthor(ref errorResponseModel);

                if (authorModelList != null)
                {
                    return Ok(authorModelList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
           
        }



        /// <summary>
        /// To Get all Authors for Repertory
        /// <param name=""></param>
        /// </summary>
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetData")]
        [ProducesResponseType(typeof(AuthorMasterModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAuthorforRepertory()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var authorModelList = _authorService.GetAuthorforRepertory(ref errorResponseModel);

                if (authorModelList != null)
                {
                    return Ok(authorModelList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }


        /// <summary>
        /// To add new Author 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(AuthorMasterModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult SaveAuthor(AuthorMasterModel authorModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var AuthorModel = _authorService.SaveAuthor(authorModel, ref errorResponseModel);

                if (AuthorModel != null)
                {
                    return Ok(AuthorModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To delete author 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteAuthor")]
        [ProducesResponseType(typeof(AuthorMasterModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeleteAuthor(AuthorMasterModel authorModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var AuthorModel = _authorService.DeleteAuthor(authorModel, ref errorResponseModel);

                if (AuthorModel != null)
                {
                    return Ok(AuthorModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
