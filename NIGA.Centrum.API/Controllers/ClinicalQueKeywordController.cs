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
    public class ClinicalQueKeywordController : BaseAPIController
    {
        IClinicalQueKeywordService _clinicalQueKeywordService;
        /// <summary>
        /// Used to initialize controller and inject ClinicalQueKeyword service
        /// </summary>
        /// <param name="clinicalQueKeywordService"></param>
        public ClinicalQueKeywordController(IClinicalQueKeywordService clinicalQueKeywordService)
        {
            _clinicalQueKeywordService = clinicalQueKeywordService;
        }


        /// <summary>
        /// To get CliniclQueKeywords by ClinicalQueKeywordId 
        /// </summary>
        /// <param name="ClinicalQueKeywordId"></param>
        /// <returns></returns>
        [HttpGet("GetClinicalQueKeywordById/{ClinicalQueKeywordId}")]
        [ProducesResponseType(typeof(ClinicalQueKeywordsModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetClinicalQueKeywordById(long ClinicalQueKeywordId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var cliniclqueKeywordsModel = _clinicalQueKeywordService.GetClinicalQueKeywordById(ClinicalQueKeywordId, ref errorResponseModel);

                if (cliniclqueKeywordsModel != null)
                {
                    return Ok(cliniclqueKeywordsModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// To Get all CliniclQueKeywords
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ClinicalQueKeywordsModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetClinicalQueKeyword()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var cliniclquekeywordsModelList = _clinicalQueKeywordService.GetClinicalQueKeyword( ref errorResponseModel);

                if (cliniclquekeywordsModelList != null)
                {
                    return Ok(cliniclquekeywordsModelList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// To add new CliniclQueKeywords 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ClinicalQueKeywordsModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult SaveClinicalQueKeyword(ClinicalQueKeywordsModel quekeywordModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var cliniclqueKeywordsModel = _clinicalQueKeywordService.SaveClinicalQueKeyword(quekeywordModel, ref errorResponseModel);

                if (cliniclqueKeywordsModel != null)
                {
                    return Ok(cliniclqueKeywordsModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


        /// <summary>
        /// To delete CliniclQueKeywords
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteClinicalQueKeyword")]
        [ProducesResponseType(typeof(ClinicalQueKeywordsModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeleteClinicalQueKeyword(ClinicalQueKeywordsModel quekeywordModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var cliniclqueKeywordsModel = _clinicalQueKeywordService.DeleteClinicalQueKeyword(quekeywordModel, ref errorResponseModel);

                if (cliniclqueKeywordsModel != null)
                {
                    return Ok(cliniclqueKeywordsModel);
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
