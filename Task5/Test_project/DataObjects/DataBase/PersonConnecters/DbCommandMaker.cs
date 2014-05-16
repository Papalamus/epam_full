using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataObjects.Attributes;
using DataObjects.DataBase.PersonConnecters;

namespace Test_project.DataBase.PersonConnecters
{
    class DbCommandMaker
    {
        private readonly MappedType _mappedType;
        private readonly MyOrmBase _ormBase;

        public DbCommandMaker(Type type, MyOrmBase ormBase)
        {
            _ormBase = ormBase;
            _mappedType = new MappedType(type);
        }

        public string LinkName 
        {
            get { return _mappedType.LinkName; }
        }

        private string TableName
        {
            get { return _mappedType.TableName; }
        }

        private object IdValue(object obj)
        {
            return _mappedType.GetIdValue(obj);
        }

        private string IdTableField
        {
            get { return _mappedType.IdTableField; }
        }


        public CustomizeCommandHandler SelectCommand(object ID)
        {
            string statement = MakeSelectString(string.Format("where {0} = @Name", _mappedType.IdTableField));

                CustomizeCommandHandler setParametrs = delegate(DbCommand command)
                {
                    command.CommandText = statement;
                    DbParameter param1 = command.CreateParameter();
                    param1.ParameterName = "@Name";
                    param1.Value = ID.ToString(); 
                    command.Parameters.Add(param1);
                };
                return setParametrs;
        }

        public CustomizeCommandHandler SelectCommand()
        {
            string statement = MakeSelectString(string.Empty);
            CustomizeCommandHandler setParametrs = delegate(DbCommand command)
            {
                command.CommandText = statement;
            };
            return setParametrs;
        }

        //TODO Это должно быть паблик?
        public void LoadInstance(object loadObject, DbDataReader reader)
        {
            foreach (KeyValuePair<string, PropertyInfo> keyVal in _mappedType.Properties)
            {
                var property = keyVal.Value;
                property.SetValue(loadObject, reader[keyVal.Key]);
            }
            foreach (KeyValuePair<string, FieldInfo> keyVal in _mappedType.Fields)
            {
                var field = keyVal.Value;
                field.SetValue(loadObject, reader[keyVal.Key]);
            }
            foreach (KeyValuePair<string, MemberInfo> keyVal in _mappedType.ArrayMembers)
            {
                var arrayMember = keyVal.Value;
                object array;
                ArrayLoader ar = new ArrayLoader(arrayMember, _ormBase);
                var query = ar.SelectQuery(IdValue(loadObject));
                _ormBase.LoadArray(query,ar.LoadArray);
                ar.EndLoad(loadObject);
            }
        }

