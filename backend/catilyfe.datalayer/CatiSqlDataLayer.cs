namespace catilyfe.datalayer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    using catilyfe.datalayer.Models;

    internal sealed class CatiSqlDataLayer : ICatiDataLayer
    {
        private readonly string connectionString;
        public CatiSqlDataLayer(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Task<IEnumerable<PostMeta>> GetPostMetadata()
        {
            throw new NotImplementedException();
        }

        private async Task<IEnumerable<T>> ExecuteReader<T>(string sproc, Action<SqlParameterCollection> parameters, Action<T> readerset)
        {
            using (var connection = await this.GetConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = sproc;
                command.CommandType = CommandType.StoredProcedure;

                parameters(command.Parameters);


                var results = new List<T>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        
                    }
                }

                return results;
            }
        }

        private async Task<SqlConnection> GetConnection()
        {
            var connection = new SqlConnection(this.connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}
