using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class userModels : SeguridadSolicitud
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string password { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int Roles { get; set; }

        public string NombreRol { get; set; }
    }
}
