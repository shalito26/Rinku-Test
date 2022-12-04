using Proof.Core;
using LinqToDB.Data;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilerias;

namespace Models.Repository
{
    public class Users
    {
        public static List<userModels> Obtener(CoreContext db, userModels value)
        {
            return db.GenericSPselect<userModels>("dbo.SP_GetUser", new DataParameter[]
                                          {
                                            new DataParameter("Id", value.Id)
                                          }).ToList();
        }



        public static Mensaje Guardar(CoreContext db, userModels value)
        {
            return db.GenericNonQuerySP("dbo.SP_UpInsUser",
                new DataParameter[]
                { 
                    new DataParameter("Id", value.Id),
                    new DataParameter("Nombres", value.Nombres),
                    new DataParameter("Apellidos", value.Apellidos),
                    new DataParameter("Telefono", value.Telefono),
                    new DataParameter("Correo", value.Correo),
                    new DataParameter("Contrasena", value.password),
                    new DataParameter("IdRol", value.Roles)
                });
        }


        public static Mensaje Eliminar(CoreContext db, userModels value)
        {
            return db.GenericNonQuerySP("dbo.SP_DelUser",
                new DataParameter[]
                {   
                    new DataParameter("Id", value.Id)
                });
        }

    }
}