        public CustomizeCommandHandler DeleteCommand(object ID)
        {
            
            string statement = MakeDeleteString("@IdValue");
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

        public List<CustomizeCommandHandler> InsertCommand(object value)
        {
           
            List<CustomizeCommandHandler> insertCommandList = new List<CustomizeCommandHandler>();
            if (value == null)
            {
                return insertCommandList;
            }
            _ormBase.Schema.Request(_mappedType);
            insertCommandList.Add((command) => command.CommandText = MakeInsertString(value));
            foreach (var member in _mappedType.ArrayMembers)
            {
                string masterLink = this.IdValue(value).ToString();
                object array = member.Value.GetValue(value);
                if (array == null)
                {
                    //TODO что-то сделать
                }
                IList arrayList = (IList)array;
                if (arrayList.Count==0)
                {
                    //Todo Массив длинны 0 решай что делать
                }
                
                Type t = arrayList[0].GetType();

                DbCommandMaker elementCommand = _ormBase.GetConnectorMaker(t);

                _ormBase.Schema.Request(elementCommand._mappedType);
                _ormBase.Schema.RequestLinkTable(_mappedType, elementCommand._mappedType);

                foreach (var obj in arrayList)
                {
                    //Команды для вставки элемента массива
                    List<CustomizeCommandHandler> elementInsert = elementCommand.InsertCommand(obj);
                    insertCommandList.AddRange(elementInsert);
                    //Команды для вставки в таблицу связей
                    string arrLink = elementCommand.IdValue(obj).ToString();
                    string relation = InsertRelation(_mappedType, elementCommand._mappedType, masterLink, arrLink);
                    insertCommandList.Add((command) => command.CommandText = relation);
                }
            }
            return insertCommandList ;
        }

    
        //________________________________________________________________

        private string MakeSelectString(string whereSection)
        {
        //    if (_mappedType.Count < 0)
        //    {
        //        //Todo это кажется ошибка
        //        return string.Empty;
        //    }

            return string.Format("Select * from {0} " + whereSection, _mappedType.TableName);
        }


        private string MakeInsertString( object obj)
        {
            //if (_mappedType.Count < 0)
            //{
            //    //Todo это кажется ошибка
            //    return string.Empty;
            //}

            

            var into = new StringBuilder();
            var values = new StringBuilder();

            foreach (KeyValuePair<string, FieldInfo> pair in _mappedType.Fields)
            {
                into.Append(pair.Key + " ,");
                values.Append(string.Format("'{0}',", pair.Value.GetValue(obj)));
            }
            foreach (KeyValuePair<string, PropertyInfo> pair in _mappedType.Properties)
            {
                into.Append(pair.Key + " ,");
                values.Append(string.Format("'{0}',", pair.Value.GetValue(obj)));
            }

            into.Remove(into.Length - 1, 1);
            values.Remove(values.Length - 1, 1);

            return string.Format("Insert into {0}({1}) Values ({2})", _mappedType.TableName, into, values);
        }

        private string MakeDeleteString(string parametrName)
        {
            //if (_mappedType.Count < 0)
            //{
            //    //TODO это наверное ошибка
            //    return string.Empty;
            //}

            return string.Format("Delete from {0} where {1} = {2}", 
                _mappedType.TableName, _mappedType.IdTableField, parametrName);
        }





        private string GetJoinString(string linkTableName)
        {
            string join = string.Format("{0}.{1} = {2}.{3}", TableName, IdTableField,linkTableName, _mappedType.LinkName); 
            
            string result = string.Format("INNER JOIN {0} ON {1}",TableName,join);
            return result;
        }

        public string SelectRelated(DbCommandMaker masterType, object fkValue)
        {
            //string linkTableName = GetLinkTableName(masterType);
            //string selectedItem = linkTableName + "." + _mappedType.LinkName;
            //string whereSection = string.Format("{0}.{1} = {2}", masterType.TableName, masterType.IdTableField, fkValue);
            //string statement = string.Format("SELECT {0} FROM {1} {2} {3} WHERE {4}",
            //    selectedItem, linkTableName, GetJoinString(linkTableName), masterType.GetJoinString(linkTableName),
            //    whereSection);
            //return statement;
            return null;
        }

        public  string InsertRelation(MappedType masterType,MappedType arrayType,string masterLink,string arrLink)
        {
            Random r = new Random();
            string table = _ormBase.Schema.GetLinkTableName(masterType, arrayType);
            string id = r.Next(32000).ToString();

            var intoSection = new StringBuilder();
            var valueSection = new StringBuilder();

            intoSection.Append(table);
            //intoSection.AppendFormat("({0},{1},{2})", "id", masterType.LinkName, arrayType.LinkName);

            //valueSection.AppendFormat("( {0},{1},{2} )", id, masterLink, arrLink);


            intoSection.AppendFormat("({1},{2})", "id", masterType.LinkName, arrayType.LinkName);

            valueSection.AppendFormat("({1},{2} )", id, masterLink, arrLink);

            
            string result = string.Format("Insert into {0} values {1}", intoSection, valueSection);
            return result;
        }                                                                                                         

        

        
    }
}
