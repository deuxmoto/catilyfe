namespace catilyfe.datalayer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
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
                    ParsePostMeta(reader));
        }

        private static PostMeta ParsePostMeta(SqlDataReader reader)
        {
            return new PostMeta(
                (int)reader["id"],
                (string)reader["slug"],
                (string)reader["title"],
                (string)reader["description"],
                (DateTime)reader["created"],
                (DateTime)reader["goeslive"]);
        }

        private static PostContent ParsePostContent(SqlDataReader reader)
        {
            return new PostContent((int)reader["id"], (int)reader["postid"], (string)reader["type"], (string)reader["content"]);
        }

        public async Task<Post> GetPost(int id)
        {
            var results = await this.ExecuteReader(
                "cati.getsinglepost",
                parmeters =>
                {
                    parmeters.AddWithValue("id", id);
                },
                ParsePostMeta, ParsePostContent);
            return new Post(results.Item1.First(), results.Item2);
        }

        public async Task<Post> GetPost(string slug)
        {
            var results = await this.ExecuteReader(
                "cati.getsinglepost",
                parmeters =>
                {
                    parmeters.AddWithValue("slug", slug);
                },
                ParsePostMeta, ParsePostContent);
            return new Post(results.Item1.First(), results.Item2);
        }

        public async Task<IEnumerable<Post>> GetPost(int? top, int? skip, DateTime? startdate, DateTime? enddate)
        {
            var results = await this.ExecuteReader(
                "cati.getposts",
                parmeters =>
                {
                    parmeters.AddWithValue("top", top);
                    parmeters.AddWithValue("skip", skip);
                    parmeters.AddWithValue("startdate", startdate);
                    parmeters.AddWithValue("enddate", enddate);
                },
                ParsePostMeta, ParsePostContent);

            var postContentlookup = results.Item2.ToLookup(c => c.PostId);

            return results.Item1.Select(meta => new Post(meta, postContentlookup.First(m => m.Key == meta.Id))).ToList();
        }

        private async Task<IEnumerable<T>> ExecuteReader<T>(string sproc, Action<SqlParameterCollection> parameters, Func<SqlDataReader, T> readerset)
        {
            using (var connection = await this.GetConnection())
            {
                var command = SetupCommand(sproc, parameters, connection);

                var results = new List<T>();

                await CatiSqlDataLayer.ExecuteSqlReader(
                    async () =>
                        {
                            using (var reader = await command.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    results.Add(readerset(reader));
                                }
                            }
                        });

                return results;
            }
        }

        private async Task<(IEnumerable<T1>, IEnumerable<T2>)> ExecuteReader<T1, T2>(
            string sproc,
            Action<SqlParameterCollection> parameters,
            Func<SqlDataReader, T1> readerset1,
            Func<SqlDataReader, T2> readerset2)
        {
            using (var connection = await this.GetConnection())
            {
                var command = SetupCommand(sproc, parameters, connection);

                var results1 = new List<T1>();
                var results2 = new List<T2>();

                await CatiSqlDataLayer.ExecuteSqlReader(
                    async () =>
                        {
                            using (var reader = await command.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    results1.Add(readerset1(reader));
                                }
                                await reader.NextResultAsync();

                                while (await reader.ReadAsync())
                                {
                                    results2.Add(readerset2(reader));
                                }
                            }
                        });

                return (results1, results2);
            }
        }

        private async Task<SqlConnection> GetConnection()
        {
            var connection = new SqlConnection(this.connectionString);
            await connection.OpenAsync();
            return connection;
        }

        private static SqlCommand SetupCommand(string sproc, Action<SqlParameterCollection> parameters, SqlConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = sproc;
            command.CommandType = CommandType.StoredProcedure;

            var error = new SqlParameter("error_message", SqlDbType.NVarChar, 2048) { Direction = ParameterDirection.Output };
            var retVal = new SqlParameter("ReturnValue", SqlDbType.Int, 4) { Direction = ParameterDirection.ReturnValue };

            command.Parameters.Add(error);
            command.Parameters.Add(retVal);
            parameters(command.Parameters);
            return command;
        }

        private static async Task ExecuteSqlReader(Func<Task> function)
        {
            try
            {
                await function();

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
