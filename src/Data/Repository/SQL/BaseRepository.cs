using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using WebAPI.Domain.Interfaces.Repositories;
using WebAPI.Infrastructure.Common;
using WebAPI.Infrastructure.Common.Enums;

namespace WebAPI.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private IConfigurationRoot DBConfig { get; set; }
        private string ConnectionString { get; set; } 
        private EnumDatabase Database { get; set; }
        public IDbConnection Connection => Database switch
        {
            EnumDatabase.SQLServer => new SqlConnection(ConnectionString),
            EnumDatabase.Oracle => new OracleConnection(ConnectionString),
            _ => null,
        };

        public BaseRepository()
        {
            DBConfig = new Config().configuration;
            ConnectionString = DBConfig.GetConnectionString("ConnectionString");
            Database = Enum.Parse<EnumDatabase>(DBConfig.GetSection("Database").Value);
        }

        public bool Delete(T entity)
        {
            using var connection = Connection;
            return connection.Delete(entity);
        }

        public IEnumerable<T> FindAll(string query)
        {
            using var connection = Connection;
            return connection.Query<T>(query);
        }

        public T Get(int id)
        {
            using var connection = Connection;
            return connection.Get<T>(id);
        }

        public long Insert(T entity)
        {
            using var connection = Connection;
            return connection.Insert(entity);
        }

        public bool InsertRange(List<T> entities)
        {
            long insertedRows;
            using var connection = Connection;
            insertedRows = connection.Insert(entities);

            return insertedRows == entities.Count;
        }

        public bool Update(T entity)
        {
            using var connection = Connection;
            return connection.Update(entity);
        }
    }
}
