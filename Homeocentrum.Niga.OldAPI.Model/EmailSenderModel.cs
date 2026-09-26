using System;
using System.Collections.Generic;
using System.Text;
namespace Homeocentrum.Niga.OldAPI.Model
{
    public class EmailSenderModel
    {
        public string ToAddress { get; set; }
        public string Body { get; set; }
        public bool isHtml { get; set; }
        public string Subject { get; set; }
        public bool sentStatus { get; set; }
        public string LastError { get; set; }
    }
}
