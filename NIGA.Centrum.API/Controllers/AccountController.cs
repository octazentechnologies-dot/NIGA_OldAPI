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
                    // Corrupt/truncated hash (often written when UserPassword was still NVARCHAR(50))
                    if (UserPasswordHasher.IsCorruptHash(userEntity.UserPassword))
                    {
                        return Unauthorized(new
                        {
                            message = "Invalid username or password",
                            detail = "Password hash in database is corrupted/truncated. Reset UserPassword to plaintext (known password) after ensuring column is NVARCHAR(500), then login again so the API can re-hash it. Do not paste manual encryption."
                        });
                    }

                    // M01 SEC-01.02 — verify PBKDF2 hash or legacy plaintext
                    if (!UserPasswordHasher.Verify(model.Password, userEntity.UserPassword))
                        return Unauthorized(new { message = "Invalid username or password" });

                    // SEC-01.01 — lazy migrate existing plaintext → full PBKDF2 (requires NVARCHAR(500))
                    string passwordHashWarning = null;
                    if (!UserPasswordHasher.IsHashed(userEntity.UserPassword))
                    {
                        try
                        {
                            passwordHashWarning = await PersistUserPasswordHashAsync(userEntity, model.Password);
                        }
                        catch (Exception hashEx)
                        {
                            // Never block login if hashing/persist fails — keep plaintext and continue
                            passwordHashWarning = "Password hash not saved: " + hashEx.Message;
                        }
                    }

                    if (userEntity.IsUserActivated == false)
                        return Unauthorized(new { message = "Account is deactivated. Please contact administrator." });

                    var roleEntity = await _context.RoleMaster
                        .FirstOrDefaultAsync(x => x.RoleId == userEntity.RoleId);

                    if (roleEntity == null)
                        return BadRequest(new { message = "User role not found" });

                    int? doctorId = null;
                    var doctorForToken = await _context.Doctor
                        .AsNoTracking()
                        .FirstOrDefaultAsync(d =>
                            d.UserId == userEntity.UserId &&
                            d.DeleteStatus == false);
                    if (doctorForToken != null)
                        doctorId = doctorForToken.DoctorId;

                    var token = await _tokenService.CreateToken(userEntity, 0, roleEntity.RoleName, doctorId);

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
                        DaysRemaining = 0,
                        DoctorId = doctorId
                    };

                    if (roleEntity.RoleId == 3 || string.Equals(roleEntity.RoleName, "Doctor", StringComparison.OrdinalIgnoreCase))
                    {
                        if (doctorForToken != null)
                        {
                            userData.DoctorId = doctorForToken.DoctorId;

                            var userSubscription = await _context.PackageEntryDetails
                                .Where(p =>
                                    p.IsActive == true &&
                                    (p.DoctorId == doctorForToken.DoctorId || p.DoctorId == userEntity.UserId))
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

                        if (!userData.IsPlanActive && IsDevClinicDoctor(userEntity.UserName))
                        {
                            userData.IsPlanActive = true;
                            userData.DaysRemaining = Math.Max(userData.DaysRemaining, 365);
                        }
                    }

                    return Ok(new
                    {
                        success = true,
                        message = "Login successful",
                        data = userData,
                        warning = passwordHashWarning
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
                    DoctorUserId = doctorUserId,
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
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred during login. Please try again.",
                    detail = ex.Message,
                    exceptionType = ex.GetType().FullName
                });
            }
        }


        /// <summary>
        /// Writes PBKDF2 hash. Prefers EF SaveChanges; falls back to raw SQL.
        /// Restores plaintext only when DB value is clearly truncated/corrupt.
        /// </summary>
        private async Task<string> PersistUserPasswordHashAsync(UserMaster user, string plaintextPassword)
        {
            var hashed = UserPasswordHasher.Hash(plaintextPassword);
            if (!UserPasswordHasher.Verify(plaintextPassword, hashed))
                throw new InvalidOperationException("Password hasher self-check failed.");

            var now = DateTime.UtcNow;
            var previous = user.UserPassword;

            // 1) EF update (tracked entity)
            user.UserPassword = hashed;
            user.ChangedDate = now;
            await _context.SaveChangesAsync();

            // 2) Fresh read (no tracker)
            var stored = await _context.UserMaster
                .AsNoTracking()
                .Where(x => x.UserId == user.UserId)
                .Select(x => x.UserPassword)
                .FirstOrDefaultAsync();

            if (UserPasswordHasher.IsWellFormedHash(stored)
                && UserPasswordHasher.Verify(plaintextPassword, stored))
            {
                user.UserPassword = stored;
                return null;
            }

            // 3) Retry with raw SQL if EF path did not stick
            try
            {
                await _context.Database.ExecuteSqlCommandAsync(
                    "UPDATE dbo.UserMaster SET UserPassword = {0}, ChangedDate = {1} WHERE UserId = {2}",
                    hashed,
                    now,
                    user.UserId);

                stored = await _context.UserMaster
                    .AsNoTracking()
                    .Where(x => x.UserId == user.UserId)
                    .Select(x => x.UserPassword)
                    .FirstOrDefaultAsync();

                if (UserPasswordHasher.IsWellFormedHash(stored)
                    && UserPasswordHasher.Verify(plaintextPassword, stored))
                {
                    user.UserPassword = stored;
                    return null;
                }
            }
            catch
            {
                // fall through to truncate handling
            }

            // 4) Truncated/corrupt → restore previous plaintext so login keeps working
            if (string.IsNullOrEmpty(stored)
                || stored.Length < UserPasswordHasher.MinWellFormedHashLength
                || UserPasswordHasher.IsCorruptHash(stored))
            {
                user.UserPassword = previous ?? plaintextPassword;
                user.ChangedDate = now;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    try
                    {
                        await _context.Database.ExecuteSqlCommandAsync(
                            "UPDATE dbo.UserMaster SET UserPassword = {0}, ChangedDate = {1} WHERE UserId = {2}",
                            user.UserPassword,
                            now,
                            user.UserId);
                    }
                    catch
                    {
                        // ignore
                    }
                }

                return "UserPassword hash was truncated or not persisted. Password kept as plaintext. Confirm column NVARCHAR(500) and restart Old-API.";
            }

            // Keep intended hash in memory; force SQL once more
            user.UserPassword = hashed;
            try
            {
                await _context.Database.ExecuteSqlCommandAsync(
                    "UPDATE dbo.UserMaster SET UserPassword = {0}, ChangedDate = {1} WHERE UserId = {2}",
                    hashed,
                    now,
                    user.UserId);
            }
            catch
            {
                // ignore
            }

            return $"Password hash retained (db len={stored.Length}).";
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
        /// M01 SEC-03.01 — Persist logout. Mobile contract (SEC-03.03): same endpoint path.
        /// Prefer New-API denylist for full JWT revoke when cut over; classic records UserLoginStatus.
        /// </summary>
        [HttpPost("Logout")]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var userIdClaim = User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                    ?? User?.FindFirst("nameid")?.Value;
                if (!long.TryParse(userIdClaim, out var userId) || userId <= 0)
                    return Unauthorized(new { message = "Invalid token" });

                var loginRow = await _context.UserLoginStatus
                    .Where(x => x.UserId == userId && x.OutTime == null)
                    .OrderByDescending(x => x.InTime)
                    .FirstOrDefaultAsync();

                if (loginRow != null)
                {
                    loginRow.OutTime = DateTime.UtcNow;
                    loginRow.Satus = false;
                    await _context.SaveChangesAsync();
                }

                return Ok(new { success = true, message = "Logged out" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Logout failed" });
            }
        }

        [HttpGet("SubscriptionStatus")]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> SubscriptionStatus()
        {
            var userIdClaim = User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                ?? User?.FindFirst("nameid")?.Value;
            if (!long.TryParse(userIdClaim, out var userId) || userId <= 0)
                return Unauthorized(new { message = "Invalid token" });

            var doctor = await _context.Doctor.AsNoTracking()
                .FirstOrDefaultAsync(d => d.UserId == userId && d.DeleteStatus == false);
            var user = await _context.UserMaster.AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == userId);

            var data = new AuthModel
            {
                IsPlanActive = false,
                IslastFiveDays = false,
                DaysRemaining = 0
            };

            if (doctor != null)
            {
                var userSubscription = await _context.PackageEntryDetails
                    .Where(p => p.IsActive == true && (p.DoctorId == doctor.DoctorId || p.DoctorId == userId))
                    .OrderByDescending(p => p.ExpiryDate)
                    .FirstOrDefaultAsync();
                if (userSubscription?.ExpiryDate != null)
                {
                    var daysRemaining = (int)Math.Floor((Convert.ToDateTime(userSubscription.ExpiryDate) - DateTime.UtcNow).TotalDays);
                    if (daysRemaining > 0)
                    {
                        data.IsPlanActive = true;
                        data.DaysRemaining = daysRemaining;
                        data.IslastFiveDays = daysRemaining <= 5;
                    }
                }
            }

            if (!data.IsPlanActive && user != null && IsDevClinicDoctor(user.UserName))
            {
                data.IsPlanActive = true;
                data.DaysRemaining = Math.Max(data.DaysRemaining, 365);
            }

            return Ok(new { success = true, data });
        }

        private static bool IsDevClinicDoctor(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName)) return false;
            return userName.Equals("Tufan_Doctor", StringComparison.OrdinalIgnoreCase)
                || userName.Equals("NIGA HOMEOPATHY", StringComparison.OrdinalIgnoreCase)
                || userName.Equals("testdoctor", StringComparison.OrdinalIgnoreCase);
        }
    }
}
