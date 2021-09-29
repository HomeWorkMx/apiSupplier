using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using apiSupplier.Entities;
namespace apiSupplier.Models
{
    public partial class ContratoDto
    {
        public int IdContrato { get; set; }
        public int IdProducto { get; set; }
        public int IdProveedor { get; set; }
        public int EstadoContrato { get; set; }


        public virtual List<TransaccionDto> TransaccionesDto { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }


    }

}
