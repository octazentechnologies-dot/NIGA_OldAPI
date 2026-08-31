using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Entity.DataModels
{
    public partial class UserMaster
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public bool? UserStatus { get; set; }
        public string UserPhoto { get; set; }
        public string FirmIds { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string OldPassword { get; set; }
        public DateTime? PasswordRenewDate { get; set; }
        public string EnteredBy { get; set; }
        public DateTime? EnteredDate { get; set; }
        public string ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
        public bool DeleteStatus { get; set; }
        public bool? IsCurrentlyLoggedIn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public bool? IsUserActivated { get; set; }
        public int? RoleId { get; set; }
    }
}
