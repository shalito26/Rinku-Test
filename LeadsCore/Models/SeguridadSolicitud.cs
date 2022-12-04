using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class SeguridadSolicitud
    {
        public string Token { get; set; }
        public string Usuario { get; set; }
        public string Idioma { get; set; }
        public string IdiomaSAP { get; set; }
        public string Grupo { get; set; }
        public string Host { get; set; }

        public SeguridadSolicitud()
        {
            Token = string.Empty;
            Usuario = null;
            Idioma = string.Empty;
            IdiomaSAP = string.Empty;
            Grupo = string.Empty;
            Host = string.Empty;
        }
    }
}
