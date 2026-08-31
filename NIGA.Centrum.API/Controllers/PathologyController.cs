using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Model;
using System;

namespace NIGA.Centrum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PathologyController : BaseAPIController
    {
        IPathologyService _PathologyService;
        /// <summary>
        /// Used to initialize controller and inject author service
        /// </summary>
        /// <param name="authorService"></param>
        public PathologyController(IPathologyService pathologyService)
        {
            _PathologyService = pathologyService;
        }

        /// <summary>
        /// To get Author by authorID 
        /// </summary>
        /// <param name="pathologyId"></param>
        /// <returns></returns>
        [HttpGet("GetPathologyById/{pathologyId}")]
        [ProducesResponseType(typeof(PathologyModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetPathologyById(long pathologyId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var rModel = _PathologyService.GetPathologyById(pathologyId, ref errorResponseModel);

                if (rModel != null)
                {
                    return Ok(rModel);
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
        [HttpGet("GetPathology")]
        [ProducesResponseType(typeof(PathologyModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetPathology()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var PathologyModelList = _PathologyService.GetPathology(ref errorResponseModel);

                if (PathologyModelList != null)
                {
                    return Ok(PathologyModelList);
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
        [ProducesResponseType(typeof(PathologyModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult SavePathology(PathologyModel pathologyModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var Model = _PathologyService.SavePathology(pathologyModel, ref errorResponseModel);

                if (Model != null)
                {
                    return Ok(Model);
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
        [Route("DeletePathology")]
        [ProducesResponseType(typeof(PathologyModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeletePathology(PathologyModel pathologyModel )
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var Model = _PathologyService.DeletePathology(pathologyModel, ref errorResponseModel);

                if (Model != null)
                {
                    return Ok(Model);
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
