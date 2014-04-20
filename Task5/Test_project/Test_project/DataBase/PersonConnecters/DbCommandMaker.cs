using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects.DataBase.PersonConnecters;

namespace Test_project.DataBase.PersonConnecters
{
    class DbCommandMaker
    {
        public CustomizeCommandHandler DeleteCommand(MyOrmBase.MappedType mappedType,object ID)
        {
            string statement = MakeDeleteString(mappedType, "@IdValue");
            CustomizeCommandHandler deleteQuery = delegate(DbCommand command)
            {
                command.CommandText = statement;
                DbParameter param1 = command.CreateParameter();
                param1.ParameterName = "@IdValue";
                param1.Value = ID.ToString();
                command.Parameters.Add(param1);
            };

            return deleteQuery;
        }

        private string MakeDeleteString(MyOrmBase.MappedType mappedType, string parametrName)
        {
            if (mappedType.MappedMembers.Count < 0)
            {
                //TODO это наверное ошибка
                return string.Empty;
            }

            return string.Format("Delete from {0} where {1} = {2}", 
                mappedType.TableName, mappedType.IdTableField, parametrName);
        }
    }
}
