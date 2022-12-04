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
    public class Movimiento
    {
        public static List<MovimientoModel> Obtener(CoreContext db, MovimientoModel value)
        {
            return db.GenericSPselect<MovimientoModel>("dbo.SP_GetMovimientos", new DataParameter[]
                                          {
                                            new DataParameter("Id", value.Id)
                                          }).ToList();
        }



        public static Mensaje Guardar(CoreContext db, MovimientoModel value)
        {
            try
            {
                                 return db.GenericNonQuerySP("dbo.SP_UpInsMovimientos",
                       new DataParameter[]
                       {
                                new DataParameter("Id", value.Id),
                                new DataParameter("IdUser", value.IdUser),
                                new DataParameter("IdMes", value.IdMes),
                                new DataParameter("Entregas", value.Entregas)

                       });
            
            }
            catch(Exception ex)
            {
                return null;
            }
    }



        public static Mensaje Eliminar(CoreContext db, MovimientoModel value)
        {
            return db.GenericNonQuerySP("dbo.SP_DelMovimientos",
                new DataParameter[]
                {
                new DataParameter("Id", value.Id)
                });
        }

    }
}
