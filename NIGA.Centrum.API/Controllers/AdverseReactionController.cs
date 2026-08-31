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
    public class AdverseReactionController : BaseAPIController
    {
        IAdverseReactionService _adverseReactionService;
        /// <summary>
        /// Used to initialize controller and inject author service
        /// </summary>
        /// <param name="adverseReactionService"></param>
        public AdverseReactionController(IAdverseReactionService adverseReactionService)
        {
            _adverseReactionService = adverseReactionService;
        }

        ///// <summary>
        ///// To get qualification by Qualification ID 
        ///// </summary>
        ///// <param name="adverseReactionId"></param>
        ///// <returns></returns>
        //[HttpGet("{adverseReactionId}")]
        //[ProducesResponseType(typeof(AdverseReactionModel), 200)]
        //[ProducesResponseType(typeof(string), 404)]
        //[ProducesResponseType(typeof(string), 400)]
        //[ProducesResponseType(typeof(string), 500)]
        //public IActionResult GetAdverseReactionById(long adverseReactionId)
        //{
        //    ErrorResponseModel errorResponseModel = null;
        //    try
        //    {
        //        var adverseReactionModel = _adverseReactionService.GetAdverseReactionnById(adverseReactionId, ref errorResponseModel);

        //        if (adverseReactionModel != null)
        //        {
        //            return Ok(adverseReactionModel);
        //        }
        //        return ReturnErrorResponse(errorResponseModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        ///// <summary>
        ///// To get all qualifications
        ///// </summary>
        ///// <param name=""></param>
        ///// <returns></returns>
        //[HttpGet("GetAdverseReaction")]
        //[ProducesResponseType(typeof(AdverseReactionModel), 200)]
        //[ProducesResponseType(typeof(string), 404)]
        //[ProducesResponseType(typeof(string), 400)]
        //[ProducesResponseType(typeof(string), 500)]
        //public IActionResult GetAdverseReaction()
        //{
        //    ErrorResponseModel errorResponseModel = null;
        //    try
        //    {
        //        var qualificationModelList = _adverseReactionService.GetAdverseReaction(ref errorResponseModel);

        //        if (qualificationModelList != null)
        //        {
        //            return Ok(qualificationModelList);
        //        }
        //        return ReturnErrorResponse(errorResponseModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        ///// <summary>
        ///// To add new Qualification 
        ///// </summary>
        ///// <param name=""></param>
        ///// <returns></returns>
        //[HttpPost]
        //[ProducesResponseType(typeof(AdverseReactionModel), 200)]
        //[ProducesResponseType(typeof(string), 404)]
        //[ProducesResponseType(typeof(string), 400)]
        //[ProducesResponseType(typeof(string), 500)]
        //public IActionResult SaveAdverseReaction(AdverseReactionModel adverseReactionModel)
        //{
        //    ErrorResponseModel errorResponseModel = null;
        //    try
        //    {
        //        var adverseReactionEntity = _adverseReactionService.SaveAdverseReaction(adverseReactionModel, ref errorResponseModel);

        //        if (adverseReactionEntity != null)
        //        {
        //            return Ok(adverseReactionEntity);
        //        }
        //        return ReturnErrorResponse(errorResponseModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        /// <summary>
        /// To delete Qualification 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteAdverseReaction")]
        [ProducesResponseType(typeof(AdverseReactionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeleteAdverseReaction(AdverseReactionModel adverseReactionModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var adverseReactionEntry = _adverseReactionService.DeleteAdverseReaction(adverseReactionModel, ref errorResponseModel);

                if (adverseReactionEntry != null)
                {
                    return Ok(adverseReactionEntry);
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
