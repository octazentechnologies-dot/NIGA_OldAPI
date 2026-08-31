using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    /// <summary>
    /// Interface used for country related operations
    /// </summary>
   public interface ICountryService
    {
        /// <summary>
        /// Method is used for to get country by countryId
        /// </summary>
        /// <param name="countryId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        CountryModel GetCountryById(long countryId, ref ErrorResponseModel errorResponseModel);

        /// <summary>
        /// interface for getting all the countries
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<CountryModel> GetCountries(ref ErrorResponseModel errorResponseModel);

        ///// <summary>
        ///// Interface is used to save Country
        ///// </summary>
        ///// <param name="countryModel"></param>
        ///// <param name="errorResponseModel"></param>
        ///// <returns></returns>
        //string SaveCountry(CountryModel countryModel, ref ErrorResponseModel errorResponseModel);

        ///// <summary>
        ///// Interface is used to deactivate Country.
        ///// </summary>
        ///// <param name="countryModel"></param>
        ///// <param name="errorResponseModel"></param>
        ///// <returns></returns>
        //string DeleteCountry(CountryModel countryModel, ref ErrorResponseModel errorResponseModel);
    }
}
