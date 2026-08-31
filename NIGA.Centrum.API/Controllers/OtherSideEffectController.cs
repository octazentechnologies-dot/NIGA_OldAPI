using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Model;
using System;

namespace NIGA.Centrum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OtherSideEffectController : BaseAPIController
    {
        IOtherSideEffectService _otherSideEffectService;
        /// <summary>
        /// Used to initialize controller and inject author service
        /// </summary>
        /// <param name="adverseReactionService"></param>
        public OtherSideEffectController(IOtherSideEffectService otherSideEffectService)
        {
            _otherSideEffectService = otherSideEffectService;
        }
        /// <summary>
        /// To delete OtherSideEffect 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteOtherSideEffect")]
        [ProducesResponseType(typeof(AdverseReactionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeleteOtherSideEffect(OtherSideEffectModel otherSideEffectModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var otherSideEffectEntry = _otherSideEffectService.DeleteOtherSideEffect(otherSideEffectModel, ref errorResponseModel);

                if (otherSideEffectEntry != null)
                {
                    return Ok(otherSideEffectEntry);
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
