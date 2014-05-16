using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataObjects.Attributes;
using DataObjects.DataBase.PersonConnecters;
using DataObjects.Entities;

namespace Test_project.DataBase.PersonConnecters
{
    public class MyOrmBase
    {
        private AdoHelper _adoHelper;
        private Dictionary<Type, DbCommandMaker> ConnectionMakers { get; set; }
        internal DbSchema Schema { get; set; }

        public void Test()
        {
            //GetArrayValue(typeof(Person),typeof(Article),5);
        }

        public void Delete(Type type, object ID)
        {

            DbCommandMaker cm = GetConnectorMaker(type);
            CustomizeCommandHandler deleteQuery = cm.DeleteCommand(ID);
            _adoHelper.ExequteNonQuery(deleteQuery);
        }

        public object Get(Type type, object ID)
        {
            object loadObject = Activator.CreateInstance(type); 
            DbCommandMaker cm = GetConnectorMaker(type);
            CustomizeCommandHandler selectQuery = cm.SelectCommand(ID);
            if (!_adoHelper.ExequteQuery(selectQuery, reader => cm.LoadInstance(loadObject, reader)))
            {
                loadObject = null;
            }
            //CustomizeCommandHandler[] selectRelated = cm;

            return loadObject;
        }

        public List<object> GetAll(Type type)
        {
            List<object> result = new List<object>();
            DbCommandMaker cm = GetConnectorMaker(type);
            CustomizeCommandHandler selectQuery = cm.SelectCommand();
            object loadObject;
            _adoHelper.ExequteQuery(selectQuery,
            reader =>
            {
                loadObject = Activator.CreateInstance(type);
                //cm.LoadInstance(loadObject, reader);
                result.Add(loadObject);
            });
            return result;
        }
       
        public bool Insert(Type type, object value)
        {
            DbCommandMaker cm = GetConnectorMaker(type);
            List<CustomizeCommandHandler> insertCommands = cm.InsertCommand(value);
            CheckExistence();
            foreach (var command in insertCommands)
            {
                _adoHelper.ExequteNonQuery(command);
            }
            return true;
        }

        public MyOrmBase(params Type[] types)
        {
            _adoHelper = new AdoHelper();
            ConnectionMakers = new Dictionary<Type, DbCommandMaker>();
            Schema = new DbSchema();
            foreach (var type in types)
            {
                ConnectionMakers.Add(type,new DbCommandMaker(type,this));
            }
        }

        internal DbCommandMaker GetConnectorMaker(Type type)
        {
            DbCommandMaker cm;
            //Todo Проверить что будет если не тип ранее не был размаплен
            if (!ConnectionMakers.TryGetValue(type, out cm))
            {
                cm = new DbCommandMaker(type,this);
                ConnectionMakers.Add(type, cm);
            }
            return cm;
        }




        //public MyOrmConnecter<T> GetConnecter<T>() where T : class, new ()
        //{
        //    //TODO Доделать нормальный возврат коннектора
        //    return new MyOrmConnecter<T>();
        //} 

        //TODO make internal
        public void LoadArray(string query,ProcessReaderHandler readArray)
        {
            _adoHelper.ExequteQuery(query, readArray);
        }



        //Todo private void
        public void CheckExistence()
        {
            Dictionary<string, string> requiredTables = Schema.RequiredTables;
            foreach (KeyValuePair<string, string> keyValuePair in requiredTables)
            {
                string tableName = keyValuePair.Key;
                string tableCreate = keyValuePair.Value;
                //Проверка существования таблицы
                string query = Schema.CheckString(tableName);
                bool exist = false;
                _adoHelper.ExequteQuery(query, reader => exist = (reader[0].ToString() != "0"));

                if (!exist)
                {
                    //Создаем таблицу 
                    _adoHelper.ExequteNonQuery(tableCreate);
                }
            }
        }


    }
}
