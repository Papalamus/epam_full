using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects.DataBase.Interface
{
    public interface IAdoSaveable
    {
        void ReadObject(DbDataReader reader);
        void SaveObject(DbCommand command,string tableName);
    }
}
