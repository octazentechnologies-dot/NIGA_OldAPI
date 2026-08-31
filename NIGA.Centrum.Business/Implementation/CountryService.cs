using NIGA.Centrum.Business.Interface;
using NIGA.Centrum.Entity.DataModels;
using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace NIGA.Centrum.Business.Implementation
{
    /// <summary>
    /// This is implementation  for the country operations 
    /// </summary>
   public class CountryService : ICountryService
    {
        NIGACentrumContext context;
        /// <summary>
        /// Creating constructor and injection dbContext
        /// </summary>
        /// <param name="centrumContext"></param>
        public CountryService(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }       

        /// <summary>
        /// Methood to get country by CountryId
        /// </summary>
        /// <param name="countryId"></param>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public CountryModel GetCountryById(long countryId, ref ErrorResponseModel errorResponseModel)
        {           
            errorResponseModel = new ErrorResponseModel();
            var countryEntity = context.CountryMaster.FirstOrDefault(x => x.CountryId == countryId && !x.DeleteStatus);
            if (countryEntity == null)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Country not found";
            }
            return new CountryModel
            {
                CountryId = countryEntity.CountryId,
                CountryName = countryEntity.CountryName,
                EnteredDate = countryEntity.EnteredDate,
                EnteredBy = countryEntity.EnteredBy,
                ChangedBy = countryEntity.ChangedBy,
                ChangedDate = countryEntity.ChangedDate,
                DeleteStatus = countryEntity.DeleteStatus,
            };
        }

        /// <summary>
        /// Method for getting all the countries
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        public List<CountryModel> GetCountries( ref ErrorResponseModel errorResponseModel)
        {
            errorResponseModel = new ErrorResponseModel();
            var countryModelList = new List<CountryModel>();
            var countryEntityList = context.CountryMaster.ToList();

            if (countryEntityList.Count==0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "Country not found";
            }
            countryEntityList.ForEach(item =>
            {
                countryModelList.Add(new CountryModel
                {
                    CountryId = item.CountryId,
                    CountryName = item.CountryName,
                    EnteredBy = item.EnteredBy,
                    EnteredDate=item.EnteredDate,
                    ChangedBy=item.ChangedBy,
                    ChangedDate=item.ChangedDate,
                    DeleteStatus=item.DeleteStatus
                });
            });
            return countryModelList;
        }
    }
}
