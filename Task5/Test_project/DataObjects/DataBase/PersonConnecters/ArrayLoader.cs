using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataObjects.DataBase.PersonConnecters;

namespace Test_project.DataBase.PersonConnecters
{
    class ArrayLoader
    {
        private MemberInfo _loadingArray;
        private DbSchema _schema;
        private Type _compositedType;
        private Type _masterType;
        private MyOrmBase _ormBase;
        private DbCommandMaker _maker;

        private IList _loadedObjects;
        



        public ArrayLoader(MemberInfo loadingArray, MyOrmBase ormBase)
        {
            _loadingArray = loadingArray;
            _ormBase = ormBase;
            _schema = _ormBase.Schema;
            _compositedType = _loadingArray.GetReflectedType().GetElementType();
            _masterType = _loadingArray.DeclaringType;
            _maker = _ormBase.GetConnectorMaker(_compositedType);

            
            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(_compositedType);
            _loadedObjects = (IList)Activator.CreateInstance(constructedListType);

        }


        public string SelectQuery(object masterId)
        {
            MappedType master = GetMappedType(_masterType);
            MappedType composited = GetMappedType(_compositedType);
            List<object> result = new List<object>();
            //object loadObject;
            var selectQuery = CreateSelect(master, composited, masterId);
            //_adoHelper.ExequteQuery(selectQuery,
            //reader =>
            //{
            //    loadObject = Activator.CreateInstance(arrType);
            //    arrayMaker.LoadInstance(loadObject, reader);
            //    result.Add(loadObject);
            //});
            return selectQuery;
        }

        public void LoadArray(DbDataReader reader)
        {
            object loadingObject = Activator.CreateInstance(_compositedType); 
            _maker.LoadInstance(loadingObject,  reader);
            _loadedObjects.Add(loadingObject);
        }

        public void EndLoad(object loadObject)
        {
            Type arrayType = _compositedType.MakeArrayType();
            object array =  Activator.CreateInstance(arrayType,new object[]{_loadedObjects.Count});
            _loadedObjects.CopyTo((Array)array,0);
            _loadingArray.SetValue(loadObject, array);
        }
      

        private string CreateSelect(MappedType masterType, MappedType compositedType, object masterId)
        {
            string linkTableName = _schema.GetLinkTableName(masterType, compositedType);
            string fromSection = linkTableName + " " + masterType.TableName + " " + compositedType.TableName;
            string whereSection = string.Format("{0}.{1} = {2}", masterType.TableName, masterType.IdTableField, masterId);
            string statement = string.Format("SELECT * FROM {0} {1} {2} WHERE {3}",
                compositedType.TableName, 
                GetJoinString(compositedType, linkTableName, linkTableName),
                GetJoinString(masterType, linkTableName),
                whereSection);
            return statement;

        }

        private string GetJoinString(MappedType mappedType,string linkTableName,string addedTable )
        {
            
            string join = string.Format("{0}.{1} = {2}.{3}", mappedType.TableName, 
                mappedType.IdTableField, linkTableName,  mappedType.LinkName);

            string result = string.Format("INNER JOIN {0} ON {1}", addedTable, join);
            return result;
        }
        private string GetJoinString(MappedType mappedType, string linkTableName)
        {
            return GetJoinString(mappedType, linkTableName, mappedType.TableName);
        }

        private MappedType GetMappedType(Type type)
        {
            return new MappedType(type);
        }
    
    }
}
