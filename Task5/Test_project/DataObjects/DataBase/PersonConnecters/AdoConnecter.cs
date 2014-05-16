using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects.DataBase.Interface;
using DataObjects.Entities;

namespace DataObjects.DataBase.PersonConnecters
{
    public class AdoConnecter<T> : IPersonConnecter<T> where T:class,IAdoSaveable, new()
    {
        private string cnStr;
        private string tableName;

        private AdoHelper adoHelper;

        public AdoConnecter()
        {
            adoHelper = new AdoHelper();
            tableName = typeof(T).Name;
        }

        public List<T> GetAll()
        {
            List<T> result = new List<T>();
            T temp;

            ProcessReaderHandler readObject = delegate(DbDataReader reader)
            {
                temp = new T();
                temp.ReadObject(reader);
                result.Add(temp);
            };

            adoHelper.ExequteQuery(string.Format("Select * from {0}",tableName), readObject);
             
            return result;
        }

        


        public bool Insert(T p)
        {
            adoHelper.ExequteNonQuery(command => p.SaveObject(command,tableName));
            return true;
        }
        

        public T GetbyID(object ID)
        {
            string stringID = ID.ToString();
            T result =new T();
            var idField = ConfigurationManager.AppSettings[tableName];
            string statement = String.Format("Select * from {0} where {1}= @id", tableName, idField);

            CustomizeCommandHandler getSelectString = delegate(DbCommand command)
            {
                command.CommandText = statement;
                DbParameter param1 = command.CreateParameter();
                param1.ParameterName = "@id";
                param1.Value = stringID;
                command.Parameters.Add(param1);
            };
            if (!adoHelper.ExequteQuery(getSelectString, result.ReadObject))
            {
                result = null;
            }
            return result;
        
        }

        public void DeletebyID(object ID)
        {
            CustomizeCommandHandler getDeleteString = delegate(DbCommand command)
            {
                string stringID = ID.ToString();
                var idField = ConfigurationManager.AppSettings[tableName];
                string statement = string.Format("Delete from {0} where {1} = @Name", tableName, idField);
                command.CommandText = statement;

                DbParameter param1 = command.CreateParameter();
                param1.ParameterName = "@Name";
                param1.Value = stringID;

                command.Parameters.Add(param1);
            };
            adoHelper.ExequteNonQuery(getDeleteString);
        }
    }
}
