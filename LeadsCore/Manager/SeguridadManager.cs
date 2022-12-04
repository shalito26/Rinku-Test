using Proof.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Utilerias;

namespace Models.Manager
{
    public class SeguridadManager
    {
        public static string RegistrarLog(string token, string mensajeError)
        {
            using (var db = new SqlServer())
            {
                string idLog = string.Empty;

                try
                {
                    Token tkn = new Token();
                    if (!string.IsNullOrEmpty(token))
                    {
                        try
                        {
                            tkn = Token.DecryptKey(token);
                        }
                        catch
                        {
                            mensajeError += " El formato del token es incorrecto.";
                        }
                    }
                    //idLog = Seguridad.RegistrarLog(db, mensajeError, tkn.Usuario.ToUpper());
                }
                catch (Exception ex)
                {
                    string mensaje = ex.Message;
                }
                finally
                {
                    db.CommitTransaction();
                }
                return idLog;
            }
        }

        private static Mensaje ValidarSesion(string grupo, string cveUsuario, string pToken, string pControlador, string pPagina, string host, bool soloConsulta = true)
        {
            using (var db = new SqlServer())
            {
                Mensaje mensaje = new Mensaje();
                try
                {
                    //mensaje = Seguridad.ValidarSesion(db, grupo, cveUsuario, pToken, pControlador, pPagina, host, soloConsulta);
                }
                catch (Exception ex)
                {
                    mensaje = new Mensaje { MensajeTecnico = ex.Message };
                }
                finally
                {
                    db.CommitTransaction();
                }
                return mensaje;
            }
        }

        public static Response<bool> ValidarToken(string token, bool soloConsulta = true)
        {
            Response<bool> respuesta = new Response<bool>();
            string controlador = string.Empty;
            string pagina = string.Empty;
            string host = string.Empty;

            try
            {

                if (!string.IsNullOrEmpty(token))
                {
                    //Obtiene la pagina de donde se invoca el servicio
                    if (ConfiguracionLocal.Patch != null)
                    {
                        //Obtiene los segmentos que conforman a la URL
                        string[] segmentos = ConfiguracionLocal.Patch.ToString().Split("/");
                        host = segmentos[segmentos.Length - 3].Replace("/", "");
                        controlador = segmentos[segmentos.Length - 2].Replace("/", "");
                        pagina = segmentos[segmentos.Length - 1];
                    }
                    else
                    {
                        throw new Exception("Error al obtener el nombre de la pagina que consume el servicio.");
                    }

                    //Desencripta el token
                    Token tkn = Token.DecryptKey(token);

                    //Valida si tiene acceso al sistema con el token actual
                    respuesta.Mensaje = ValidarSesion(tkn.Grupo, tkn.Usuario, token, controlador, pagina, host, soloConsulta);
                }
                else
                {
                    throw new Exception("No se indico el token al invocar el servicio.");
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje.Estatus = Estatus.ErrorAutent;
                respuesta.Mensaje.MensajeUsuario = "GrlMsgErrorAutenticacion";
                respuesta.Mensaje.MensajeTecnico = ex.Message;
            }

            return respuesta;
        }

    }
}
