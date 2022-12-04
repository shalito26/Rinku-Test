using Proof.Core;
using Models.Models;
using System;
using System.Collections;
using Utilerias;

namespace Models.Manager
{
    public class RespuestaManager<T, U> where U : SeguridadSolicitud
    {
        internal static void ValidaLista(Response<T> respuesta)
        {
            ICollection col = respuesta.Datos as ICollection;
            if (respuesta.Datos == null || (respuesta.Datos is IList && col.Count <= 0))
            {
                respuesta.Mensaje.Estatus = Estatus.Advertencia;
                respuesta.Mensaje.MensajeUsuario = "GrlMsgAdvertenciaConsultaNoInformacion";
            }
            else
            {
                respuesta.Mensaje.Estatus = Estatus.Exito;
                respuesta.Mensaje.MensajeUsuario = "GrlMsgExitoConsulta";
            }
        }

        #region "Metodos para consultas a BASE DE DATOS"
        internal static Response<T> Obtener(Func<CoreContext, T> funcion, CoreContext conexion)
        {
            var respuesta = new Response<T>();

            using (var db = conexion)
            {
                try
                {
                    respuesta.Datos = funcion(db);
                    ValidaLista(respuesta);
                }
                catch (Exception ex)
                {
                    respuesta.Mensaje = new Mensaje { MensajeTecnico = ex.Message };
                }
            }
            return respuesta;
        }

        internal static Response<T> Obtener(Func<CoreContext, U, T> funcion, CoreContext conexion, U solicitud)
        {
            var respuesta = new Response<T>();

            using (var db = conexion)
            {
                try
                {
                    respuesta.Datos = funcion(db, solicitud);
                    ValidaLista(respuesta);
                }
                catch (Exception ex)
                {
                    respuesta.Mensaje = new Mensaje { MensajeTecnico = ex.Message };
                }
            }
            return respuesta;
        }

        #endregion

        #region "Metodos obtener para RFC´s de SAP"        

        internal static Response<T> ObtenerSAP(Func<U, T> funcion, U solicitud)
        {
            var respuesta = new Response<T>();

            try
            {
                respuesta.Datos = funcion(solicitud);
                ValidaLista(respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = new Mensaje { MensajeTecnico = ex.Message };
            }
            return respuesta;
        }

        #endregion
    }

    public class RespuestaManager<U> where U : SeguridadSolicitud
    {
        internal static Response Obtener(Func<CoreContext, U, Mensaje> funcion, CoreContext conexion, U solicitud)
        {
            var respuesta = new Response();

            using (var db = conexion)
            {
                try
                {
                    respuesta.Mensaje = funcion(db, solicitud);
                }
                catch (Exception ex)
                {
                    respuesta.Mensaje = new Mensaje { MensajeTecnico = ex.Message };
                }
            }
            return respuesta;
        }

        internal static Response Guardar(Func<CoreContext, U, Mensaje> funcion, CoreContext conexion, U solicitud)
        {
            var respuesta = new Response();

            using (var db = conexion)
            {
                try
                {
                    respuesta.Mensaje = funcion(db, solicitud);
                }
                catch (Exception ex)
                {
                    respuesta.Mensaje = new Mensaje { MensajeTecnico = ex.Message };
                }
                finally
                {
                    if (Estatus.Exito == respuesta.Mensaje.Estatus)
                    {
                        db.CommitTransaction();
                    }
                    else
                    {
                        db.RollbackTransaction();
                    }
                }
            }
            return respuesta;
        }

    }

    public class RespuestaManager
    {
        internal static Response Obtener(Func<CoreContext, Mensaje> funcion, CoreContext conexion)
        {
            var respuesta = new Response();

            using (var db = conexion)
            {
                try
                {
                    respuesta.Mensaje = funcion(db);
                }
                catch (Exception ex)
                {
                    respuesta.Mensaje = new Mensaje { MensajeTecnico = ex.Message };
                }
            }
            return respuesta;
        }
    }
}
