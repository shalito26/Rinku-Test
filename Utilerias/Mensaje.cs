using System;
using System.Linq;

namespace Utilerias
{
    public enum Estatus : int
    {
        Error = -1,
        Exito = 1,
        Advertencia = 0,
        ErrorAutent = -2
    }

    public static class Error
    {
        public static string MensajeError = "GrlMsgError";
    }

    public class Mensaje
    {
        public Estatus Estatus { get; set; }
        public string MensajeUsuario { get; set; }
        public string MensajeTecnico { get; set; }
        public string Token { get; set; }
        public string IdLog { get; set; }
        public string Id { get; set; }

        public Mensaje()
        {
            Estatus = Estatus.Error;
            MensajeUsuario = Error.MensajeError;
            MensajeTecnico = string.Empty;
            Token = string.Empty;
            IdLog = string.Empty;
            Id = string.Empty;
        }
    }

    public class Response<T>
    {
        public Mensaje Mensaje { get; set; }
        public T Datos { get; set; }
        public string MensajeUsuario { get; set; }

        public Response()
        {
            Mensaje = new Mensaje();
            if (typeof(T).IsClass)
            {
                try
                {
                    var cons = typeof(T).GetConstructors().Any(c => c.GetParameters().Count() == 0);
                    if (cons)
                    {
                        Datos = (T)Activator.CreateInstance(typeof(T));
                    }
                }
                catch { }
            }
        }
    }

    public class Response
    {
        public Mensaje Mensaje { get; set; }

        public Response()
        {
            Mensaje = new Mensaje();
        }
    }
}