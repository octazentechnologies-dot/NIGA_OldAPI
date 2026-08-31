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
    //[Authorize]
    public class PatientLabTestController : BaseAPIController
    {
        IPatientLabTestService _patientLabTest;
        /// <summary>
        /// Used to initialize controller and inject author service
        /// </summary>
        /// <param name="authorService"></param>
        public PatientLabTestController(IPatientLabTestService patientLabTestService)
        {
            _patientLabTest = patientLabTestService;
        }

        /// <summary>
        /// To Get all Authors
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPatientLabTest")]
        [ProducesResponseType(typeof(PatientLabTestModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetPatientLabTest()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var LabTestModelList = _patientLabTest.GetPatientLabTests(ref errorResponseModel);

                if (LabTestModelList != null)
                {
                    return Ok(LabTestModelList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To add new Author 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddEditPatientLabTest")]
        [ProducesResponseType(typeof(PatientLabTestModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult AddEditPatientLabTest(PatientLabTestModel patientLabTestModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                //int userId = 0;
                //if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
                //{
                //    if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
                //    {
                //        userId = Convert.ToInt32(((System.Security.Claims.ClaimsIdentity)User.Identity).FindFirst(System.Security.Claims.ClaimTypes.Name).Value);
                //    }
                //}
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


                var Model = _patientLabTest.AddEditPatientLabTest(patientLabTestModel, userId, ref errorResponseModel);

                if (Model != null)
                {
                    return Ok(Model);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To get Author by authorID 
        /// </summary>
        /// <param name="pathologyId"></param>
        /// <returns></returns>
        [HttpGet("GetPatientLabTestById/{patientLabTestId}")]
        [ProducesResponseType(typeof(PatientLabTestModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetPatientLabTestById(int patientLabTestId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var rModel = _patientLabTest.GetPatientLabTestById(patientLabTestId, ref errorResponseModel);

                if (rModel != null)
                {
                    return Ok(rModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
       

        /// <summary>
        /// To delete author 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeletePatientLabTest/{patientLabTestId}")]
        [ProducesResponseType(typeof(PatientLabTestModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeletePatientLabTest(int patientLabTestId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var Model = _patientLabTest.DeletePatientLabTest(patientLabTestId, ref errorResponseModel);

                if (Model != null)
                {
                    return Ok(Model);
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
