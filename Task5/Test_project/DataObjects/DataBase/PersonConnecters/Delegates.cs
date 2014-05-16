using System.Data.Common;

namespace DataObjects.DataBase.PersonConnecters
{
    public delegate void CustomizeCommandHandler(DbCommand command);
    public delegate void ProcessReaderHandler(DbDataReader reader);
}