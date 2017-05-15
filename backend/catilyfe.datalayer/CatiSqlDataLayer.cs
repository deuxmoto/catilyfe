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

        public Task<IEnumerable<PostMeta>> GetPostMetadata(int? top, int? skip, DateTime? startdate, DateTime? enddate)
        {
            return this.ExecuteReader(
                "cati.getpostmetadata",
                parmeters =>
                    {
                        parmeters.AddWithValue("top", top);
                        parmeters.AddWithValue("skip", skip);
                        parmeters.AddWithValue("startdate", startdate);
                        parmeters.AddWithValue("enddate", enddate);
                    },
                reader =>
                    new PostMeta(
                        (int)reader["id"],
                        (string)reader["slug"],
                        (string)reader["title"],
                        (string)reader["description"],
                        (DateTime)reader["created"],
                        (DateTime)reader["goeslive"]));
        }

        private async Task<IEnumerable<T>> ExecuteReader<T>(string sproc, Action<SqlParameterCollection> parameters, Func<SqlDataReader, T> readerset)
        {
            using (var connection = await this.GetConnection())
            {
                var command = connection.CreateCommand();
                command.CommandText = sproc;
                command.CommandType = CommandType.StoredProcedure;


                var error = new SqlParameter("error_message", SqlDbType.NVarChar, 2048)
                                {
                                    Direction = ParameterDirection.Output
                                };
                var retVal = new SqlParameter("ReturnValue", SqlDbType.Int, 4)
                                 {
                                     Direction = ParameterDirection.ReturnValue
                                 };


                command.Parameters.Add(error);
                command.Parameters.Add(retVal);
                parameters(command.Parameters);


                var results = new List<T>();

                try
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            results.Add(readerset(reader));
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
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
