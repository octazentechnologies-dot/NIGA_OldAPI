using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Model;

namespace NIGA.Centrum.API.Controllers
{
    /// <summary>
    /// APIs for Remedy entity 
    /// </summary>
    [Route("api/remedy")]
    [ApiController]
    [Authorize]
    public class RemedyController : BaseAPIController
    {
        IRemedyService _remedyService;
        /// <summary>
        /// Used to initialize controller and inject remedy service
        /// </summary>
        /// <param name="remedyService"></param>
        public RemedyController(IRemedyService remedyService)
        {
            _remedyService = remedyService;
        }

        /// <summary>
        /// To get remedy by Remedy ID 
        /// </summary>
        /// <param name="remedyId"></param>
        /// <returns></returns>
        [HttpGet("{remedyId}")]
        [ProducesResponseType(typeof(RemedyModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetRemedyById(long remedyId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var remedyModel = _remedyService.GetRemedyById(remedyId, ref errorResponseModel);

                if (remedyModel != null)
                {
                    return Ok(remedyModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get all remedies
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet("GetRemedies")]
        [ProducesResponseType(typeof(SearchRemedyModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetRemedies(string search)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var remedyModelList = _remedyService.GetRemedies(search,ref errorResponseModel);

                if (remedyModelList != null)
                {
                    return Ok(remedyModelList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To add new Remedie 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(RemedyModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult SaveRemedy(RemedyModel remedyModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var remedymodel = _remedyService.SaveRemedy(remedyModel, ref errorResponseModel);

                if (remedymodel != null)
                {
                    return Ok(remedymodel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To delete Remedie 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteRemedy")]
        [ProducesResponseType(typeof(RemedyModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeleteRemedy(RemedyModel remedyModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var remedymodel = _remedyService.DeleteRemedy(remedyModel, ref errorResponseModel);

                if (remedymodel != null)
                {
                    return Ok(remedymodel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// To get all remedies by subsection
        /// </summary>
        /// <param name="subSectionId"></param>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet("GetRemediesBySubSection/{subSectionId}")]
        [ProducesResponseType(typeof(RemedyModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetRemediesBySubSection(long subSectionId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var remedyModelList = _remedyService.GetRemedyBySection(subSectionId,ref errorResponseModel);

                if (remedyModelList != null)
                {
                    return Ok(remedyModelList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Added by Vikas More
        /// <summary>
        /// To get all remedies by subsection
        /// </summary>
        /// <param name="subSectionId"></param>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet("GetCommonUnCommonRemedyBySection/{subSectionId}")]
        [ProducesResponseType(typeof(RemedyCommonUncommonModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetCommonUnCommonRemedyBySection(long subSectionId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var remedyModelList = _remedyService.GetRemedyBySection(subSectionId, ref errorResponseModel);

                if (remedyModelList != null)
                {
                    return Ok(remedyModelList);
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