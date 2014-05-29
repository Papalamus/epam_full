using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataObjects.DataBase.PersonConnecters
{
    internal class AdoHelper
    {
        private DbProviderFactory providerFactory;
        private string cnStr;
        
        public AdoHelper()
        {
            try
            {
               
                cnStr = ConfigurationManager.ConnectionStrings["cnString"].ConnectionString;                
                string provider = ConfigurationManager.ConnectionStrings["cnString"].ProviderName;
                providerFactory = DbProviderFactories.GetFactory(provider);
            }
            catch (NullReferenceException e)
            {
                throw new ConfigurationErrorsException("Не удалось прочитать connectionString, " +
                                                       "необходимые для соединения с базой данных ", e);
            }
           
        }

        public bool ExequteQuery(CustomizeCommandHandler customizeFoo, ProcessReaderHandler readerHandler)
        {
            bool isRead = false;
            using (DbConnection connection = providerFactory.CreateConnection())
            {
                try
                {
                    connection.ConnectionString = cnStr;
                    connection.Open();
                }
                catch (InvalidOperationException e)
                {
                    //TODO Добавить обработчик
                }

                DbCommand command = connection.CreateCommand();
                customizeFoo(command);
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        readerHandler(reader);
                        isRead = true;
                    }
                }
               
            }
            return isRead;
        }

        public void ExequteQuery(string commandText, ProcessReaderHandler readerHandler)
        {
            ExequteQuery(command => command.CommandText = commandText, readerHandler);
        }

        public void ExequteNonQuery(CustomizeCommandHandler customizeFoo)
        {
            using (DbConnection connection = providerFactory.CreateConnection())
            {
                try
                {
                    connection.ConnectionString = cnStr;
                    connection.Open();
                } 
                catch (InvalidOperationException e)
                {
                    //TODO Добавить обработчик
                }
                DbCommand command = connection.CreateCommand();
                customizeFoo(command);
                command.ExecuteNonQuery();
                
            }

        }

        public void ExequteNonQuery(string commandText)
        {
            ExequteNonQuery(command => command.CommandText = commandText);
        }

    }
}
