using System;
using System.Collections.Generic;
using System.Linq;
using DataObjects.DataBase.Interface;
using NUnit.Framework;
using Test_project.DataBase.PersonConnecters;

namespace DataObjects.DataBase.PersonConnecters
{
    public class MyOrmConnecter<T>:IPersonConnecter<T>where T:class,new()
    {
        private MyOrmBase _classBase;

        public MyOrmConnecter(MyOrmBase classBase)
        {
            _classBase = classBase;
        }

        public List<T> GetAll()
        {
            List<T> result = (_classBase.GetAll(typeof(T))).ConvertAll<T>(ConvertTo);
            return result;
        }

        public T GetbyID(object ID)
        {
            object result = _classBase.Get(typeof (T), ID);
            return ConvertTo(result);
        }

        public void DeletebyID(object ID)
        {
            _classBase.Delete(typeof(T),ID);
        }

        public bool Insert(T p)
        {
           return _classBase.Insert(typeof (T), p);
        }

        public static T ConvertTo(object obj)
        {
            return (T)obj;
        }
      
        //Dictionary<string, MemberInfo> mappedType = new Dictionary<string, MemberInfo>();
        //private string tableName;
        //private string idTableField;


 
        //public T MakeInstance(DbDataReader reader)
        //{
        //    T result = Activator.CreateInstance<T>();
        //    foreach (KeyValuePair<string,MemberInfo> keyVal in mappedType)
        //    {
        //        FieldOrmSaveAttribute attribute = keyVal.Value.GetCustomAttribute<FieldOrmSaveAttribute>();
        //        keyVal.Value.SetValue(result, reader[keyVal.Key]);
        //    }
        //    return result;
        //}

        //_______________________________________________________-

        //public List<T> GetAll()
        //{
        //    List<T> result = new List<T>();

        //    adoHelper.ExequteQuery(MakeSelectString(string.Empty), 
        //        reader =>result.Add(MakeInstance(reader)));
        //    return result;
        //}

        //public T GetbyID(object ID)
        //{
        //    T result = Activator.CreateInstance<T>();

        //    string statement = MakeSelectString(string.Format("where {0} = @Name", idTableField ));

        //    CustomizeCommandHandler setParametrs = delegate(DbCommand command)
        //    {
        //        command.CommandText = statement;
        //        DbParameter param1 = command.CreateParameter();
        //        param1.ParameterName = "@Name";
        //        param1.Value = ID.ToString();
        //        command.Parameters.Add(param1);
        //    };

        //    if (!adoHelper.ExequteQuery(setParametrs, reader => result = MakeInstance(reader)))
        //    {
        //        result = null;
        //    }
        //    return result ;
        //}

        //private bool isTableExist()
        //{
        //    object result;

        //    adoHelper.ExequteQuery(command =>
        //    {
        //        command.CommandText = "SELECT 1 FROM Information_Schema.Tables WHERE TABLE_NAME = @tableName";
        //        DbParameter param1 = command.CreateParameter();
        //        param1.ParameterName = "@tableName";
        //        param1.Value = tableName;
        //        command.Parameters.Add(param1);
        //    },
        //        reader =>
        //        { result = reader[0]; });
        //    return true;
        //}




        
    }
}
