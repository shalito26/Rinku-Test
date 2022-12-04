using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Utilerias;

namespace Proof.Core
{
    public class CoreContext : DataConnection
    {
        public static string Provider = string.Empty;

        public CoreContext(string EnvironmentString, string connectionString) : base(EnvironmentString, connectionString)
        {
            Provider = EnvironmentString;
            this.CommandTimeout = 12000;
            this.BeginTransaction();
        }

        ~CoreContext()
        {
            this.Close();
        }

        public IEnumerable<T> GenericSPselect<T>(string procedure, DataParameter[] parameters = null)
        {
            List<DataParameter> parametros = new List<DataParameter>();

            if (parameters != null)
            {
                parametros.AddRange(parameters);
            }

            //if (Provider.Equals(ProviderName.Oracle))
            //{
            //    DataParameter cur = new DataParameter();
            //    cur.Name = "cur";
            //    cur.DataType = DataType.Cursor;
            //    cur.Direction = ParameterDirection.Output;
            //    parametros.Add(cur);
            //}

            return this.QueryProc<T>(procedure, parametros.ToArray()).ToList();
        }

        public Mensaje GenericNonQuerySP(string procedure, DataParameter[] parameters = null, bool returnID = false)
        {
            Mensaje message = new Mensaje();
            List<DataParameter> parametros = new List<DataParameter>();
            DataParameter pID = new DataParameter();

            if (returnID)
            {
                pID = new DataParameter { Name = "pID", Size = 250, Direction = ParameterDirection.InputOutput, DataType = DataType.VarChar };
            }

            DataParameter pEs = new DataParameter { Name = "pEstatus", Direction = ParameterDirection.Output, DataType = DataType.Int32 };

            DataParameter pMe = new DataParameter { Name = "pMensaje", Size = 1024, Direction = ParameterDirection.Output, DataType = DataType.VarChar };

            if (parameters != null && parameters.Length > 0)
                parametros.AddRange(parameters);
            if (returnID)
                parametros.Add(pID);
            parametros.Add(pMe);
            parametros.Add(pEs);

            var item = this.ExecuteProc(procedure, parametros.ToArray());
            var pEstatus = (Estatus)(pEs.Value.ToString() != "null" ? Convert.ToInt32(pEs.Value.ToString()) : 5);

            message = new Mensaje
            {
                Estatus = pEstatus,
                MensajeUsuario = Estatus.Error == pEstatus ? Error.MensajeError : pMe.Value.ToString(),
                MensajeTecnico = Estatus.Error == pEstatus ? pMe.Value.ToString() : string.Empty,
                Id = returnID ? pID.Value.ToString() : ""
            };

            return message;
        }

        public Mensaje GenericNonQuerySPID(string procedure, DataParameter[] parameters = null)
        {
            Mensaje message = new Mensaje();
            List<DataParameter> parametros = new List<DataParameter>();
            DataParameter pID = new DataParameter();

            pID = new DataParameter { Name = "pID", Size = 250, Direction = ParameterDirection.Output, DataType = DataType.VarChar };

            if (parameters != null && parameters.Length > 0)
                parametros.AddRange(parameters);

            parametros.Add(pID);

            var item = this.ExecuteProc(procedure, parametros.ToArray());
            message.Id = pID.Value.ToString();
            return message;
        }
    }

    public class SqlServer : CoreContext
    {
        public SqlServer() : base(ProviderName.SqlServer, ConfiguracionLocal.StringConnectionSqlServer) { }
    }

}
