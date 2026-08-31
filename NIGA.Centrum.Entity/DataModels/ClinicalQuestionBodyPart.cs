using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class ClinicalQuestionBodyPart
    {
        public int ClinicalQuestionBodyPartId { get; set; }
        public int? QuestionId { get; set; }
        public int? BodyPartId { get; set; }
        public bool? DeletedStatus { get; set; }
    }
}
