using Proof.Core;
using Models.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Utilerias;


namespace Models.Manager
{
    public class UsersManager
    {

        public enum CanalesId
        {
            Facebook = 103,
            Google = 2
        }

        public static Response<List<userModels>> Obtener(userModels value)
        {
            return RespuestaManager<List<userModels>, userModels>.Obtener(Repository.Users.Obtener, new SqlServer(), value);
        }

        public static Response<List<userModels>> ObtenerById(userModels value)
        {
            return RespuestaManager<List<userModels>, userModels>.Obtener(Repository.Users.Obtener, new SqlServer(), value);
        }

        public static Response Guardar(userModels value)
        {
            Mensaje guardar(CoreContext db, userModels value)
            {
                Mensaje respuesta = new Mensaje();
                string Id = string.Empty;

                respuesta = Repository.Users.Guardar(db, value);
                if (respuesta.Estatus == Estatus.Exito)
                {
                    Id = respuesta.Id;

                    if (respuesta.Estatus == Estatus.Exito)
                    {
                        respuesta.Id = Id;
                    }
                }
                return respuesta;
            }

            return RespuestaManager<userModels>.Guardar(guardar, new SqlServer(), value);
        }

        public static Response Delete(userModels value)
        {
            Mensaje guardar(CoreContext db, userModels value)
            {
                Mensaje respuesta = new Mensaje();
                string Id = string.Empty;

                respuesta = Repository.Users.Eliminar(db, value);
                if (respuesta.Estatus == Estatus.Exito)
                {
                    Id = respuesta.Id;

                    if (respuesta.Estatus == Estatus.Exito)
                    {
                        respuesta.Id = Id;
                    }
                }
                return respuesta;
            }

            return RespuestaManager<userModels>.Guardar(guardar, new SqlServer(), value);
        }

    }
}
