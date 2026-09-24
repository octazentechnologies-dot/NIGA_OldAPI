using Homeocentrum.Niga.OldAPI.Business.Interface;
using Homeocentrum.Niga.OldAPI.Entity.DataModels;
using Homeocentrum.Niga.OldAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Homeocentrum.Niga.OldAPI.Business.Implementation
{
    public class LabTestMasterServices : ILabTestMasterServices
    {
        NIGACentrumContext context;
        /// <summary>
        /// Constructor
        /// </summary>
        public LabTestMasterServices(NIGACentrumContext centrumContext)
        {
            context = centrumContext;
        }

        public List<LabTestMasterModel> GetLabTests( ref ErrorResponseModel errorResponseModel)
        {
            var labTestMasterModelList = new List<LabTestMasterModel>();
            errorResponseModel = new ErrorResponseModel();
            var labTestMasterEntities = context.LabTestMaster.Where(x => x.DeleteStatus == false).ToList();

            if (labTestMasterEntities.Count == 0)
            {
                errorResponseModel.StatusCode = HttpStatusCode.NotFound;
                errorResponseModel.Message = "records not found";
            }
            labTestMasterEntities.ForEach(item =>
            {
                labTestMasterModelList.Add(new LabTestMasterModel
                {
                    TestId = item.TestId,
                    TestName = item.TestName,
                    TestAlias = item.TestAlias,

                });
            });
            return labTestMasterModelList;
        }
    }
}
