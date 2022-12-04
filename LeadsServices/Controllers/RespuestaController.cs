using Microsoft.AspNetCore.Mvc;
using Models.Manager;
using Models.Models;
using System;
using Utilerias;

namespace ProofServices.Controllers
{
    public class RespuestaController<T>
    {
        public static Response<T> ObtenerSinToken(Func<Response<T>> funcion, ControllerContext CTX)
        {
            var respuesta = new Response<T>();

            try
            {
                ConfiguracionLocal.Patch = CTX.HttpContext.Request.Headers["Referer"];
                respuesta = funcion();
            }
            catch (Exception ex)
            {
                respuesta.Mensaje.MensajeTecnico = ex.Message;
            }

            if (Estatus.ErrorAutent == respuesta.Mensaje.Estatus || Estatus.Error == respuesta.Mensaje.Estatus && respuesta.Mensaje.IdLog == string.Empty)
            {
                respuesta.Mensaje.IdLog = SeguridadManager.RegistrarLog("", respuesta.Mensaje.MensajeTecnico);
                respuesta.Mensaje.MensajeTecnico = string.Empty;
            }

            return respuesta;
        }

        public static Response ObtenerSinToken(Func<Response> funcion, ControllerContext CTX)
        {
            var respuesta = new Response();

            try
            {
                ConfiguracionLocal.Patch = CTX.HttpContext.Request.Headers["Referer"];
                respuesta = funcion();
            }
            catch (Exception ex)
            {
                respuesta.Mensaje.MensajeTecnico = ex.Message;
            }

            if (Estatus.ErrorAutent == respuesta.Mensaje.Estatus || Estatus.Error == respuesta.Mensaje.Estatus && respuesta.Mensaje.IdLog == string.Empty)
            {
                respuesta.Mensaje.IdLog = SeguridadManager.RegistrarLog("", respuesta.Mensaje.MensajeTecnico);
                respuesta.Mensaje.MensajeTecnico = string.Empty;
            }

            return respuesta;
        }
    }

    public class RespuestaController<T, U> where U : SeguridadSolicitud
    {
        public static Response<T> ObtenerSinToken(Func<U, Response<T>> funcion, U value, ControllerContext CTX)
        {
            var respuesta = new Response<T>();

            try
            {
                if (value != null)
                {
                    ConfiguracionLocal.Patch = CTX.HttpContext.Request.Headers["Referer"];
                    respuesta = funcion(value);
                }
                else
                {
                    throw new Exception("No se indico los parametros de entrada del servicio.");
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje.MensajeTecnico = ex.Message;
            }

            if (Estatus.ErrorAutent == respuesta.Mensaje.Estatus || Estatus.Error == respuesta.Mensaje.Estatus && respuesta.Mensaje.IdLog == string.Empty)
            {
                respuesta.Mensaje.IdLog = SeguridadManager.RegistrarLog("", respuesta.Mensaje.MensajeTecnico);
                respuesta.Mensaje.MensajeTecnico = string.Empty;
            }

            return respuesta;
        }
        public static Response<T> Obtener(Func<U, Response<T>> funcion, U value, ControllerContext CTX)
        {
            var respuesta = new Response<T>();

            try
            {
                if (value != null)
                {
                    ConfiguracionLocal.Patch = CTX.HttpContext.Request.Headers["Referer"];
                   // respuesta.Mensaje = SeguridadManager.ValidarToken(value.Token).Mensaje;

                    //if (Estatus.Exito == respuesta.Mensaje.Estatus)
                    //{
                        //Token token = Token.DecryptKey(value.Token);
                        //value.Usuario = token.Usuario;
                        //value.Idioma = token.Idioma;
                        //value.IdiomaSAP = token.IdiomaSAP;
                        //value.Grupo = token.Grupo;
                        respuesta = funcion(value);
                   // }
                }
                else
                {
                    throw new Exception("No se indico los parametros de entrada del servicio.");
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje.MensajeTecnico = ex.Message;
            }

            if (Estatus.ErrorAutent == respuesta.Mensaje.Estatus || Estatus.Error == respuesta.Mensaje.Estatus && respuesta.Mensaje.IdLog == string.Empty)
            {
                respuesta.Mensaje.IdLog = SeguridadManager.RegistrarLog(value != null ? value.Token : string.Empty, respuesta.Mensaje.MensajeTecnico);
                respuesta.Mensaje.MensajeTecnico = string.Empty;
            }

            return respuesta;
        }
        public static Response Obtener(Func<U, Response> funcion, U value, ControllerContext CTX)
        {
            var respuesta = new Response();

            try
            {
                if (value != null)
                {
                    ConfiguracionLocal.Patch = CTX.HttpContext.Request.Headers["Referer"];
                    respuesta.Mensaje = SeguridadManager.ValidarToken(value.Token).Mensaje;

                    if (Estatus.Exito == respuesta.Mensaje.Estatus)
                    {
                        Token token = Token.DecryptKey(value.Token);
                        value.Usuario = token.Usuario;
                        value.Idioma = token.Idioma;
                        value.IdiomaSAP = token.IdiomaSAP;
                        value.Grupo = token.Grupo;
                        respuesta = funcion(value);
                    }
                }
                else
                {
                    throw new Exception("No se indico los parametros de entrada del servicio.");
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje.MensajeTecnico = ex.Message;
            }

            if (Estatus.ErrorAutent == respuesta.Mensaje.Estatus || Estatus.Error == respuesta.Mensaje.Estatus && respuesta.Mensaje.IdLog == string.Empty)
            {
                respuesta.Mensaje.IdLog = SeguridadManager.RegistrarLog(value != null ? value.Token : string.Empty, respuesta.Mensaje.MensajeTecnico);
                respuesta.Mensaje.MensajeTecnico = string.Empty;
            }

            return respuesta;
        }
        public static Response Guardar(Func<U, Response> funcion, U value, ControllerContext CTX, bool soloConsulta = false)
        {
            var respuesta = new Response();

            try
            {
                if (value != null)
                {
                    ConfiguracionLocal.Patch = CTX.HttpContext.Request.Headers["Referer"];
                    respuesta = funcion(value);
                }
                else
                {
                    throw new Exception("No se indico los parametros de entrada del servicio.");
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje.MensajeTecnico = ex.Message;
            }

            if (Estatus.ErrorAutent == respuesta.Mensaje.Estatus || Estatus.Error == respuesta.Mensaje.Estatus && respuesta.Mensaje.IdLog == string.Empty)
            {
                respuesta.Mensaje.IdLog = SeguridadManager.RegistrarLog(value != null ? value.Token : string.Empty, respuesta.Mensaje.MensajeTecnico);
                respuesta.Mensaje.MensajeTecnico = string.Empty;
            }

            return respuesta;
        }
    }
}