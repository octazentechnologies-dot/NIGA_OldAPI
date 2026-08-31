using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Model;

namespace NIGA.Centrum.API.Controllers
{
    /// <summary>
    /// APIs for gender entity 
    /// </summary>
    [Route("api/gender")]
    [ApiController]
    [Authorize]
    public class GenderController : BaseAPIController
    {
        IGenderService _genderService;

        /// <summary>
        /// Used to initialize controller and inject gender service
        /// </summary>
        /// <param name="genderService"></param>
        public GenderController(IGenderService genderService)
        {
            _genderService = genderService;
        }

        /// <summary>
        /// To get gender by Gender ID 
        /// </summary>
        /// <param name="genderId"></param>
        /// <returns></returns>
        [HttpGet("{genderId}")]
        [ProducesResponseType(typeof(GenderModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetGenderById(long genderId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var genderModel = _genderService.GetGenderById(genderId, ref errorResponseModel);

                if (genderModel != null)
                {
                    return Ok(genderModel);
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