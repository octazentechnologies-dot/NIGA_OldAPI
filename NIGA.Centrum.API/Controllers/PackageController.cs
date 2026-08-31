using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Model;

namespace NIGA.Centrum.API.Controllers
{
    /// <summary>
    /// APIs for package entity 
    /// </summary>
    [Route("api/package")]
    [ApiController]
  //  [Authorize]
    public class PackageController : BaseAPIController
    {
        IPackageService _packageService;

        /// <summary>
        /// Used to initialize controller and inject packge service
        /// </summary>
        /// <param name="packageService"></param>
        public PackageController(IPackageService packageService)
        {
            _packageService = packageService;
        }

        /// <summary>
        /// To get package by Package ID 
        /// </summary>
        /// <param name="packageId"></param>
        /// <returns></returns>
        [HttpGet("{packageId}")]
        [ProducesResponseType(typeof(PackageModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetGenderById(long packageId)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var packageModel = _packageService.GetPackageById(packageId, ref errorResponseModel);

                if (packageModel != null)
                {
                    return Ok(packageModel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To Get all Packages
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PackageModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetPackages()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var packageModelList = _packageService.GetPackages(ref errorResponseModel);

                if (packageModelList != null)
                {
                    return Ok(packageModelList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To add new Package 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(PackageModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult SavePackage(PackageModel packageModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var packagemodel = _packageService.SavePackage(packageModel, ref errorResponseModel);

                if (packagemodel != null)
                {
                    return Ok(packagemodel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To delete Package 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeletePackage")]
        [ProducesResponseType(typeof(PackageModel), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult DeletePackage(PackageModel packageModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var packagemodel = _packageService.DeletePackage(packageModel, ref errorResponseModel);

                if (packagemodel != null)
                {
                    return Ok(packagemodel);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To Get all Package topups
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPackageTopup")]
        [ProducesResponseType(typeof(List<PackageTopupModel>), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult GetPackageTopup()
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var packageModelList = _packageService.GetPackageTopup(ref errorResponseModel);

                if (packageModelList != null)
                {
                    return Ok(packageModelList);
                }
                return ReturnErrorResponse(errorResponseModel);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// To add new Package 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SavePackageTopup")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult SavePackageTopup(PackageTopupModel packageModel)
        {
            ErrorResponseModel errorResponseModel = null;
            try
            {
                var packagemodel = _packageService.SavePackageTopup(packageModel, ref errorResponseModel);

                if (packagemodel != null)
                {
                    return Ok(packagemodel);
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