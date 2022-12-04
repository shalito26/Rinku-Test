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
    public class MovimientoManager
    {

        public enum CanalesId
        {
            Facebook = 103,
            Google = 2
        }

        public static Response<List<MovimientoModel>> Obtener(MovimientoModel value)
        {
            return RespuestaManager<List<MovimientoModel>, MovimientoModel>.Obtener(Repository.Movimiento.Obtener, new SqlServer(), value);
        }


        public static Response Guardar(MovimientoModel value)
        {
            Mensaje guardar(CoreContext db, MovimientoModel value)
            {
                Mensaje respuesta = new Mensaje();
                string Id = string.Empty;

                respuesta = Repository.Movimiento.Guardar(db, value);
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

            return RespuestaManager<MovimientoModel>.Guardar(guardar, new SqlServer(), value);
        }

        public static Response Delete(MovimientoModel value)
        {
            Mensaje guardar(CoreContext db, MovimientoModel value)
            {
                Mensaje respuesta = new Mensaje();
                string Id = string.Empty;

                respuesta = Repository.Movimiento.Eliminar(db, value);
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

            return RespuestaManager<MovimientoModel>.Guardar(guardar, new SqlServer(), value);
        }


    }
}
