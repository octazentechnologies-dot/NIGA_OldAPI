using System;
using System.Globalization;

namespace NIGA.Centrum.Entity.DataModels
{
    /// <summary>
    /// PatientAppointment.AppointmentDate is datetime in SQL and string on the classic entity.
    /// </summary>
    internal static class AppointmentDateStore
    {
        public static DateTime? ToStore(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            if (DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var dt)
                || DateTime.TryParse(value, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out dt))
            {
                return dt.Date;
            }

            throw new FormatException("Invalid appointment date.");
        }

        public static string FromStore(DateTime? value)
        {
            return value.HasValue
                ? value.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                : null;
        }
    }
}
