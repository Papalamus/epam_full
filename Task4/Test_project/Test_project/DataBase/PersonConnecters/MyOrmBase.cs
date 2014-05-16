using System;
using System.Collections;
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
            CustomizeCommandHandler deleteQuery = commandMaker.DeleteCommand(mt, ID);
            adoHelper.ExequteNonQuery(deleteQuery);
        }

        

        public bool Insert(Type type, object value)
        {
            MappedType mapeeType = getMappedType(type);
            CustomizeCommandHandler insertCommand = commandMaker.InsertCommand(mapeeType, value);
            adoHelper.ExequteNonQuery(insertCommand)
            //command =>
            //{
            //    command.CommandText = MakeInsertString(p);
            //});
            return true;
        }

        private MappedType getMappedType(Type type)
        {
            MappedType mt;
            //Todo Проверить что будет если не тип ранее не был размаплен
            MappedTypes.TryGetValue(type, out mt);
            return mt;
        }


        internal class MappedType : IEnumerable<KeyValuePair<string, MemberInfo>>
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

            public int Count
            {
                get
                {
                    return MappedMembers.Count;
                }
            }

            IEnumerator<KeyValuePair<string, MemberInfo>> IEnumerable<KeyValuePair<string, MemberInfo>>.GetEnumerator()
            {
                return MappedMembers.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return MappedMembers.GetEnumerator();
            }
        }


        public MyOrmConnecter<T> GetConnecter<T>() where T : class, new ()
        {
            //TODO Доделать нормальный возврат коннектора
            return new MyOrmConnecter<T>();
        }

    }
}
