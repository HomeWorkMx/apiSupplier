using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using apiSupplier.Entities;
using System.ComponentModel.DataAnnotations;

namespace apiSupplier.Models
{
    public partial class MailFormatDto
    {
        [Required]
        public string mail { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string body { get; set; }
        [Required]
        public string subject { get; set; }

    }

}
