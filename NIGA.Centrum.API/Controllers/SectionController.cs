using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NIGA.Centrum.API.Controllers
{
    /// <summary>
    /// APIs for Section entity 
    /// </summary>
    [Route("api/section")]
    [ApiController]
    [Authorize]
    public class SectionController : BaseAPIController
    {
        ISectionService _sectionService;
        /// <summary>
        /// Used to initialize controller and inject section service
        /// </summary>
        /// <param name="sectionService"></param>
        public SectionController(ISectionService sectionService)
        {
            _sectionService = sectionService;
        }

        /// <summary>
        /// To get section by Section ID 
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        [HttpGet("{sectionId}")]
        [ProducesResponseType(typeof(SectionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetSectionById(long sectionId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var sectionModel = _sectionService.GetSectionById(sectionId, ref errorResponseModel);

                if (sectionModel != null)
                {
                    return Ok(sectionModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get all Sections
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(SectionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAllSections()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var sectionModel = _sectionService.getAllSections( ref errorResponseModel);

                if (sectionModel != null)
                {
                    return Ok(sectionModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To add new Section 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(SectionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult SaveSection(SectionModel sectionModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var sectionmodel = _sectionService.SaveSection(sectionModel, ref errorResponseModel);

                if (sectionmodel != null)
                {
                    return Ok(sectionmodel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To delete Section 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteSection")]
        [ProducesResponseType(typeof(SectionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeleteSection(SectionModel sectionModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var sectionmodel = _sectionService.DeleteSection(sectionModel, ref errorResponseModel);

                if (sectionmodel != null)
                {
                    return Ok(sectionmodel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }




        [HttpGet]
        [Route("GetAllRemedyByFilter")]
        [ProducesResponseType(typeof(SectionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult getAllRemedyByFilter(string search, int SectionId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var sectionModel = _sectionService.getAllRemedyByFilter( search,  SectionId, ref errorResponseModel);

                if (sectionModel != null)
                {
                    return Ok(sectionModel);
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