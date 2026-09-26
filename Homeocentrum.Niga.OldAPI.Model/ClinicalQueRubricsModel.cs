using System;
using System.Collections.Generic;
using System.Text;

namespace Homeocentrum.Niga.OldAPI.Model
{
    public class ClinicalQueRubricsModel
    {
        public int ClinicalQueRubricId { get; set; }
        public int? QuestionsId { get; set; }
        public int? SubsectionId { get; set; }
        public int? IsDeleted { get; set; }

    }
}
