using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Model
{
  public class QuestionSubGroupModel
    {
        public QuestionSubGroupModel()
        {
            SectionIds = new List<int>();
            Sections = new List<SectionViewModel>();
        }

        public int QuestionSubgroupId { get; set; }
        public string QuestionSubGroupName { get; set; }
        public int? QuestionGroupId { get; set; }
        public string QuestionGroupName { get; set; }
        public string Description { get; set; }
        public bool? DeleteStatus { get; set; }
        public List<int> SectionIds { get; set; }
        public List<SectionViewModel> Sections { get; set; }
    }

    public class QuestionSubGroupModelDDL
    {
        public int QuestionSubgroupId { get; set; }
        public string QuestionSubgroup1 { get; set; }
        public List<int> SectionIds { get; set; } = new List<int>();
    }
}
