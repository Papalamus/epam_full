using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using DataObjects.Attributes;
using DataObjects.DataBase.Interface;
using NUnit.Framework;
using Test_project.DataBase.PersonConnecters;

namespace DataObjects.DataBase.PersonConnecters
{
    public class MyOrmConnecter<T>:IPersonConnecter<T>where T:class,new()
    {
        private MyOrmBase classBase;
        
        private MyOrmBase.MappedType mappedType;
        //Dictionary<string, MemberInfo> mappedType = new Dictionary<string, MemberInfo>();
        //private string tableName;
        //private string idTableField;



        //public  MyOrmConnecter()
        //{
        //    MapClass();
        //    adoHelper = new AdoHelper();
        //}



        //public void MapClass()
        //{
        //    Type t = typeof(T);
        //    TableOrmSaveAttribute tableOrmInstance = t.GetCustomAttribute<TableOrmSaveAttribute>();

        //    if (tableOrmInstance == null)
        //        return;
        //    tableName = tableOrmInstance.Name;

        //    MapMembers(t.GetFields());
        //    MapMembers(t.GetProperties());
        //}

        //private void MapMembers(IEnumerable<MemberInfo> members)
        //{
        //    foreach (MemberInfo memberInfo in members)
        //    {
        //        FieldOrmSaveAttribute attr = memberInfo.GetCustomAttribute<FieldOrmSaveAttribute>();
        //        if (attr != null)
        //        {
        //            mappedType.Add(attr.Name, memberInfo);
        //        }
        //        IdFieldOrmSave id = memberInfo.GetCustomAttribute<IdFieldOrmSave>();
        //        if (id != null)
        //        {
        //            idTableField = id.Name;
        //        }
        //    }
        //}


        //_______________________________________________________-
        //public string MakeInsertString(object obj)
        //{
        //    if (mappedType.Count < 0)
        //    {
        //        return string.Empty;
        //    }

        //    var into = new StringBuilder();
        //    var values = new StringBuilder();

        //    foreach (KeyValuePair<string, MemberInfo> pair in mappedType)
        //    {
        //        into.Append(pair.Key + " ,");
        //        values.Append(string.Format("'{0}',",pair.Value.GetValue(obj)));
        //    }

        //    into.Remove(into.Length - 1, 1);
        //    values.Remove(values.Length - 1, 1);

        //    return string.Format("Insert into {0}({1}) Values ({2})",tableName, into, values);
        //}

        //public string MakeDeleteString(string whereSection)
        //{
        //    if (mappedType.Count < 0)
        //    {
        //        return string.Empty;
        //    }

        //    return string.Format("Delete from {0} " + whereSection, tableName);
        //}

        //public string MakeSelectString(string whereSection)
        //{
        //    if (mappedType.Count < 0)
        //    {
        //        return string.Empty;
        //    }

        //    return string.Format("Select * from {0} " + whereSection, tableName, whereSection);
        //}

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

        public void DeletebyID(object ID)
        {
           
        }

        //public bool Insert(T p)
        //{
        //    adoHelper.ExequteNonQuery(command =>
        //    {
        //        command.CommandText = MakeInsertString(p);
        //    });
        //    return true;
        //}

    }
}
