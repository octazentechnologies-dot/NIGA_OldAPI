using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Xunit.Abstractions;

namespace Homeocentrum.Niga.OldAPI.Model
{
    public class PackageTopupModel
    {
        public int PackageTopupId { get; set; }
        public string PackageTopupName { get; set; }
        public int CaseCount { get; set; }
        public decimal TopupAmount { get; set; }
        public string EnteredBy { get; set; }
        public string ChangedBy { get; set; }
    }
}
