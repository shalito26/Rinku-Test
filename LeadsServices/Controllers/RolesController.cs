using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models.Manager;
using Models.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Utilerias;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProofServices.Controllers
{

    [ApiController]
    [EnableCors("AllowOrigin")]
    public class RolesController : ControllerBase
    {
        private static Serilog.ILogger Log => Serilog.Log.ForContext<UsersController>();

        [HttpGet]
        [Route("api/getAllRoles")]
        public Response<List<rolesModel>> GetAll()
        {
            rolesModel user = new rolesModel();
            Response<List<rolesModel>> respuesta = new Response<List<rolesModel>>();

            try
            {
                respuesta = RespuestaController<List<rolesModel>, rolesModel>.Obtener(rolesManager.Obtener, user, ControllerContext);
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

    }
}
