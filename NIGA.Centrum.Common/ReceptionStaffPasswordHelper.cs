using System;
using System.Net;
using System.Text;

namespace NIGA.Centrum.Common
{
    public static class ReceptionStaffPasswordHelper
    {
        public static string EncodePassword(string password)
        {
            var bytes = Encoding.ASCII.GetBytes(password);
            var base64 = Convert.ToBase64String(bytes);
            return WebUtility.UrlEncode(base64);
        }

        public static bool VerifyPassword(string plainPassword, string storedPassword)
        {
            var encodedPassword = EncodePassword(plainPassword);
            if (string.Equals(storedPassword, encodedPassword, StringComparison.Ordinal))
            {
                return true;
            }

            try
            {
                var decoded = DecodePassword(storedPassword);
                return string.Equals(decoded, plainPassword, StringComparison.Ordinal);
            }
            catch
            {
                return string.Equals(storedPassword, plainPassword, StringComparison.Ordinal);
            }
        }

        private static string DecodePassword(string storedPassword)
        {
            var urlDecoded = WebUtility.UrlDecode(storedPassword);
            var bytes = Convert.FromBase64String(urlDecoded);
            return Encoding.ASCII.GetString(bytes);
        }
    }
}
