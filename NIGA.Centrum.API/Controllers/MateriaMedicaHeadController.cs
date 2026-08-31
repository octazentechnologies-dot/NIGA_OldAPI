using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace NIGA.Centrum.API.Controllers
{
    /// <summary>
    /// APIs for  MateriaMedicaHead entity 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MateriaMedicaHeadController : BaseAPIController
    {
        IMateriaMedicaHeadMasterService _materiamedicaheadService;
        /// <summary>
        /// Used to initialize controller and inject MateriaMedicaHead service
        /// </summary>
        /// <param name="materiamedicaheadService"></param>
        public MateriaMedicaHeadController(IMateriaMedicaHeadMasterService materiamedicaheadService)
            {
                _materiamedicaheadService = materiamedicaheadService;
            }

        /// <summary>
        /// To get Materiamedicahead by HeadID 
        /// </summary>
        /// <param name="materiamedicaheadId"></param>
        /// <returns></returns>
        [HttpGet("{materiamedicaheadId}")]
            [ProducesResponseType(typeof(MateriaMedicaHeadMasterModel), 200)]
            [ProducesResponseType(typeof(string), 404)]
            [ProducesResponseType(typeof(string), 400)]
            [ProducesResponseType(typeof(string), 500)]
            public IActionResult GetMateriaMedicaHeadById(long materiamedicaheadId)
            {
                ErrorResponseModel errorResponseModel = null;
                try
                {
                    var materiamedicaheadModel = _materiamedicaheadService.GetMateriaMedicaHeadById(materiamedicaheadId, ref errorResponseModel);

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
        [HttpGet("GetMateriaMedicaHead")]
            [ProducesResponseType(typeof(MateriaMedicaHeadMasterModel), 200)]
            [ProducesResponseType(typeof(string), 404)]
            [ProducesResponseType(typeof(string), 400)]
            [ProducesResponseType(typeof(string), 500)]
            public IActionResult GetMateriaMedicaHead()
            {
                ErrorResponseModel errorResponseModel = null;
                try
                {
                    var materiamedicaheadList = _materiamedicaheadService.GetMateriaMedicaHead(ref errorResponseModel);

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
            [ProducesResponseType(typeof(MateriaMedicaHeadMasterModel), 200)]
            [ProducesResponseType(typeof(string), 404)]
            [ProducesResponseType(typeof(string), 400)]
            [ProducesResponseType(typeof(string), 500)]
            public IActionResult SaveMateriaMedicaHead(MateriaMedicaHeadMasterModel materiamedicheadModel)
            {
                ErrorResponseModel errorResponseModel = null;
                try
                {
                    var materiamedicheadmodel = _materiamedicaheadService.SaveMateriaMedicaHead(materiamedicheadModel, ref errorResponseModel);

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
            [Route("DeleteMateriaMedicaHead")]
            [ProducesResponseType(typeof(MateriaMedicaHeadMasterModel), 200)]
            [ProducesResponseType(typeof(string), 404)]
            [ProducesResponseType(typeof(string), 400)]
            [ProducesResponseType(typeof(string), 500)]
            public IActionResult DeleteMateriaMedicaHead(MateriaMedicaHeadMasterModel materiamedicaheadModel)
            {
                ErrorResponseModel errorResponseModel = null;
                try
                {
                    var materiamedicaheadmodel = _materiamedicaheadService.DeleteMateriaMedicaHead(materiamedicaheadModel, ref errorResponseModel);

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
        [HttpGet("GetMateriaMedicaHeadByAuthorId/{authorId}")]
            [ProducesResponseType(typeof(MateriaMedicaHeadMasterModel), 200)]
            [ProducesResponseType(typeof(string), 404)]
            [ProducesResponseType(typeof(string), 400)]
            [ProducesResponseType(typeof(string), 500)]
            public IActionResult GetMateriaMedicaHeadByAuthorId(long authorId)
            {
                ErrorResponseModel errorResponseModel = null;
                try
                {
                    var meteriamedicaheadModelList = _materiamedicaheadService.GetMateriaMedicaHeadByAuthorId(authorId, ref errorResponseModel);

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

        // Created by Vikas More


        /// <summary>
        /// To update differential materia medica status
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost("UpdateDifferentialMateriaMedicadDefaultStatus")]
        [ProducesResponseType(typeof(MateriaMedicaHeadMasterModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult UpdateDifferentialMateriaMedicadDefaultStatus(DifferentialMateriaMedicadDefaultStatusModel differentialMateriaMedicadDefaultStatus)
        {
            try
            {
                var updatedStatus = _materiamedicaheadService.UpdateDifferentialMateriaMedicadDefaultStatus(differentialMateriaMedicadDefaultStatus.MateriaMedicaHeadId, differentialMateriaMedicadDefaultStatus.DifferentialMM);
                return Ok(updatedStatus);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

