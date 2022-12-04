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
    public class roles
    {
        public static List<rolesModel> Obtener(CoreContext db, rolesModel value)
        {
            return db.GenericSPselect<rolesModel>("dbo.SP_GetRoles", new DataParameter[]
                                          {
                                            new DataParameter("Id", value.Id)
                                          }).ToList();
        }
    }
}
