using System;
using System.Collections.Generic;

namespace Homeocentrum.Niga.OldAPI.Entity.DataModels
{
    public partial class EnquiryDetails
    {
        public int EnquiryId { get; set; }
        public string EnquiryName { get; set; }
        public DateTime? EnquiryDate { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string EnquiryDetails1 { get; set; }
        public bool? EnquiryStatus { get; set; }
        public string TicketStatus { get; set; }
        public long? AssignedTo { get; set; }
    }
}
