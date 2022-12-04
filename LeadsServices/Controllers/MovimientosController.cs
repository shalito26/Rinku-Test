using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Manager;
using Models.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Utilerias;

namespace ProofServices.Controllers
{
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class MovimientosController : ControllerBase
    {
        private static Serilog.ILogger Log => Serilog.Log.ForContext<MovimientosController>();

        public MovimientosController()
        {

        }

        [HttpGet]
        [Route("api/getAllMovimientos")]
        public Response<List<MovimientoModel>> GetAll()
        {
            MovimientoModel mov = new MovimientoModel();
            Response<List<MovimientoModel>> respuesta = new Response<List<MovimientoModel>>();

            try
            {
                respuesta = RespuestaController<List<MovimientoModel>, MovimientoModel>.Obtener(MovimientoManager.Obtener, mov, ControllerContext);
                respuesta.Mensaje.MensajeTecnico = "Se ha consultado correctamente";
                respuesta.Mensaje.MensajeUsuario = "GrlMsgExitoSeleccion";
                Log.Here().Information(respuesta.Mensaje.MensajeTecnico + " JSON: " + JsonConvert.SerializeObject(respuesta, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));


            }
            catch (Exception ex)
            {
                respuesta.Mensaje.MensajeUsuario = "GrlMsgErrorSleccion";
                respuesta.Mensaje.MensajeTecnico = ex.Message.ToString();
                Log.Here().Fatal(ex.Message + " JSON: " + JsonConvert.SerializeObject(respuesta, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
            }
            return respuesta;
        }

        [HttpPost]
        [Route("api/saveUpdateMovimientos")]
        public Response Guardar([FromBody] MovimientoModel value)
        {

            Response respuesta = new Response();

            try
            {


                respuesta = RespuestaController<Response, MovimientoModel>.Guardar(MovimientoManager.Guardar, value, ControllerContext);
                if (respuesta.Mensaje.Estatus != Estatus.Error)
                {
                    respuesta.Mensaje.MensajeTecnico = "Se ha registrado exitosamente";
                    respuesta.Mensaje.MensajeUsuario = "GrlMsgExitoGuardar";
                }
                
                Log.Here().Information(respuesta.Mensaje.MensajeTecnico + " JSON: " + JsonConvert.SerializeObject(value, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));


            }
            catch (Exception ex)
            {
                respuesta.Mensaje.MensajeUsuario = "GrlMsgErrorGuardar";
                respuesta.Mensaje.MensajeTecnico = ex.Message.ToString();
                Log.Here().Fatal(ex.Message + " JSON: " + JsonConvert.SerializeObject(value, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
            }
            return respuesta;
        }


        [HttpDelete]
        [Route("api/DeleteMovimientos/{id}")]
        public Response Eliminar([FromRoute] int id)
        {
            MovimientoModel value = new MovimientoModel();
            value.Id = id;

            Response respuesta = new Response();
            try
            {


                respuesta = RespuestaController<Response, MovimientoModel>.Guardar(MovimientoManager.Delete, value, ControllerContext);
                respuesta.Mensaje.Id = value.Id.ToString();
                respuesta.Mensaje.MensajeUsuario = "GrlMsgErrorEliminar";
                respuesta.Mensaje.MensajeTecnico = "Se ha Eliminado exitosamente";
                Log.Here().Information(respuesta.Mensaje.MensajeTecnico + " JSON: " + JsonConvert.SerializeObject(value, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));


            }
            catch (Exception ex)
            {
                respuesta.Mensaje.MensajeUsuario = "GrlMsgErrorEliminar";
                respuesta.Mensaje.MensajeTecnico = ex.Message.ToString();
                Log.Here().Fatal(ex.Message + " JSON: " + JsonConvert.SerializeObject(value, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
            }
            return respuesta;
        }

    }
}