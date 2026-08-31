using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIGA.Centrum.Business.Implementation;
using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Common;
using NIGA.Centrum.Entity.DataModels;
using NIGA.Centrum.Model;
using System;

namespace NIGA.Centrum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MeteriaMedicaDetailsController : BaseAPIController
    {
        IMateriaMedicaDetailService _materiamedicadetailService;
        /// <summary>
        /// Used to initialize controller and inject MateriaMedica service
        /// </summary>
        /// <param name="materiamedicadetailService"></param>
        public MeteriaMedicaDetailsController(IMateriaMedicaDetailService materiamedicadetailService)
        {
            _materiamedicadetailService = materiamedicadetailService;
        }

        /// <summary>
        /// To get MateriaMedica by MateriamedicaID 
        /// </summary>
        /// <param name="materiamedicadetailId"></param>
        /// <returns></returns>

        [HttpGet("{materiamedicadetailId}")]
        [ProducesResponseType(typeof(MateriaMedicaDetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetMateriaMedicaDetailsById(long materiamedicadetailId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var materiamedicaheadModel = _materiamedicadetailService.GetMateriaMedicaDetailsById(materiamedicadetailId, ref errorResponseModel);

                if (materiamedicaheadModel != null)
                {
                    return Ok(materiamedicaheadModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get all MateriaMedicaHead
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet("GetMateriaMedicaDetails")]
        [ProducesResponseType(typeof(MateriaMedicaDetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetMateriaMedicaDetails()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var materiamedicaheadList = _materiamedicadetailService.GetMateriaMedicaDetails(ref errorResponseModel);

                if (materiamedicaheadList != null)
                {
                    return Ok(materiamedicaheadList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To add new MateriaMedicaHead 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(MateriaMedicaDetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult SaveMateriaMedicaDetails(MateriaMedicaDetailModel materiamedicadetailmodel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var materiamedicheadmodel = _materiamedicadetailService.SaveMateriaMedicaDetails(materiamedicadetailmodel, ref errorResponseModel);

                if (materiamedicheadmodel != null)
                {
                    return Ok(materiamedicheadmodel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To delete MateriaMedicaHead 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteMateriaMedicaDetails")]
        [ProducesResponseType(typeof(MateriaMedicaDetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeleteMateriaMedicaDetails(MateriaMedicaDetailModel materiamedicadetailmodel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var materiamedicaheadmodel = _materiamedicadetailService.DeleteMateriaMedicaDetails(materiamedicadetailmodel, ref errorResponseModel);

                if (materiamedicaheadmodel != null)
                {
                    return Ok(materiamedicaheadmodel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// To get all MateriaMedicaHead by authorId
        /// </summary>
        /// <param name="authorId"></param>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet("GetMateriaMedicaDetail/{materiamedicaId}")]
        [ProducesResponseType(typeof(MateriaMedicaHeadMasterModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetMateriaMedicaDetail(long materiamedicaId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var meteriamedicaheadModelList = _materiamedicadetailService.GetMateriaMedicaDetail(materiamedicaId, ref errorResponseModel);

                if (meteriamedicaheadModelList != null)
                {
                    return Ok(meteriamedicaheadModelList);
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

