using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Xunit.Abstractions;

namespace NIGA.Centrum.Model
{
    public class MateriaMedicaDetailModel
    {
        public int MatriaMedicaDetailId { get; set; }
        [Required(ErrorMessage = "Please select MateriMedica")]
        public int MateriaMedicaId { get; set; }
        public string MateriaMedicaDetail1 { get; set; }
        public int? SeqNo { get; set; }

    }
}
