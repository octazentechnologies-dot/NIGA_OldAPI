using System;
using System.Collections.Generic;
using System.Text;

namespace NIGA.Centrum.Model
{
    public class PathologyModel
    {
        public int PathologyId { get; set; }
        public string PathologyName { get; set; }
        public string Description { get; set; }
        public bool? DeleteStatus { get; set; }
    }
}
