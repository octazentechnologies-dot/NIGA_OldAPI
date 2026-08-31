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
    public class DrugSystemController : BaseAPIController
    {
        IDrugSystemService _drugSystemService;
        /// <summary>
        /// Used to initialize controller and inject author service
        /// </summary>
        /// <param name="authorService"></param>
        public DrugSystemController(IDrugSystemService drugSystemService)
        {
            _drugSystemService = drugSystemService;
        }

        /// <summary>
        /// To get DrugSystem by drugSystemID 
        /// </summary>
        /// <param name="drugSystemId"></param>
        /// <returns></returns>
        [HttpGet("{drugSystemId}")]
        [ProducesResponseType(typeof(DrugSystemModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetDrugSystemById(long drugSystemId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var drugSystemModel = _drugSystemService.GetDrugSystemById(drugSystemId, ref errorResponseModel);

                if (drugSystemModel != null)
                {
                    return Ok(drugSystemModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get all DrugSystem
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet("GetDrugSystem")]
        [ProducesResponseType(typeof(DrugSystemModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetDrugSystem()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var drugSystemModelList = _drugSystemService.GetDrugSystem(ref errorResponseModel);

                if (drugSystemModelList != null)
                {
                    return Ok(drugSystemModelList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To add new DrugSystem 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(DrugSystemModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult SaveDrugSystem(DrugSystemModel drugSystemModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var drugSystemEntity = _drugSystemService.SaveDrugSystem(drugSystemModel, ref errorResponseModel);

                if (drugSystemEntity != null)
                {
                    return Ok(drugSystemEntity);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To delete DrugSystem 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteDrugSystem")]
        [ProducesResponseType(typeof(DrugSystemModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeleteDrugSystem(DrugSystemModel drugSystemModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var drugSystemEntity = _drugSystemService.DeleteDrugSystem(drugSystemModel, ref errorResponseModel);

                if (drugSystemEntity != null)
                {
                    return Ok(drugSystemEntity);
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
