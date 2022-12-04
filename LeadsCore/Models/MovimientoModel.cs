using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class MovimientoModel : SeguridadSolicitud
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string NombreCompleto { get; set; }
        public int IdMes { get; set; }
        public string NombreMes { get; set; }
        public decimal Entregas { get; set; }
        public decimal Salario { get; set; }
        public decimal Bono { get; set; }
        public decimal Vales { get; set; }
        public decimal Retencion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }

        public int IdRol { get; set; }
        public string pEstatus { get; set; }
        public string pMensaje { get; set; }

    }
}
