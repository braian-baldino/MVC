using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Accountant.MVC.Models
{
    public class CommonType
    {
        public string Category { get; set; }
        [StringLength(40, ErrorMessage = "La descripcion no debe tener mas de 40 caracteres.")]
        public string Description { get; set; }
        public double Amount { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

    }
}
