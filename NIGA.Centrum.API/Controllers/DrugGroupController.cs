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
    public class DrugGroupController : BaseAPIController
    {
        IDrugGroupService _drugSystemService;
        /// <summary>
        /// Used to initialize controller and inject author service
        /// </summary>
        /// <param name="authorService"></param>
        public DrugGroupController(IDrugGroupService drugSystemService)
        {
            _drugSystemService = drugSystemService;
        }

        /// <summary>
        /// To get DrugGroup by drugSystemID 
        /// </summary>
        /// <param name="drugSystemId"></param>
        /// <returns></returns>
        [HttpGet("{drugSystemId}")]
        [ProducesResponseType(typeof(DrugGroupModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetDrugGroupById(long drugSystemId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var drugGroupModel = _drugSystemService.GetDrugGroupById(drugSystemId, ref errorResponseModel);

                if (drugGroupModel != null)
                {
                    return Ok(drugGroupModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get all DrugGroup
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet("GetDrugGroup")]
        [ProducesResponseType(typeof(DrugGroupModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetDrugGroup()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var drugGroupModelList = _drugSystemService.GetDrugGroup(ref errorResponseModel);

                if (drugGroupModelList != null)
                {
                    return Ok(drugGroupModelList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To add new DrugGroup 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(DrugGroupModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult SaveDrugGroup(DrugGroupModel drugGroupModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var drugGroupEntity = _drugSystemService.SaveDrugGroup(drugGroupModel, ref errorResponseModel);

                if (drugGroupEntity != null)
                {
                    return Ok(drugGroupEntity);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To delete DrugGroup 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteDrugGroup")]
        [ProducesResponseType(typeof(DrugGroupModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeleteDrugGroup(DrugGroupModel drugGroupModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var drugGroupEntity = _drugSystemService.DeleteDrugGroup(drugGroupModel, ref errorResponseModel);

                if (drugGroupEntity != null)
                {
                    return Ok(drugGroupEntity);
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
