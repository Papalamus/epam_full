using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DataObjects.Attributes;
using DataObjects.DataBase.PersonConnecters;

namespace Test_project.DataBase.PersonConnecters
{
    class MyOrmBase
    {
        private AdoHelper adoHelper;
        private  Dictionary<Type,MappedType> MappedTypes { get; set; }
        private DbCommandMaker commandMaker = new DbCommandMaker();

        public void DeleteObject(Type type, object ID)
        {
            MappedType mt;
            MappedTypes.TryGetValue(type, out mt);
            CustomizeCommandHandler deleteQuery = commandMaker.DeleteCommand(mt, ID);
            adoHelper.ExequteNonQuery(deleteQuery);
        }

        internal class MappedType
        {
            public Dictionary<string, MemberInfo> MappedMembers { get; private set; }
        
            private string tableName;
            private string idTableField;

            internal string TableName
            {
                get { return tableName; }
            }

            internal string IdTableField
            {
                get { return idTableField; }
            }

            public MappedType(Type type)
            {
                TableOrmSaveAttribute tableOrmInstance = type.GetCustomAttribute<TableOrmSaveAttribute>();
                if (tableOrmInstance == null)
                    return;
                tableName = tableOrmInstance.Name;
                MappedMembers = new Dictionary<string, MemberInfo>();
                MapMembers(type.GetFields());
                MapMembers(type.GetProperties());
            }
            private void MapMembers(IEnumerable<MemberInfo> members)
            {
                foreach (MemberInfo memberInfo in members)
                {
                    FieldOrmSaveAttribute attr = memberInfo.GetCustomAttribute<FieldOrmSaveAttribute>();
                    if (attr != null)
                    {
                        MappedMembers.Add(attr.Name, memberInfo);
                    }
                    IdFieldOrmSave id = memberInfo.GetCustomAttribute<IdFieldOrmSave>();
                    if (id != null)
                    {
                        idTableField = id.Name;
                    }
                }
            }
            
        }


        public MyOrmConnecter<T> GetConnecter<T>() where T : class, new ()
        {
            //TODO Доделать нормальный возврат коннектора
            return new MyOrmConnecter<T>();
        }

    }
}
