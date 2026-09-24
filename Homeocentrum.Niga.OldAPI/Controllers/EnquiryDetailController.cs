using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Homeocentrum.Niga.OldAPI.Business.Interface;
using Homeocentrum.Niga.OldAPI.Model;
using System;

namespace Homeocentrum.Niga.OldAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 
    public class EnquiryDetailController : BaseAPIController
    {
        IEnquiryDetailService enquiryDetailService;
        private readonly IOptions<SmtpSettingsModel> _mailSettings;

        /// <summary>
        /// Used to initialize controller and inject EnquiryDetail service
        /// </summary>
        /// <param name="_blogDetailService"></param>
        public EnquiryDetailController(IEnquiryDetailService _enquiryDetailService, IOptions<SmtpSettingsModel> mailSettings)
        {
            enquiryDetailService = _enquiryDetailService;
            _mailSettings = mailSettings;

        }

        /// <summary>
        /// To get EnquiryDetail by enquiryId 
        /// </summary>
        /// <param name="enquiryId"></param>
        /// <returns></returns>
        [HttpGet("GetEnquiryDetailById/{enquiryId}")]
        [Authorize]
        [ProducesResponseType(typeof(EnquiryDetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetEnquiryDetailById(long enquiryId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var enquiryModel = enquiryDetailService.GetEnquiryDetailById(enquiryId, ref errorResponseModel);

                if (enquiryModel != null)
                {
                    return Ok(enquiryModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To Get all EnquiryDetail
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllEnquiryDetails")]
        [Authorize]
        [ProducesResponseType(typeof(EnquiryDetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAllEnquiryDetails()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var enquiryModel = enquiryDetailService.GetAllEnquiryDetails(ref errorResponseModel);

                if (enquiryModel != null)
                {
                    return Ok(enquiryModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To add/update new EnquiryDetail 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(EnquiryDetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult SaveEnquiryDetail(EnquiryDetailModel model)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var gemailSettings = _mailSettings.Value;

                var enquirymodel = enquiryDetailService.SaveEnquiryDetail(model, gemailSettings, ref errorResponseModel);

                if (enquirymodel != null)
                {
                    return Ok(enquirymodel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To delete EnquiryDetail 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteEnquiryDetail")]
        [Authorize]
        [ProducesResponseType(typeof(EnquiryDetailModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeleteEnquiryDetail(long enquiryId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var enquirymodel = enquiryDetailService.DeleteEnquiryDetail(enquiryId, ref errorResponseModel);

                if (enquirymodel != null)
                {
                    return Ok(enquirymodel);
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
