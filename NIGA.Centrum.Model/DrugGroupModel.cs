using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Model
{
    public class DrugGroupModel
    {
        public int DrugGroupId { get; set; }
        public int DrugSystemId { get; set; }
        public string DrugSystemName { get; set; }
        public string DrugGroupName { get; set; }
        public bool? DeleteStatus { get; set; }
    }
}
