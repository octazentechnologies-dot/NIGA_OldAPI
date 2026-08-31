using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;

namespace NIGA.Centrum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class SubscriptionController : BaseAPIController
    {
        ISubscriptionService _subscriptionService;
        /// <summary>
        /// Used to initialize controller and inject subscription service
        /// </summary>
        /// <param name="subscriptionService"></param>
        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        /// <summary>
        /// To get all lab entries
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet("GetSubscription")]
        [ProducesResponseType(typeof(List<SubscriptionModel>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetSubscription()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var subscriptionEntryModel = _subscriptionService.GetSubscription(ref errorResponseModel);

                if (subscriptionEntryModel != null)
                {
                    return Ok(subscriptionEntryModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get all lab entries
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet("GetSubscriptionById/{packageDetailId}")]
        [ProducesResponseType(typeof(SubscriptionModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetPatientLabEntry(int packageDetailId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var subscriptionModel = _subscriptionService.GetSubscriptionById(packageDetailId, ref errorResponseModel);

                if (subscriptionModel != null)
                {
                    return Ok(subscriptionModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get all save lab order
        /// </summary>
        /// <param name="patientLabOrderModel"></param>
        /// <returns></returns>
        [HttpPost("SaveUpdateSubscription")]
        [ProducesResponseType(typeof(PatientLabOrderModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult SaveUpdateSubscription(SubscriptionModel subscriptionModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                int userId = 0;
                var identity = User?.Identity as System.Security.Claims.ClaimsIdentity;
                if (identity != null && identity.IsAuthenticated)
                {
                    var userIdClaim = identity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                                      ?? identity.FindFirst("UserId")?.Value; // fallback

                    if (!string.IsNullOrEmpty(userIdClaim) && int.TryParse(userIdClaim, out int parsedUserId))
                    {
                        userId = parsedUserId;
                    }
                }


                var response = _subscriptionService.SaveSubscription(subscriptionModel, userId, ref errorResponseModel);

                if (response != null)
                {
                    return Ok(response);
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
