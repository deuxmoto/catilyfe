namespace CatiLyfe.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;

    using CatiLyfe.DataLayer.Models;
    using CatiLyfe.Common.Exceptions;

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

        private async Task<IEnumerable<T1>> ExecuteReader<T1>(string sproc, Action<SqlParameterCollection> parameters, Func<SqlDataReader, T1> readerset1)
            where T1 : class
        {
            var results = await this.ExecuteReader(sproc, parameters, new Func<SqlDataReader, object>[] { readerset1 });

            return results[0].Cast<T1>();
        }

        private async Task<(IEnumerable<T1>, IEnumerable<T2>)> ExecuteReader<T1, T2>(
            string sproc,
            Action<SqlParameterCollection> parameters,
            Func<SqlDataReader, T1> readerset1,
            Func<SqlDataReader, T2> readerset2) where T1: class where T2: class
        {
            var results = await this.ExecuteReader(sproc, parameters, new Func<SqlDataReader, object>[] {readerset1, readerset2});

            return (results[0].Cast<T1>(), results[1].Cast<T2>());
        }

        private async Task<List<object>[]> ExecuteReader(
            string sproc,
            Action<SqlParameterCollection> parameters,
            Func<SqlDataReader, object>[] readersets)
        {
            using (var connection = await this.GetConnection())
            {
                var command = SetupCommand(sproc, parameters, connection);

                var results = new List<object>[readersets.Length];

                await CatiSqlDataLayer.ExecuteSqlReader(
                    async () =>
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            // If we returned an error, exit immediatly.
                            this.HandleSoftError(command);

                            for (var i = 0; i < readersets.Length; i++)
                            {
                                results[i] = new List<object>();

                                while (true == await reader.ReadAsync())
                                {
                                    results[i].Add(readersets[i](reader));
                                }

                                if (i < readersets.Length - 1)
                                {
                                    if (false == await reader.NextResultAsync())
                                    {
                                        // Check to see if we got something nice.
                                        this.HandleSoftError(command);

                                        // Otherwise throw normally
                                        throw new InvalidOperationException($"Expecting another result set in sproc {sproc}. Current {i+1} out of {readersets.Length}.");
                                    }
                                }
                            }
                        }
                    });

                return results;
            }
        }

        private void HandleSoftError(SqlCommand command)
        {
            var retvalue = command.Parameters["ReturnValue"]?.Value as int? ?? 0;
            var message = command.Parameters["error_message"]?.Value as string;

            switch (retvalue)
            {
                case 0:
                    return;
                case 50001:
                    throw new ItemNotFoundException(message ?? "Item not found");
                case 50002:
                    throw new InvalidArgumentException(message ?? "Invalid arguments provided.");
                default:
                    if (retvalue > 50000)
                    {
                        throw new DeveloperIsAnIdiotException();
                    }
                    else
                    {
                        throw new Exception("SQL failure. Generic. No error. Peter will fix.");
                    }
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
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
