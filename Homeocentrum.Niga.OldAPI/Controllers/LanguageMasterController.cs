using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Homeocentrum.Niga.OldAPI.Business.Implementation;
using Homeocentrum.Niga.OldAPI.Business.Interface;
using Homeocentrum.Niga.OldAPI.Model;
using System;

using Homeocentrum.Niga.OldAPI.Common;
namespace Homeocentrum.Niga.OldAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LanguageMasterController : BaseAPIController
    {

        ILanguageMasterService languageMasterService;
        /// <summary>
        /// Used to initialize controller and inject author service
        /// </summary>
        /// <param name="languageMasterService"></param>
        public LanguageMasterController(ILanguageMasterService _languageMasterService)
        {
            languageMasterService = _languageMasterService;
        }



        /// <summary>
        /// To Get all Language
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(LanguageMasterModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetLanguage()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var LanguageModelList = languageMasterService.GetLanguage(ref errorResponseModel);

                if (LanguageModelList != null)
                {
                    return Ok(LanguageModelList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }




        /// <summary>
        /// To add new Language 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(LanguageMasterModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [Authorize(Policy = AdminAuthorizationPolicies.AdminPortal)]
        public IActionResult SaveLanguage(LanguageMasterModel languagemasterModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var languageModel = languageMasterService.SaveLanguage(languagemasterModel, ref errorResponseModel);

                if (languageModel != null)
                {
                    return Ok(languageModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }




        /// <summary>
        /// To delete Language 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteLanguage")]
        [ProducesResponseType(typeof(LanguageMasterModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        [Authorize(Policy = AdminAuthorizationPolicies.AdminPortal)]
        public IActionResult DeleteLanguage(LanguageMasterModel languagemasterModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var LanguageModel = languageMasterService.DeleteLanguage(languagemasterModel, ref errorResponseModel);

                if (LanguageModel != null)
                {
                    return Ok(LanguageModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }




        /// <summary>
        /// To get Language by languageId 
        /// </summary>
        /// <param name="languageId"></param>
        /// <returns></returns>
        [HttpGet("GetLanguageById/{languageId}")]
        [ProducesResponseType(typeof(LanguageMasterModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetLanguageById(long languageId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var LanguageModel = languageMasterService.GetLanguageById(languageId, ref errorResponseModel);

                if (LanguageModel != null)
                {
                    return Ok(LanguageModel);
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
