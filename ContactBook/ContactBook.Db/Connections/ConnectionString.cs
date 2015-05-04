using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Db.Connections
{
    public static class ConnectionString
    {
        static Dictionary<string, EntityConnectionStringBuilder> entityInstance;
        static ConnectionString()
        {
            entityInstance = new Dictionary<string, EntityConnectionStringBuilder>();
        }

        public static string EFConnectionString(string connectionName)
        {
            if (!entityInstance.ContainsKey(connectionName))
            {

                var sqlbuilder = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings[connectionName].ConnectionString);
                var entityBuilder = new EntityConnectionStringBuilder();
                entityBuilder.ProviderConnectionString = sqlbuilder.ToString();
                entityBuilder.Metadata = @"res://*/Data.ContactBookEdm.csdl|res://*/Data.ContactBookEdm.ssdl|res://*/Data.ContactBookEdm.msl";
                entityBuilder.Provider = ConfigurationManager.ConnectionStrings[connectionName].ProviderName;
                lock (entityInstance)
                {
                    if (!entityInstance.ContainsKey(connectionName))
                    {
                        entityInstance.Add(connectionName, entityBuilder);
                    }
                }
            }
            return entityInstance[connectionName].ToString();
        }
    }
}
