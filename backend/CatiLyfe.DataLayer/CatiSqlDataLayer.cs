﻿namespace CatiLyfe.DataLayer
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
                new DateTimeOffset((DateTime)reader["created"]),
                new DateTimeOffset((DateTime)reader["goeslive"]));
        }

        /// <summary>
        /// Parses a post to tag mapping.
        /// </summary>
        /// <param name="reader">The sql data reader.</param>
        /// <returns>A post to tag mapping object.</returns>
        private static (int post, string tag) ParsePostTag(SqlDataReader reader)
        {
            return (post: (int)reader["post"], tag: (string)reader["tag"]);
        }

        /// <summary>
        /// Parses a row of post content from the the reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>A peice of post content.</returns>
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
                ParsePostMeta, ParsePostContent, ParsePostTag);

            var metadata = results.Item1.First();
            var tags = results.Item3;
            metadata.Tags = tags.Select(t => t.Item2).ToArray();

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
                ParsePostMeta, ParsePostContent, ParsePostTag);

            var metadata = results.Item1.First();
            var tags = results.Item3;
            metadata.Tags = tags.Select(t => t.Item2).ToArray();

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
                ParsePostMeta, ParsePostContent, ParsePostTag);

            var postContentlookup = results.Item2.ToLookup(c => c.PostId);
            var tagsLookup = results.Item3.ToLookup(t => t.Item1);

            // Get the tag mapping.
            foreach (var metadata in results.Item1)
            {
                if (tagsLookup.Contains(metadata.Id))
                {
                    metadata.Tags = tagsLookup[metadata.Id].Select(t => t.Item2).ToList();
                }
            }

            return results.Item1.Select(meta => new Post(meta, postContentlookup.First(m => m.Key == meta.Id))).ToList();
        }

        private async Task<IEnumerable<T1>> ExecuteReader<T1>(string sproc, Action<SqlParameterCollection> parameters, Func<SqlDataReader, T1> readerset1)
        {
            var results = await this.ExecuteReader(sproc, parameters, (new object[] { readerset1 }).Cast<Func<SqlDataReader, object>>().ToArray());

            return results[0].Cast<T1>();
        }

        private async Task<(IEnumerable<T1>, IEnumerable<T2>)> ExecuteReader<T1, T2>(
            string sproc,
            Action<SqlParameterCollection> parameters,
            Func<SqlDataReader, T1> readerset1,
            Func<SqlDataReader, T2> readerset2)
        {
            var results = await this.ExecuteReader(sproc, parameters, (new object [] {readerset1, readerset2}).Cast<Func<SqlDataReader, object>>().ToArray());

            return (results[0].Cast<T1>(), results[1].Cast<T2>());
        }

        private async Task<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>)> ExecuteReader<T1, T2, T3>(
            string sproc,
            Action<SqlParameterCollection> parameters,
            Func<SqlDataReader, T1> readerset1,
            Func<SqlDataReader, T2> readerset2,
            Func<SqlDataReader, T3> readerset3)
        {
            var results = await this.ExecuteReader(
                              sproc,
                              parameters,
                              (new object[] { readerset1, readerset2, readerset3 }).Cast<Func<SqlDataReader, object>>().ToArray());

            return (results[0].Cast<T1>(), results[1].Cast<T2>(), results[2].Cast<T3>());
        }

        /// <summary>
        /// A genius function which allows for the parsing of SQL result sets.
        /// </summary>
        /// <param name="sproc">The stored procedure name.</param>
        /// <param name="parameters">The parameters modifier.</param>
        /// <param name="readersets">The reader functions.</param>
        /// <returns>All of the data.</returns>
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