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
    /// APIs for Country entity 
    /// </summary>
    [Route("api/country")]
    [ApiController]

    public class CountryController : BaseAPIController
    {
        ICountryService _countryService;
        /// <summary>
        /// Used to initialize controller and inject country service
        /// </summary>
        /// <param name="countryService"></param>
        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        /// <summary>
        /// To get country by Country ID 
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        [HttpGet("{countryId}")]
        [ProducesResponseType(typeof(CountryModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetCountryById(long countryId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var countryModel = _countryService.GetCountryById(countryId, ref errorResponseModel);

                if (countryModel != null)
                {
                    return Ok(countryModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To Get all counrriecountries
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(CountryModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetCountries()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var countryModelList = _countryService.GetCountries(ref errorResponseModel);

                if (countryModelList.Count != 0)
                {
                    return Ok(countryModelList);
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