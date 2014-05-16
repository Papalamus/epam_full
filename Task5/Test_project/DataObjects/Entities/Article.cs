using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects.Attributes;
using DataObjects.DataBase.Interface;

namespace DataObjects.Entities
{
    [Serializable]
    [TableOrmSave(Name = "ArticleTable")]
    public class Article : IAdoSaveable
    {
        [FieldOrmSave(Name ="ValueField",type = typeof(int))]
        public int Value { get; set; }
        [FieldOrmSave(Name = "TitleField", type = typeof(string))]
        public string Title{ get; set; }
        [IdFieldOrmSave(Name = "Article_id", type = typeof(int))]
        public int ArticleCode { get; set; }

        public Article() { }

        

        void IAdoSaveable.ReadObject(DbDataReader reader)
        {

            string val = reader["Value"].ToString();
            Value = int.Parse(reader["Value"].ToString());
            Title = (string)reader["Title"];
            ArticleCode = int.Parse(reader["Article_id"].ToString());
           
        }

        public void SaveObject(DbCommand command, string tableName)
        {
            command.CommandText = string.Format("Insert into {0}(Article_id,Title,Value) Values (@ID,@Title,@Value)",tableName);
            
            AddParametrs(command, "@ID",ArticleCode);
            AddParametrs(command, "@Value", Value);
            AddParametrs(command, "@Title", Title??"");
        }

        public void AddParametrs(DbCommand command, string name, object value)
        {
            var parametr = command.CreateParameter();
            parametr.ParameterName = name;
            parametr.Value = value;
            command.Parameters.Add(parametr);
        }

        public override string ToString()
        {
            return string.Format("Code = {0}, Title= {1}, Value = {2}", ArticleCode, Title, Value);
        }
    }
}
