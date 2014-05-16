using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
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

        public CustomizeCommandHandler InsertCommand(MyOrmBase.MappedType mappedType, object value)
        {
            return (command) => command.CommandText = MakeInsertString(mappedType,value);
        }
        private string MakeInsertString(MyOrmBase.MappedType mappedType, object obj)
        {
            if (mappedType.Count < 0)
            {
                //Todo это кажется ошибка
                return string.Empty;
            }

            var into = new StringBuilder();
            var values = new StringBuilder();

            foreach (KeyValuePair<string, MemberInfo> pair in mappedType)
            {
                into.Append(pair.Key + " ,");
                values.Append(string.Format("'{0}',", pair.Value.GetValue(obj)));
            }

            into.Remove(into.Length - 1, 1);
            values.Remove(values.Length - 1, 1);

            return string.Format("Insert into {0}({1}) Values ({2})", mappedType.TableName, into, values);
        }

        private string MakeDeleteString(MyOrmBase.MappedType mappedType, string parametrName)
        {
            if (mappedType.Count < 0)
            {
                //TODO это наверное ошибка
                return string.Empty;
            }

            return string.Format("Delete from {0} where {1} = {2}", 
                mappedType.TableName, mappedType.IdTableField, parametrName);
        }
    }
}
