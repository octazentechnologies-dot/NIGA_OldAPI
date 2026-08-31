using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NIGA.Centrum.Common;
using NIGA.Centrum.Entity.DataModels;
using System.Linq;
using System.Threading.Tasks;
using NIGA.Centrum.Model;
using System;
using NIGA.Centrum.Business.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Niga_Domain.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly NIGACentrumContext _context;
        private readonly IConfiguration _configuration;

        public AccountController(
            ITokenService tokenService, 
            NIGACentrumContext context,
            IConfiguration configuration)
        {
            _tokenService = tokenService;
            _context = context;
            _configuration = configuration;
        }

        //[HttpPost("Login")]
        //public async Task<IActionResult> Login([FromBody] LoginModel model)
        //{
        //    try
        //    {
        //        // Validate input
        //        if (model == null)
        //        {
        //            return BadRequest(new { message = "Invalid request data" });
        //        }

        //        if (string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrWhiteSpace(model.Password))
        //        {
        //            return BadRequest(new { message = "Username and password are required" });
        //        }

        //        // Find user by username
        //        var userEntity = await _context.UserMaster
        //            .FirstOrDefaultAsync(x => x.UserName == model.UserName);

        //        if (userEntity == null)
        //        {
        //            return Unauthorized(new { message = "Invalid username or password" });
        //        }

        //        // Verify password (assuming passwords are stored as plain text for now - should be hashed in production)
        //        if (userEntity.UserPassword != model.Password)
        //        {
        //            return Unauthorized(new { message = "Invalid username or password" });
        //        }

        //        // Check if user is active (assuming there's an IsActive field)
        //        if (userEntity.IsUserActivated == false)
        //        {
        //            return Unauthorized(new { message = "Account is deactivated. Please contact administrator." });
        //        }

        //        // Get user role
        //        var roleEntity = await _context.RoleMaster
        //            .FirstOrDefaultAsync(x => x.RoleId == userEntity.RoleId);

        //        if (roleEntity == null)
        //        {
        //            return BadRequest(new { message = "User role not found" });
        //        }

        //        // Generate JWT token
        //        var token = await _tokenService.CreateToken(userEntity);

        //        // Create response model
        //        var userData = new AuthModel
        //        {
        //            IsSuperUser = roleEntity.RoleId == 1, // Assuming role 1 is super user
        //            UserId = userEntity.UserId,
        //            UserName = $"{userEntity.FirstName} {userEntity.LastName}".Trim(),
        //            Role = roleEntity.RoleName,
        //            RoleId = userEntity.RoleId,
        //            FirmIds = userEntity.FirmIds,
        //            Token = token
        //        };

        //        // Check subscription for doctors (role 3)
        //        if (roleEntity.RoleId == 3)
        //        {
        //            var userSubscription = await _context.PackageEntryDetails
        //                .FirstOrDefaultAsync(x =>
        //                    x.DoctorId == userEntity.UserId &&
        //                    x.IsActive == true);

        //            if (userSubscription != null)
        //            {
        //                var expiryDate = Convert.ToDateTime(userSubscription.ExpiryDate);
        //                var timeDifference = expiryDate - DateTime.UtcNow;

        //                int daysRemaining = (int)Math.Floor(timeDifference.TotalDays);

        //                if (daysRemaining > 0)
        //                {
        //                    userData.IsPlanActive = true;
        //                    userData.DaysRemaining = daysRemaining;

        //                    // Last 5 days warning
        //                    userData.IslastFiveDays = daysRemaining <= 5;
        //                }
        //                else
        //                {
        //                    // Expired (0 or negative days)
        //                    userData.IsPlanActive = false;
        //                    userData.IslastFiveDays = false;
        //                    userData.DaysRemaining = 0;
        //                }
        //            }
        //            else
        //            {
        //                userData.IsPlanActive = false;
        //                userData.IslastFiveDays = false;
        //                userData.DaysRemaining = 0;
        //            }
        //        }


        //        return Ok(new { 
        //            success = true, 
        //            message = "Login successful", 
        //            data = userData 
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception (you should implement proper logging)
        //        return StatusCode(500, new { 
        //            success = false, 
        //            message = "An error occurred during login. Please try again." 
        //        });
        //    }
        //}

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                // 1. Validate input
                if (model == null)
                    return BadRequest(new { message = "Invalid request data" });

                if (string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrWhiteSpace(model.Password))
                    return BadRequest(new { message = "Username and password are required" });

                // 2. Find user in UserMaster first
                var userEntity = await _context.UserMaster
                    .FirstOrDefaultAsync(x => x.UserName == model.UserName);

                if (userEntity != null)
                {
                    if (userEntity.UserPassword != model.Password)
                        return Unauthorized(new { message = "Invalid username or password" });

                    if ((bool)!userEntity.IsUserActivated)
                        return Unauthorized(new { message = "Account is deactivated. Please contact administrator." });

                    var roleEntity = await _context.RoleMaster
                        .FirstOrDefaultAsync(x => x.RoleId == userEntity.RoleId);

                    if (roleEntity == null)
                        return BadRequest(new { message = "User role not found" });

                    var token = await _tokenService.CreateToken(userEntity);

                    var userData = new AuthModel
                    {
                        IsSuperUser = roleEntity.RoleId == 1,
                        UserId = userEntity.UserId,
                        UserName = $"{userEntity.FirstName} {userEntity.LastName}".Trim(),
                        Role = roleEntity.RoleName,
                        RoleId = userEntity.RoleId,
                        FirmIds = userEntity.FirmIds,
                        Token = token,
                        IsPlanActive = false,
                        IslastFiveDays = false,
                        DaysRemaining = 0
                    };

                    if (roleEntity.RoleId == 3)
                    {
                        var doctorEntity = await _context.Doctor
                            .FirstOrDefaultAsync(d =>
                                d.UserId == userEntity.UserId &&
                                d.DeleteStatus == false);

                        if (doctorEntity != null)
                        {
                            userData.DoctorId = doctorEntity.DoctorId;

                            var userSubscription = await _context.PackageEntryDetails
                                .Where(p =>
                                    p.DoctorId == doctorEntity.DoctorId &&
                                    p.IsActive == true)
                                .OrderByDescending(p => p.ExpiryDate)
                                .FirstOrDefaultAsync();

                            if (userSubscription != null)
                            {
                                var expiryDate = Convert.ToDateTime(userSubscription.ExpiryDate);
                                int daysRemaining = (int)Math.Floor((expiryDate - DateTime.UtcNow).TotalDays);

                                if (daysRemaining > 0)
                                {
                                    userData.IsPlanActive = true;
                                    userData.DaysRemaining = daysRemaining;
                                    userData.IslastFiveDays = daysRemaining <= 5;
                                }
                            }
                        }
                    }

                    return Ok(new
                    {
                        success = true,
                        message = "Login successful",
                        data = userData
                    });
                }

                // 3. Reception staff (DoctorReceptionStaff.UserID)
                var receptionStaff = await _context.DoctorReceptionStaff
                    .FirstOrDefaultAsync(x =>
                        x.UserId == model.UserName &&
                        !x.DeleteStatus);

                if (receptionStaff == null || !ReceptionStaffPasswordHelper.VerifyPassword(model.Password, receptionStaff.Password))
                    return Unauthorized(new { message = "Invalid username or password" });

                var receptionDoctor = await _context.Doctor
                    .FirstOrDefaultAsync(d =>
                        d.DoctorId == receptionStaff.DoctorId &&
                        !d.DeleteStatus);

                var receptionRole = await _context.RoleMaster
                    .FirstOrDefaultAsync(r =>
                        r.RoleName == "Reception" &&
                        !r.DeleteStatus);

                if (receptionRole == null)
                    return BadRequest(new { message = "Reception role not found" });

                var (firstName, lastName) = SplitFullName(receptionStaff.FullName);
                var doctorUserId = receptionDoctor?.UserId;

                var receptionToken = await _tokenService.CreateReceptionStaffToken(
                    receptionStaff.ReceptionStaffId,
                    receptionStaff.UserId,
                    receptionStaff.DoctorId,
                    receptionStaff.FullName,
                    receptionRole.RoleName,
                    receptionRole.RoleId,
                    doctorUserId);

                var receptionData = new AuthModel
                {
                    UserId = doctorUserId ?? receptionStaff.ReceptionStaffId,
                    UserName = receptionStaff.FullName,
                    FirstName = firstName,
                    LastName = lastName,
                    Role = receptionRole.RoleName,
                    RoleId = receptionRole.RoleId,
                    FirmIds = receptionRole.FirmIds ?? string.Empty,
                    IsSuperUser = false,
                    DoctorId = receptionStaff.DoctorId,
                    ReceptionStaffId = receptionStaff.ReceptionStaffId,
                    Token = receptionToken,
                    IsPlanActive = false,
                    IslastFiveDays = false,
                    DaysRemaining = 0
                };

                var doctorSubscription = await _context.PackageEntryDetails
                    .Where(p =>
                        p.DoctorId == receptionStaff.DoctorId &&
                        p.IsActive == true)
                    .OrderByDescending(p => p.ExpiryDate)
                    .FirstOrDefaultAsync();

                if (doctorSubscription != null)
                {
                    var expiryDate = Convert.ToDateTime(doctorSubscription.ExpiryDate);
                    int daysRemaining = (int)Math.Floor((expiryDate - DateTime.UtcNow).TotalDays);

                    if (daysRemaining > 0)
                    {
                        receptionData.IsPlanActive = true;
                        receptionData.DaysRemaining = daysRemaining;
                        receptionData.IslastFiveDays = daysRemaining <= 5;
                    }
                }

                return Ok(new
                {
                    success = true,
                    message = "Login successful",
                    data = receptionData
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred during login. Please try again."
                });
            }
        }


        private static (string FirstName, string LastName) SplitFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return (null, null);

            var parts = fullName.Trim().Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0)
                return (null, null);
            if (parts.Length == 1)
                return (parts[0], null);
            return (parts[0], parts[1]);
        }

        /// <summary>
        /// Helper method to hash passwords (for future use when implementing password hashing)
        /// </summary>
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }



    }
}
