using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIGA.Centrum.Business.Implementation;
using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Model;
using System;

namespace NIGA.Centrum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RepertorizationPageController : BaseAPIController
    {
        IRepertorizationPageService repertorizationPageService;
        /// <summary>
        /// Used to initialize controller and inject MateriaMedicaHead service
        /// </summary>
        /// <param name="materiamedicaheadService"></param>
        public RepertorizationPageController(IRepertorizationPageService _repertorizationPageService)
        {
            repertorizationPageService = _repertorizationPageService;
        }

        [HttpGet("GetMateriaMedicaHeadingbyAuthorId/{authorId}")]
        [ProducesResponseType(typeof(MateriaMedicaHeadMasterModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetMateriaMedicaHeadingbyAuthorId(int authorId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var materiamedicaheadModel = repertorizationPageService.GetMateriaMedicaHeadingbyAuthorId(authorId);

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
        [HttpPost("GetDifferentialMateriaMedica")]
        [ProducesResponseType(typeof(MateriaMedicaHeadMasterModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetDifferentialMateriaMedica(DifferentialMateriaMedica differentialMateriaMedica)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var differentialMateriaMedicaLists = repertorizationPageService.GetDifferentialMateriaMedica(differentialMateriaMedica);

                if (differentialMateriaMedicaLists != null)
                {
                    return Ok(differentialMateriaMedicaLists);
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
