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
    public class AllopathicDrugController : BaseAPIController
    {
        IAllopathicDrugService _allopathicDrugService;
    /// <summary>
    /// Used to initialize controller and inject author service
    /// </summary>
    /// <param name="authorService"></param>
    public AllopathicDrugController(IAllopathicDrugService allopathicDrugService)
    {
            _allopathicDrugService = allopathicDrugService;
    }

    /// <summary>
    /// To get qualification by Qualification ID 
    /// </summary>
    /// <param name="allopathicDrugId"></param>
    /// <returns></returns>
    [HttpGet("{allopathicDrugId}")]
    [ProducesResponseType(typeof(AllopathicDrugModel), 200)]
    [ProducesResponseType(typeof(string), 404)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 500)]
    public IActionResult GetAllopathicDrugById(long allopathicDrugId)
    {
        ErrorResponseModel errorResponseModel = null;
        try
        {
            var allopathicDrugModel = _allopathicDrugService.GetAllopathicDrugById(allopathicDrugId, ref errorResponseModel);

            if (allopathicDrugModel != null)
            {
                return Ok(allopathicDrugModel);
            }
            return ReturnErrorResponse(errorResponseModel);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// To get all qualifications
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    [HttpGet("GetAllopathicDrug")]
    [ProducesResponseType(typeof(AllopathicDrugModel), 200)]
    [ProducesResponseType(typeof(string), 404)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 500)]
    public IActionResult GetAllopathicDrug()
    {
        ErrorResponseModel errorResponseModel = null;
        try
        {
            var qualificationModelList = _allopathicDrugService.GetAllopathicDrug(ref errorResponseModel);

            if (qualificationModelList != null)
            {
                return Ok(qualificationModelList);
            }
            return ReturnErrorResponse(errorResponseModel);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// To add new Qualification 
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(AllopathicDrugModel), 200)]
    [ProducesResponseType(typeof(string), 404)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 500)]
    public IActionResult SaveAllopathicDrug(AllopathicDrugModel allopathicDrugModel)
    {
        ErrorResponseModel errorResponseModel = null;
        try
        {
            var adverseReactionEntity = _allopathicDrugService.SaveAllopathicDrug(allopathicDrugModel, ref errorResponseModel);

            if (adverseReactionEntity != null)
            {
                return Ok(adverseReactionEntity);
            }
            return ReturnErrorResponse(errorResponseModel);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    /// <summary>
    /// To delete Qualification 
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    [HttpPost]
    [Route("DeleteAllopathicDrug")]
    [ProducesResponseType(typeof(AllopathicDrugModel), 200)]
    [ProducesResponseType(typeof(string), 404)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 500)]
    public IActionResult DeleteAllopathicDrug(AllopathicDrugModel allopathicDrugModel)
    {
        ErrorResponseModel errorResponseModel = null;
        try
        {
            var adverseReactionEntry = _allopathicDrugService.DeleteAllopathicDrug(allopathicDrugModel, ref errorResponseModel);

            if (adverseReactionEntry != null)
            {
                return Ok(allopathicDrugModel);
            }
            return ReturnErrorResponse(errorResponseModel);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

        /// <summary>
        /// To get qualification by Qualification ID 
        /// </summary>
        /// <param name="allopathicDrugName"></param>
        /// <returns></returns>
        [HttpGet("GetAllopathicDrugByName/{allopathicDrugName}")]
        [ProducesResponseType(typeof(AllopathicDrugModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAllopathicDrugByName(string allopathicDrugName)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var allopathicDrugModel = _allopathicDrugService.GetAllopathicDrugByName(allopathicDrugName, ref errorResponseModel);

                if (allopathicDrugModel != null)
                {
                    return Ok(allopathicDrugModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        /// <summary>
        /// To delete diagnosis 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllopathicDrugfordropdown")]
        [ProducesResponseType(typeof(AllopathicDrugDDModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAllopathicDrugDDL()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var diagnosisrubric = _allopathicDrugService.GetAllopathicDrugDDL();

                if (diagnosisrubric != null)
                {
                    return Ok(diagnosisrubric);
                }
                else
                {
                    errorResponseModel.Message = "Not data found";
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// To get allopathic drug by allopathicDrugId 
        /// </summary>
        /// <param name="allopathicDrugId"></param>
        /// <returns></returns>
        [HttpGet("GetAllopathicDrugById/{allopathicDrugId}")]
        [ProducesResponseType(typeof(AllopathicDrugModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetAllopathicDrugById(int allopathicDrugId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var allopathicDrugModel = _allopathicDrugService.GetAllopathicDrugByID(allopathicDrugId, ref errorResponseModel);

                if (allopathicDrugModel != null)
                {
                    return Ok(allopathicDrugModel);
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
