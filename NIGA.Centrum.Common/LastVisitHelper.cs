using System;
using System.Collections.Generic;

namespace NIGA.Centrum.Common
{
    /// <summary>DOC-03.02 — parse classic PatientAppointment.AppointmentDate strings to a last-visit datetime.</summary>
    public static class LastVisitHelper
    {
        public static DateTime? MaxParsedDate(IEnumerable<string> dateStrings)
        {
            DateTime? max = null;
            if (dateStrings == null)
                return null;

            foreach (var s in dateStrings)
            {
                if (string.IsNullOrWhiteSpace(s))
                    continue;
                if (!DateTime.TryParse(s, out var d))
                    continue;
                if (!max.HasValue || d > max.Value)
                    max = d;
            }

            return max;
        }
    }
}
