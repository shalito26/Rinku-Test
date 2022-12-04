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
    public class rolesManager
    {
        public static Response<List<rolesModel>> Obtener(rolesModel value)
        {
            return RespuestaManager<List<rolesModel>, rolesModel>.Obtener(Repository.roles.Obtener, new SqlServer(), value);
        }
    }
}
