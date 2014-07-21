using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataObjects.Entities;
using Test_project.Reflection;

namespace Test_project.DataBase.PersonConnecters
{
    internal class DbSchema
    {
        private Dictionary<Type, string> NetToDb = new Dictionary<Type, string>();
        private Dictionary<string, string> requiredTables = new Dictionary<string, string>();

        public Dictionary<string, string> RequiredTables
        {
            get { return requiredTables; }
        }

        public DbSchema()
        {
            GetDatabaseMapping();
        }

        private void GetDatabaseMapping()
        {
            NetToDb.Add(typeof(string), "nvarchar(255)");
            NetToDb.Add(typeof(int), "integer");
        }


        public string GetLinkTableName(MappedType masterType, MappedType compositedType)
        {
            string result = masterType.TableName + "_" +
                            compositedType.TableName;
            return result;
        }

        public void Request(MappedType mappedType)
        {
            string tableName = mappedType.TableName;
            if (!requiredTables.ContainsKey(tableName))
            {
                requiredTables.Add(tableName,QueryClassTable(mappedType));
            }
        }

        public void RequestLinkTable(MappedType masterType, MappedType compositedType)
        {
            string tableName = GetLinkTableName(masterType, compositedType);
            if (!requiredTables.ContainsKey(tableName))
            {
                requiredTables.Add(tableName, QuerytLinkTable(masterType, compositedType));
            }
        }
        
        private string QuerytLinkTable(MappedType masterType, MappedType compositedType)
        {
            string tableName = GetLinkTableName(masterType, compositedType);

            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.AppendFormat("CREATE TABLE {0} (",tableName );
            
            AddIdColumn(tableName,queryBuilder);

            AddFkColumn(masterType,queryBuilder);
            AddFkColumn(compositedType, queryBuilder);

            queryBuilder.Append(")");
            return queryBuilder.ToString();
        }

        private void AddIdColumn(string tableName, StringBuilder queryBuilder)
        {
            queryBuilder.Append("id integer IDENTITY NOT NULL PRIMARY KEY ");
            //queryBuilder.AppendFormat(" CONSTRAINT {0}_{1}_pk PRIMARY KEY ({1})", tableName, "id");
        }
        private void AddFkColumn(MappedType mappedType,StringBuilder queryBuilder)
        {
            var dbType = NetToDb[mappedType.IdMember.GetReflectedType()];
            queryBuilder.AppendFormat(", {0} {1} ,", mappedType.LinkName, dbType);

            queryBuilder.AppendFormat(" CONSTRAINT {0}_fk FOREIGN KEY ({0}) REFERENCES {1}({2})",
                mappedType.LinkName, mappedType.TableName, mappedType.IdTableField);
        }


        private string QueryClassTable(MappedType mappedType)
        {

            StringBuilder createQuery = new StringBuilder();
            createQuery.AppendFormat("CREATE TABLE {0} (", mappedType.TableName);
           
            foreach (var pair in mappedType.Fields)
            {
                var fieldName = pair.Key;
                var Type = pair.Value.FieldType;
                var dbType = NetToDb[Type];
                createQuery.AppendFormat("{0} {1} ,", fieldName, dbType);
                
            }
            foreach (var pair in mappedType.Properties)
            {
                var propertyName = pair.Key;
                var Type = pair.Value.PropertyType;
                var dbType = NetToDb[Type];
                createQuery.AppendFormat("{0} {1},", propertyName, dbType);
            }
            var pkConstraintName = string.Format("{0}_{1}_pk", mappedType.TableName, mappedType.IdTableField);
            createQuery.AppendFormat(" CONSTRAINT {0} PRIMARY KEY ({1}))", pkConstraintName, mappedType.IdTableField);

            //RequestTable(mappedType.TableName,createQuery.ToString());            
            return createQuery.ToString();
        }

      
        //public void RequestTable(string tableName, string createQuery)
        //{
        //    if (!requiredTables.ContainsKey(tableName))
        //    {
        //        requiredTables.Add(tableName, createQuery);
        //    }
        //}

        

        public string CheckString(string tableName)
        {
            return string.Format(@"SELECT      Count(*) AS Entries "+
                                 "FROM         INFORMATION_SCHEMA.TABLES "+
                                 "WHERE        (TABLE_NAME = '{0}')"
                , tableName);
        }
    }
}
