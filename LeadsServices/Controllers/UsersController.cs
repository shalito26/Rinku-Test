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
    public class UsersController : ControllerBase
    {
        private static Serilog.ILogger Log => Serilog.Log.ForContext<UsersController>();

        public UsersController()
        {

        }
        //Google Consume api nuestra

        [HttpGet]
        [Route("api/getAllUsers")]
        public Response<List<userModels>> GetAll()
        {
            userModels user = new userModels();
            Response<List<userModels>> respuesta = new Response<List<userModels>>();

            try
            {
                respuesta = RespuestaController<List<userModels>, userModels>.Obtener(UsersManager.Obtener, user, ControllerContext);
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
        [Route("api/getUsersbyId")]
        public Response<List<userModels>> GetById([FromBody] userModels user)
        {

            Response<List<userModels>> respuesta = new Response<List<userModels>>();

            try
            {
                respuesta = RespuestaController<List<userModels>, userModels>.Obtener(UsersManager.Obtener, user, ControllerContext);
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
        [Route("api/saveUpdateUsers")]
        public Response Guardar([FromBody] userModels value)
        {

            Response respuesta = new Response();

            try
            {


                respuesta = RespuestaController<Response, userModels>.Guardar(UsersManager.Guardar, value, ControllerContext);
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
        [Route("api/DeleteUsers/{id}")]
        public Response Eliminar([FromRoute] int id)
        {
            userModels value = new userModels();
            value.Id = id;

            Response respuesta = new Response();
            try
            {


                respuesta = RespuestaController<Response, userModels>.Guardar(UsersManager.Delete, value, ControllerContext);
                respuesta.Mensaje.Id = value.Id.ToString();
                respuesta.Mensaje.MensajeUsuario = "GrlMsgExitoEliminar";
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