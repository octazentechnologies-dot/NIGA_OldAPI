using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIGA.Centrum.Business.Implementation;
using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Common;
using NIGA.Centrum.Model;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using System.Security.Policy;

namespace NIGA.Centrum.API.Controllers
{
    /// <summary>
    /// APIs for news entity 
    /// </summary>
    
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class NewsDetailController : BaseAPIController
    {
        INewsDetailService newsDetailService;

      //  private readonly IWebHostEnvironment iwebhostingEnvironment;

        /// <summary>
        /// Used to initialize controller and inject news details service
        /// </summary>
        /// <param name="_newsDetailService"></param>
        public NewsDetailController(INewsDetailService _newsDetailService) //IWebHostEnvironment _webHostEnvironment)
        {
            newsDetailService = _newsDetailService;
           // iwebhostingEnvironment = _webHostEnvironment;
        }



        /// <summary>
        /// To get newsdetails by newsId 
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        [HttpGet("GetNewsDetailsbyId/{newsId}")]
        [ProducesResponseType(typeof(NewDetailModel1), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetNewsDetailsbyId(long newsId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var newsdetailModel = newsDetailService.GetNewsDetailsbyId(newsId, ref errorResponseModel);

                if (newsdetailModel != null)
                {
                    return Ok(newsdetailModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        /// <summary>
        /// To Get all NewsDetails
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllNewsDetails")]
        [ProducesResponseType(typeof(NewDetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAllNewsDetails()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var newsModelList = newsDetailService.GetAllNewsDetails(ref errorResponseModel);

                if (newsModelList != null)
                {
                    return Ok(newsModelList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To add new newsdetails 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost("SaveNewsDetails")]
        [ProducesResponseType(typeof(NewDetailModel1), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult SaveNewsDetails( NewDetailModel1 model)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var newsModel = newsDetailService.SaveNewsDetails(model, ref errorResponseModel);

                if (newsModel != null)
                {
                    return Ok(newsModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// To delete newsdetails 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteNewsDetails")]
        [ProducesResponseType(typeof(NewDetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeleteNewsDetails(int newsId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var newsModel = newsDetailService.DeleteNewsDetails(newsId, ref errorResponseModel);



                if (newsModel != null)
                {
                    return Ok(newsModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        /// <summary>
        /// To get newsdetails by newscategoryId 
        /// </summary>
        /// <param name="newscategoryId"></param>
        /// <returns></returns>
        [HttpGet("GetNewsDetailsbyCategoryId/{newscategoryId}")]
        [ProducesResponseType(typeof(NewDetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetNewsDetailsbyCategoryId(long newscategoryId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var newsdetailModel = newsDetailService.GetNewsDetailsbyCategoryId(newscategoryId, ref errorResponseModel);

                if (newsdetailModel != null)
                {
                    return Ok(newsdetailModel);
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
