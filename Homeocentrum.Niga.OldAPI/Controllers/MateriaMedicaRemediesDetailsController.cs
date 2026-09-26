using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Homeocentrum.Niga.OldAPI.Business.Interface;
using Homeocentrum.Niga.OldAPI.Model;
using System;

using Homeocentrum.Niga.OldAPI.Common;
namespace Homeocentrum.Niga.OldAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [DoctorOnly]
    public class MateriaMedicaRemediesDetailsController : BaseAPIController
    {
        IMateriaMedicaRemediesDetails _materiamediService;
        /// <summary>
        /// Used to initialize controller and inject MateriaMedicaHead service
        /// </summary>
        /// <param name="materiamedicaheadService"></param>
        public MateriaMedicaRemediesDetailsController(IMateriaMedicaRemediesDetails materiamedicaheadService)
        {
            _materiamediService = materiamedicaheadService;
        }

        [HttpGet("GetMateriaMedicaRemediesDetails")]
        [ProducesResponseType(typeof(MateriaMedicaRemediesDetailsModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetMateriaMedicaRemediesDetails(long remedyId, long authorId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var materiamedicaList = _materiamediService.GetMateriaMedicaRemediesDetails(remedyId,authorId, ref errorResponseModel);

                if (materiamedicaList != null)
                {
                    return Ok(materiamedicaList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>CLN-10.02 — lightweight get-by-remedy (all authors) on classic MM host.</summary>
        [HttpGet("GetMateriaMedicaByRemedy/{remedyId}")]
        [DoctorOnly]
        [ProducesResponseType(typeof(MateriaMedicaRemediesDetailsModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult GetMateriaMedicaByRemedy(long remedyId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var list = _materiamediService.GetMateriaMedicaByRemedy(remedyId, ref errorResponseModel);
                if (list != null && list.Count > 0)
                    return Ok(list);
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
