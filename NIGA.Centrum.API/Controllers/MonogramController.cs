using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NIGA.Centrum.Business.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NIGA.Centrum.Business.Implementation;
using NIGA.Centrum.Model;
using NIGA.Centrum.Entity.DataModels;
using System;

namespace NIGA.Centrum.API.Controllers
{
    [Route("api/Monogram")]
    [ApiController]
    [Authorize]
    public class MonogramController : BaseAPIController
    {
        IMonoGramService _monoGramService;
        /// <summary>
        /// Used to initialize controller and inject clinical questions
        /// </summary>
        /// <param name="monoGramService"></param>
        public MonogramController(IMonoGramService monoGramService)
        {
            _monoGramService = monoGramService;
        }
        /// <summary>
        /// To get clinical questions by Clinical Questions ID 
        /// </summary>
        /// <param name="MonogramById"></param>
        /// <returns></returns>
        [HttpGet("MonogramById")]
        [ProducesResponseType(typeof(MonoGramModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetMonoGramById(long MonogramId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var MonogramModel = _monoGramService.GetMonoGramById(MonogramId, ref errorResponseModel);

                if (MonogramModel != null)
                {
                    return Ok(MonogramModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// To Get all Authors
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetallMonogram")]
        [ProducesResponseType(typeof(AuthorMasterModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetMonogram()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var MonogramModel = _monoGramService.GetMonogram(ref errorResponseModel);

                if (MonogramModel != null)
                {
                    return Ok(MonogramModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }








        /// <summary>
        /// To add new Monogram
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>

        [HttpPost]
        [ProducesResponseType(typeof(MonoGramModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult SaveClinicalQuestions(MonoGramModel monoGramModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var monoGramModel1 = _monoGramService.SaveMonogram(monoGramModel, ref errorResponseModel);

                if (monoGramModel != null)
                {
                    return Ok(monoGramModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To delete clinical questions 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteMonogram")]
        [ProducesResponseType(typeof(MonoGramModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeleteMonogram(MonoGramModel monoGramModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var MonoGramModel = _monoGramService.DeleteMonogram(monoGramModel, ref errorResponseModel);

                if (monoGramModel != null)
                {
                    return Ok(monoGramModel);
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
