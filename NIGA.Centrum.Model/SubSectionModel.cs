using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NIGA.Centrum.Model
{
    public class SubSectionModel
    {
        public SubSectionModel()
        {
            this.Referencerubric = new List<ReferenceRubricDetailsModel>();

            this.SubSectionLanguageDetails = new List<SubSectionLanguageDetailsModel>();
        }
        public int SubSectionId { get; set; }
        public int? SectionId { get; set; }
        public string SectionName { get; set; }

        public int? ParentSubSectionId { get; set; }
        public bool? MainParentSubsection { get; set; }

        public string ParentSubSectionName { get; set; } = string.Empty;

        [Required(ErrorMessage = "SubSection Name is required")]
        public string SubSectionName { get; set; }
        
        public string SubSectionNameAlias { get; set; }
        public string Description { get; set; }
        public string EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }
        public List<ReferenceRubricDetailsModel> Referencerubric { get; set; }

        public List<SubSectionLanguageDetailsModel> SubSectionLanguageDetails { get; set; }
    }



    public class ReferenceRubricDetailsModel
    {
        public int ReferenceRubricId { get; set; }
        public int? SectionId { get; set; }
        public string SectionName { get; set; }
        public int? SubSectionId { get; set; }
        public int? RefSubSectionId { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool? DeleteStatus { get; set; }
        public string RefSubSectionName { get; set; }

    }
    public class SubSectionLanguageDetailsModel
    {
        public int SubSectionLanguageId { get; set; }
        public int SubSectionId { get; set; }
        public int LanguageId { get; set; }
        public string SubSectionDetails { get; set; }
        public string SectionName { get; set; }
        public string LanguageName { get; set; }
        public string LanguageDescription { get; set; }


    }
    public class SubSection
    {
        public int SubSectionId { get; set; }
        public int? SectionId { get; set; }
        public int? ParentSubSectionId { get; set; }
        [Required(ErrorMessage = "SubSection Name is required")]
        public string SubSectionName { get; set; }
        [Required(ErrorMessage = "SubSection Name Alias is required")]
        public string SubSectionNameAlias { get; set; }
        public string Description { get; set; }
        public int? EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }
        public int UserId { get; set; }
        public int RemedyId { get; set; }
        public string RemedyName { get; set; }
        public int? GradeId { get; set; }

        public double RemedyCount { get; set; }


    }

    public class SubSectionForPageModel
    {
        public int SubSectionId { get; set; }
        public string SubSectionName { get; set; }
    }

    public class SubSectionViewModel
    {
        public int SubSectionId { get; set; }
        public int? SectionId { get; set; }
        public string SectionName { get; set; }
        public int? ParentSubSectionId { get; set; }
        public string SubSectionName { get; set; }
        public string SubSectionNameAlias { get; set; }
        public string Description { get; set; }

        public bool? MainParentSubsection { get; set; }
        public string ParentSubSectionName { get; set; } 
    }

    public class SubSectionDDLModel
    {
        public int SubSectionId { get; set; }
        public string SubSectionName { get; set; }
        public bool? MainParentSubsection { get; set; }
    }


    public class SubSectionLevelModel
    {
        public long SubSectionId { get; set; }
        public string SubSectionName { get; set; }
        public int ChildCount { get; set; }
    }


    public class SubSectionSearchRequest
    {
        public string Query { get; set; }
        public int Top { get; set; } = 20;
    }

    public class SubSectionSearchResultModel
    {
        public long SubSectionId { get; set; }
        public string SubSectionName { get; set; }
        public long? ParentSubSectionId { get; set; }
        public int ChildCount { get; set; }
        public List<SubSectionLevelModel> Ancestors { get; set; } = new List<SubSectionLevelModel>();
    }

    public class SubSectionSearchPagedResultModel
    {
        public List<SubSectionSearchResultModel> Items { get; set; } = new List<SubSectionSearchResultModel>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool HasMore { get; set; }
    }

    


}
