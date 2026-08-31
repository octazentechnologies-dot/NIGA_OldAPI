using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    public interface ILabTestMasterServices
    {
        /// <summary>
        /// Get All lab test 
        /// </summary>
        /// <param name="errorResponseModel"></param>
        /// <returns></returns>
        List<LabTestMasterModel> GetLabTests(ref ErrorResponseModel errorResponseModel);
    }
}
