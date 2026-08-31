using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIGA.Centrum.Business.Implementation;
using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Entity.DataModels;
using NIGA.Centrum.Model;

namespace NIGA.Centrum.API.Controllers
{
    /// <summary>
    /// APIs for SubSection entity 
    /// </summary>
    [Route("api/subsection")]
    [ApiController]
    [Authorize]
    public class SubSectionController : BaseAPIController
    {
        ISubSectionService _subsectionService;
        /// <summary>
        /// Used to initialize controller and inject subsection service
        /// </summary>
        /// <param name="subsectionService"></param>
        public SubSectionController(ISubSectionService subsectionService)
        {
            _subsectionService = subsectionService;
        }

        /// <summary>
        /// To get subsection by SubSection ID 
        /// </summary>
        /// <param name="subsectionId"></param>
        /// <returns></returns>
        [HttpGet("{subsectionId}")]
        [ProducesResponseType(typeof(SubSectionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetSubSectionById(long subsectionId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var subsectionModel = _subsectionService.GetSubSectionById(subsectionId, ref errorResponseModel);

                if (subsectionModel != null)
                {
                    return Ok(subsectionModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get all subsections
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet("GetSubSections/{sectionId}")]
        [ProducesResponseType(typeof(SubSectionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetSubSections(int sectionId, [FromQuery] NigaParameters nigaParameters)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var subsectionModelList = _subsectionService.GetSubSections(sectionId,nigaParameters);

                if (subsectionModelList != null)
                {
                    return Ok(subsectionModelList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To add new SubSection 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(SubSectionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult SaveSubSection(List<SubSectionModel> subSectionModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var subsectionModel = _subsectionService.SaveSubSection(subSectionModel, ref errorResponseModel);

                if (subsectionModel != null)
                {
                    return Ok(subsectionModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To delete SubSection 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteSubSection")]
        [ProducesResponseType(typeof(SubSectionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeleteSubSection(SubSectionModel subSectionModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var subsectionModel = _subsectionService.DeleteSubSection(subSectionModel, ref errorResponseModel);

                if (subsectionModel != null)
                {
                    return Ok(subsectionModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get all subsections by section selected
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost("GetSubSectionsBySection")]
        [ProducesResponseType(typeof(SectionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetSubSectionsBySection(SectionModel sectionModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var subsectionModelList = _subsectionService.GetSubSectionsBySection(sectionModel,ref errorResponseModel);

                if (subsectionModelList != null)
                {
                    return Ok(subsectionModelList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get all subsections
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet("GetSubSections")]
        [ProducesResponseType(typeof(SubSectionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetSubSections()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var subsectionModelList = _subsectionService.GetSubSections(ref errorResponseModel);

                if (subsectionModelList != null)
                {
                    return Ok(subsectionModelList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }




        [HttpGet("GetSubSectionsByDate/{userId}")]
        [ProducesResponseType(typeof(SubSectionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetSubSectionsByDate(int userId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var subsectionModelList = _subsectionService.GetSubSectionsByDate(userId, ref errorResponseModel);

                if (subsectionModelList != null)
                {
                    return Ok(subsectionModelList);
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
        [Route("DeleteSubSectionLanguageDetails")]
        [ProducesResponseType(typeof(SubSectionLanguageDetailsModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeleteSubSectionLanguageDetails(SubSectionLanguageDetailsModel subSectionLanguageDetailsModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var SubSectionLanguageDetailsModel = _subsectionService.DeleteSubSectionLanguageDetails(subSectionLanguageDetailsModel, ref errorResponseModel);

                if (SubSectionLanguageDetailsModel != null)
                {
                    return Ok(SubSectionLanguageDetailsModel);
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
        [Route("DeleteReferenceRubricDetails")]
        [ProducesResponseType(typeof(ReferenceRubricDetailsModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeleteReferenceRubricDetails(ReferenceRubricDetailsModel referenceRubricDetailsModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var ReferenceRubricDetailsModel = _subsectionService.DeleteReferenceRubricDetails(referenceRubricDetailsModel, ref errorResponseModel);

                if (ReferenceRubricDetailsModel != null)
                {
                    return Ok(ReferenceRubricDetailsModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get all subsections
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet("GetSubSectionsWithPagination/{sectionId}")]
        [ProducesResponseType(typeof(SubSectionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetSubSectionsWithPagination(int sectionId, [FromQuery] NigaParameters nigaParameters)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var subsectionModelList = _subsectionService.GetSubSectionsWithPagination(sectionId, nigaParameters);

                if (subsectionModelList != null)
                {
                    return Ok(subsectionModelList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Get subsection with its children and child count
        /// </summary>
        [HttpGet("GetSubSectionWithChildrenCount/{subsectionId}")]
        [ProducesResponseType(typeof(List<SubSectionLevelModel>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetSubSectionWithChildrenCount(long subsectionId)
        {
            ErrorResponseModel errorResponseModel = null;

            try
            {
                var result = _subsectionService
                    .GetSubSectionWithChildrenCount(subsectionId, ref errorResponseModel);

                if (result != null && result.Count > 0)
                {
                    return Ok(result);
                }

                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    ex.Message
                );
            }
        }



        /// <summary>
        /// Get main parent subsections with child count by section id
        /// </summary>
        [HttpGet("GetMainParentSubSectionsWithChildCount/{sectionId}")]
        [ProducesResponseType(typeof(List<SubSectionLevelModel>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetMainParentSubSectionsWithChildCount(long sectionId)
        {
            ErrorResponseModel errorResponseModel = null;

            try
            {
                var result = _subsectionService
                    .GetMainParentSubSectionsWithChildCount(sectionId, ref errorResponseModel);

                if (result != null && result.Count > 0)
                {
                    return Ok(result);
                }

                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    ex.Message
                );
            }
        }

        /// <summary>
        /// To update MainParentSubsection against subsectionId
        /// </summary>
        /// <param name="subsectionId"></param>
        /// <param name="mainParentSubsection"></param>
        /// <param name="changedBy"></param>
        /// <returns></returns>
        [HttpPost("UpdateMainParentSubsection/{subsectionId}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult UpdateMainParentSubsection(long subsectionId, [FromQuery] bool mainParentSubsection, [FromQuery] string changedBy)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var result = _subsectionService.UpdateMainParentSubsection(subsectionId, mainParentSubsection, changedBy, ref errorResponseModel);

                if (result != null && !string.IsNullOrEmpty(result))
                {
                    return Ok(result);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(
       [FromQuery] string query,
       [FromQuery] int top = 20)
        {
            var result = await _subsectionService.SearchAsync(query, top);
            return Ok(result);
        }

        /// <summary>
        /// Search subsections within a section (autocomplete + tree filter).
        /// </summary>
        [HttpGet("SearchBySection")]
        [ProducesResponseType(typeof(List<SubSectionSearchResultModel>), 200)]
        public async Task<IActionResult> SearchBySection(
            [FromQuery] long sectionId,
            [FromQuery] string query,
            [FromQuery] int top = 20)
        {
            if (sectionId <= 0)
            {
                return BadRequest("sectionId is required");
            }

            var result = await _subsectionService.SearchBySectionAsync(sectionId, query, top);
            return Ok(result);
        }

        /// <summary>
        /// Global subsection search across all sections (autocomplete + tree).
        /// </summary>
        [HttpGet("SearchGlobal")]
        [ProducesResponseType(typeof(List<SubSectionSearchResultModel>), 200)]
        public async Task<IActionResult> SearchGlobal(
            [FromQuery] string query,
            [FromQuery] int top = 20)
        {
            try
            {
                var result = await _subsectionService.SearchGlobalAsync(query, top);
                return Ok(result ?? new List<SubSectionSearchResultModel>());
            }
            catch (Exception)
            {
                return Ok(new List<SubSectionSearchResultModel>());
            }
        }

        /// <summary>
        /// Paginated global subsection search for tree results (all matches, level-wise).
        /// </summary>
        [HttpGet("SearchGlobalPaged")]
        [ProducesResponseType(typeof(SubSectionSearchPagedResultModel), 200)]
        public async Task<IActionResult> SearchGlobalPaged(
            [FromQuery] string query,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 40)
        {
            try
            {
                var result = await _subsectionService.SearchGlobalPagedAsync(query, pageNumber, pageSize);
                return Ok(result ?? new SubSectionSearchPagedResultModel());
            }
            catch (Exception)
            {
                return Ok(new SubSectionSearchPagedResultModel());
            }
        }

        /// <summary>
        /// Paginated section-scoped subsection search for tree results (all matches, level-wise).
        /// </summary>
        [HttpGet("SearchBySectionPaged")]
        [ProducesResponseType(typeof(SubSectionSearchPagedResultModel), 200)]
        public async Task<IActionResult> SearchBySectionPaged(
            [FromQuery] long sectionId,
            [FromQuery] string query,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 40)
        {
            if (sectionId <= 0)
            {
                return BadRequest("sectionId is required");
            }

            try
            {
                var result = await _subsectionService.SearchBySectionPagedAsync(sectionId, query, pageNumber, pageSize);
                return Ok(result ?? new SubSectionSearchPagedResultModel());
            }
            catch (Exception)
            {
                return Ok(new SubSectionSearchPagedResultModel());
            }
        }


    }
}