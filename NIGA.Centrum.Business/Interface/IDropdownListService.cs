using NIGA.Centrum.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Business.Interface
{
    public interface IDropdownListService
    {
        List<ThermalModel> GetAllThermalDDL();
        List<AuthorMasterModel> GetAuthorforMateriaMedica();
        List<PatientLabTestModel> GetPatientLabTestDDl();

        List<QuestionGroupModelDDL> GetQuestionGroupDDL(ref ErrorResponseModel errorResponseModel);

        List<QuestionSectionModelDDL> GetQuestionSectionsDDL(ref ErrorResponseModel errorResponseModel);

        List<QuestionSubGroupModelDDL> GetQuestionSubGroupDDL(ref ErrorResponseModel errorResponseModel);

        List<BodyPartDDLModel> GetBodyPartDDL(int sectionId, ref ErrorResponseModel errorResponseModel);

        List<QuestionSubGroupModelDDL> GetSubQuestionGroupByQGIDQSIDDDL(int questionGroupId, int questionSectionId, ref ErrorResponseModel errorResponseModel);

        List<SubSectionDDLModel> GetSubsectionBySection(long sectionId, ref ErrorResponseModel errorResponseModel);

       
    }
}
