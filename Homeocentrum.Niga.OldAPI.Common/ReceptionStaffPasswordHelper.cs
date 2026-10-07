using System;
using System.Linq;
using System.Net;
using System.Text;

namespace Homeocentrum.Niga.OldAPI.Common
{
    /// <summary>
    /// Reception staff passwords use the same PBKDF2 format as UserMaster (<see cref="UserPasswordHasher"/>).
    /// Rows written before that were URL-encoded base64 of the password (reversible), or plaintext; they still verify
    /// so they can be re-hashed on the next login.
    /// </summary>
    public static class ReceptionStaffPasswordHelper
    {
        public static string HashPassword(string plainPassword)
        {
            return UserPasswordHasher.Hash(plainPassword);
        }

        public static bool NeedsRehash(string storedPassword)
        {
            return !UserPasswordHasher.IsHashed(storedPassword);
        }

        public static bool VerifyPassword(string plainPassword, string storedPassword)
        {
            if (string.IsNullOrEmpty(plainPassword) || string.IsNullOrEmpty(storedPassword))
                return false;

            if (UserPasswordHasher.IsHashed(storedPassword))
                return UserPasswordHasher.Verify(plainPassword, storedPassword);

            if (FixedTimeEquals(storedPassword, LegacyEncode(plainPassword)))
                return true;
            return FixedTimeEquals(LegacyPlaintext(storedPassword), plainPassword);
        }

        /// <summary>Recovers the password from a legacy encoded value; values that do not decode to printable ASCII are treated as plaintext.</summary>
        public static string LegacyPlaintext(string storedPassword)
        {
            try
            {
                var bytes = Convert.FromBase64String(WebUtility.UrlDecode(storedPassword));
                if (bytes.Length > 0 && bytes.All(b => b >= 0x20 && b < 0x7F))
                    return Encoding.ASCII.GetString(bytes);
            }
            catch (FormatException)
            {
            }
            return storedPassword;
        }

        private static string LegacyEncode(string password)
        {
            return WebUtility.UrlEncode(Convert.ToBase64String(Encoding.ASCII.GetBytes(password)));
        }

        private static bool FixedTimeEquals(string a, string b)
        {
            var x = Encoding.UTF8.GetBytes(a);
            var y = Encoding.UTF8.GetBytes(b);
            var diff = x.Length ^ y.Length;
            for (var i = 0; i < Math.Min(x.Length, y.Length); i++)
                diff |= x[i] ^ y[i];
            return diff == 0;
        }
    }
}
