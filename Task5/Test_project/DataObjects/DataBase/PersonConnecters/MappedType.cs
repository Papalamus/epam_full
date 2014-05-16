using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataObjects.Attributes;

namespace Test_project.DataBase.PersonConnecters
{
    internal class MappedType : IEnumerable<KeyValuePair<string, PropertyInfo>>
    {
        public Dictionary<string, FieldInfo> Fields { get; private set; }
        public Dictionary<string, PropertyInfo> Properties { get; private set; }

        public Dictionary<string, MemberInfo> ArrayMembers { get; private set; }

        private Type _type;
        private string _tableName;
        private string _idTableField;
        public MemberInfo IdMember { get; set; }

        //Name witch use for link tables for this class id link
        private string _linkName;

        #region Propeties
        internal string LinkName
        {
            get { return _linkName; }
        }

        internal string TableName
        {
            get { return _tableName; }
        }

        internal string IdTableField
        {
            get { return _idTableField; }
        }
        

        internal MemberInfo this[string fieldName]
        {
            get
            {
                PropertyInfo result;
                Properties.TryGetValue(fieldName, out  result);
                return result;
            }
        }
        public int Count
        {
            get
            {
                return Properties.Count;
            }
        }

        IEnumerator<KeyValuePair<string, PropertyInfo>> IEnumerable<KeyValuePair<string, PropertyInfo>>.GetEnumerator()
        {
            return Properties.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Properties.GetEnumerator();
        }
        #endregion

        public MappedType(Type type)
        {
            TableOrmSaveAttribute tableOrmInstance = type.GetCustomAttribute<TableOrmSaveAttribute>();
            if (tableOrmInstance == null)
                return;

            Fields = new Dictionary<string, FieldInfo>();
            Properties = new Dictionary<string, PropertyInfo>();
            ArrayMembers = new Dictionary<string, MemberInfo>();
            _tableName = tableOrmInstance.Name;
            _linkName = tableOrmInstance.Name;
            _type = type;

            MapFields(type.GetFields());
            MapProperties(type.GetProperties());
        }

        private void MapProperties(PropertyInfo[] properties)
        {
            foreach (PropertyInfo propertyInfo in properties)
            {
                MemberMap(propertyInfo, Properties);
                IdMemberMap(propertyInfo, Properties);
            }
        }

        private void MapFields(FieldInfo[] fields)
        {
            foreach (FieldInfo fieldInfo in fields)
            {
                MemberMap(fieldInfo, Fields);
                IdMemberMap(fieldInfo, Fields);
            }
        }

        private void IdMemberMap<T>(T memberInfo, Dictionary<string, T> destination) where T : MemberInfo
        {
            IdFieldOrmSave id = memberInfo.GetCustomAttribute<IdFieldOrmSave>();
            if (id != null)
            {
                _idTableField = id.Name;
                destination.Add(id.Name, memberInfo);
                IdMember = memberInfo;
            }          
        }

        private void MemberMap<T>(T memberInfo, Dictionary<string, T> destination) where T: MemberInfo
        {
            FieldOrmSaveAttribute attr = memberInfo.GetCustomAttribute<FieldOrmSaveAttribute>();
            if (attr != null)
            {
                string name = attr.Name;
                if (memberInfo.GetReflectedType().IsArray)
                {
                    ArrayMembers.Add(name, memberInfo);
                }
                else
                {
                    destination.Add(name, memberInfo);
                }
            }
        }

        internal object GetIdValue(object obj)
        {
            if (obj.GetType()!=_type)
            {
                //TODO Ошибка странная
            }
            return IdMember.GetValue(obj);
        }

        
        
    }

}
