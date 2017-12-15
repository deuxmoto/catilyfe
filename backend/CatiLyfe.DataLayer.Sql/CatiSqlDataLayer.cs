namespace CatiLyfe.DataLayer.Sql
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;

    using CatiLyfe.DataLayer.Models;
    using CatiLyfe.Common.Exceptions;

    using Microsoft.SqlServer.Server;
    using System.Data;

    internal sealed class CatiSqlDataLayer : ICatiDataLayer, ICatiAuthDataLayer
    {
        private readonly string connectionString;
        public CatiSqlDataLayer(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<IEnumerable<PostMeta>> GetPostMetadata(int? top, int? skip, DateTime? startdate, DateTime? enddate, bool includeUnpublished, bool includeDeleted, IEnumerable<string> tags)
        {
            var results =  await this.ExecuteReader(
                "cati.getpostmetadata",
                parmeters =>
                    {
                        parmeters.AddWithValue("top", top);
                        parmeters.AddWithValue("skip", skip);
                        parmeters.AddWithValue("startdate", startdate);
                        parmeters.AddWithValue("enddate", enddate);
                        parmeters.AddWithValue("includeUnpublished", includeUnpublished);
                        parmeters.AddWithValue("includeDeleted", includeDeleted);
                        var tagslist = parmeters.AddWithValue("tags", CatiSqlDataLayer.GetPostTagRecords(tags));
                        tagslist.SqlDbType = SqlDbType.Structured;
                        tagslist.TypeName = "cati.tagslist";
                    },
                SqlParsers.ParsePostMeta,
                SqlParsers.ParsePostTag,
                SqlParsers.ParsePostAuditMapping);

            var tagsLookup = results.Item2.ToLookup(t => t.PostId);
            var historyLookup = results.Item3.ToLookup(a => a.PostId);

            // Get the tag mapping.
            foreach (var metadata in results.Item1)
            {
                if (tagsLookup.Contains(metadata.Id))
                {
                    metadata.Tags = tagsLookup[metadata.Id].Select(t => t.Tag);
                }

                if (historyLookup.Contains(metadata.Id))
                {
                    metadata.History = historyLookup[metadata.Id].Select(a => a.ToPostAuditHistory());
                }
            }

            return results.Item1;
        }

        public async Task<Post> GetPost(int id, bool includeUnpublished, bool includeDeleted)
        {
            var post = (await this.GetPostInternal(
                        id: id,
                        slug: null,
                        top: null,
                        skip: null,
                        startdate: null,
                        enddate: null,
                        includeUnpublished: includeUnpublished,
                        includeDeleted: includeDeleted,
                        tags: null)).FirstOrDefault();

            if (null == post)
            {
                throw new ItemNotFoundException($"The post with the id '{id}' was not found.");
            }

            return post;
        }

        public async Task<Post> GetPost(string slug, bool includeUnpublished, bool includeDeleted)
        {
            var post = (await this.GetPostInternal(
                        id: null,
                        slug: slug,
                        top: null,
                        skip: null,
                        startdate: null,
                        enddate: null,
                        includeUnpublished: includeUnpublished,
                        includeDeleted: includeDeleted,
                        tags: null)).FirstOrDefault();

            if (null == post)
            {
                throw new ItemNotFoundException($"The post with the slug '{slug}' was not found.");
            }

            return post;
        }

        public async Task<IEnumerable<Post>> GetPost(int? top, int? skip, DateTime? startdate, DateTime? enddate, bool includeUnpublished, bool includeDeleted, IEnumerable<string> tags)
        {
            return (await this.GetPostInternal(
                        id: null,
                        slug: null,
                        top: top,
                        skip: skip,
                        startdate: startdate,
                        enddate: enddate,
                        includeUnpublished: includeUnpublished,
                        includeDeleted: includeDeleted,
                        tags: tags)).ToImmutableList();
        }

        private async Task<IEnumerable<Post>> GetPostInternal(
            int? id,
            string slug,
            int? top,
            int? skip,
            DateTime? startdate,
            DateTime? enddate,
            bool includeUnpublished,
            bool includeDeleted,
            IEnumerable<string> tags)
        {
            var results = await this.ExecuteReader(
                              "cati.getposts",
                              parmeters =>
                                  {
                                      parmeters.AddWithValue("id", id);
                                      parmeters.AddWithValue("slug", slug);
                                      parmeters.AddWithValue("top", top);
                                      parmeters.AddWithValue("skip", skip);
                                      parmeters.AddWithValue("startdate", startdate);
                                      parmeters.AddWithValue("enddate", enddate);
                                      parmeters.AddWithValue("includeUnpublished", includeUnpublished);
                                      parmeters.AddWithValue("includeDeleted", includeDeleted);
                                      var tagslist = parmeters.AddWithValue("tags", CatiSqlDataLayer.GetPostTagRecords(tags));
                                      tagslist.SqlDbType = SqlDbType.Structured;
                                      tagslist.TypeName = "cati.tagslist";
                                  },
                              SqlParsers.ParsePostMeta,
                              SqlParsers.ParsePostContent,
                              SqlParsers.ParsePostTag,
                              SqlParsers.ParsePostAuditMapping);

            var postContentlookup = results.Item2.ToLookup(c => c.PostId);
            var tagsLookup = results.Item3.ToLookup(t => t.PostId);
            var historyLookup = results.Item4.ToLookup(a => a.PostId);

            // Get the tag mapping.
            foreach (var metadata in results.Item1)
            {
                if (tagsLookup.Contains(metadata.Id))
                {
                    metadata.Tags = tagsLookup[metadata.Id].Select(t => t.Tag);
                }

                if (historyLookup.Contains(metadata.Id))
                {
                    metadata.History = historyLookup[metadata.Id].Select(a => a.ToPostAuditHistory());
                }
            }

            return results.Item1.Select(meta => new Post(meta, postContentlookup.First(m => m.Key == meta.Id)));
        }

        /// <summary>
        /// Gets all of the tags.
        /// </summary>
        /// <returns>The tags.</returns>
        public async Task<IEnumerable<PostTag>> GetTags()
        {
            var result = await this.ExecuteReader(
                "cati.gettags",
                parmeters =>
                {
                },
                SqlParsers.ParseTag);

            return result;
        }

        /// <summary>
        /// Set a post.
        /// </summary>
        /// <param name="post">The post.</param>
        /// <param name="userAccessDetails">The user access details.</param>
        /// <returns>An async task.</returns>
        public async Task<Post> SetPost(Post post, UserAccessDetails userAccessDetails)
        {
            var results = await this.ExecuteReader(
                              "cati.setpost",
                              parmeters =>
                                  {
                                      parmeters.AddWithValue("id", post.MetaData.Id <= 0 ? null : (int?)post.MetaData.Id);
                                      parmeters.AddWithValue("slug", post.MetaData.Slug);
                                      parmeters.AddWithValue("title", post.MetaData.Title);
                                      parmeters.AddWithValue("description", post.MetaData.Description);
                                      parmeters.AddWithValue("userid", userAccessDetails.UserId);
                                      parmeters.AddWithValue("goeslive", post.MetaData.GoesLive);
                                      parmeters.AddWithValue("ispublished", post.MetaData.IsPublished);
                                      parmeters.AddWithValue("isreserved", post.MetaData.IsReserved);
                                      parmeters.AddWithValue("revision", post.MetaData.Revision);

                                      var contentList = parmeters.AddWithValue(
                                          "content",
                                          CatiSqlDataLayer.GetPostContentRecord(post.PostContent));
                                      contentList.SqlDbType = SqlDbType.Structured;
                                      contentList.TypeName = "cati.postcontentlist";
                                      var tagslist = parmeters.AddWithValue(
                                          "tags",
                                          CatiSqlDataLayer.GetPostTagRecords(post.MetaData.Tags));
                                      tagslist.SqlDbType = SqlDbType.Structured;
                                      tagslist.TypeName = "cati.tagslist";
                                  },
                              SqlParsers.ParsePostMeta,
                              SqlParsers.ParsePostContent,
                              SqlParsers.ParsePostTag,
                              SqlParsers.ParsePostAuditMapping);

            var metadata = results.Item1.First();
            var tags = results.Item3;
            metadata.Tags = tags.Select(t => t.Tag);

            var history = results.Item4;
            metadata.History = history.Select(h => h.ToPostAuditHistory());

            return new Post(results.Item1.First(), results.Item2);
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
            Func<SqlDataReader, T2> readerset2) where T1 : class where T2 : class
        {
            var results = await this.ExecuteReader(sproc, parameters, new Func<SqlDataReader, object>[] { readerset1, readerset2 });

            return (results[0].Cast<T1>(), results[1].Cast<T2>());
        }

        private async Task<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>)> ExecuteReader<T1, T2, T3>(
            string sproc,
            Action<SqlParameterCollection> parameters,
            Func<SqlDataReader, T1> readerset1,
            Func<SqlDataReader, T2> readerset2,
            Func<SqlDataReader, T3> readerset3) where T1 : class where T2 : class where T3 : class
        {
            var results = await this.ExecuteReader(
                              sproc,
                              parameters,
                              new Func<SqlDataReader,object>[] { readerset1, readerset2, readerset3 });

            return (results[0].Cast<T1>(), results[1].Cast<T2>(), results[2].Cast<T3>());
        }

        private async Task<(IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>)> ExecuteReader<T1, T2, T3, T4>(
            string sproc,
            Action<SqlParameterCollection> parameters,
            Func<SqlDataReader, T1> readerset1,
            Func<SqlDataReader, T2> readerset2,
            Func<SqlDataReader, T3> readerset3,
            Func<SqlDataReader, T4> readerset4) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            var results = await this.ExecuteReader(
                              sproc,
                              parameters,
                              new Func<SqlDataReader, object>[] { readerset1, readerset2, readerset3, readerset4 });

            return (results[0].Cast<T1>(), results[1].Cast<T2>(), results[2].Cast<T3>(), results[3].Cast<T4>());
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
            IList<Func<SqlDataReader, object>> readersets)
        {
            using (var connection = await this.GetConnection())
            {
                var command = SetupCommand(sproc, parameters, connection);

                var results = new List<object>[readersets.Count];

                await CatiSqlDataLayer.ExecuteSqlReader(
                    async () =>
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            // If we returned an error, exit immediatly.
                            this.HandleSoftError(command);

                            for (var i = 0; i < readersets.Count; i++)
                            {
                                results[i] = new List<object>();

                                while (true == await reader.ReadAsync())
                                {
                                    results[i].Add(readersets[i](reader));
                                }

                                if (i < readersets.Count - 1)
                                {
                                    if (false == await reader.NextResultAsync())
                                    {
                                        // Check to see if we got something nice.
                                        this.HandleSoftError(command);

                                        // Otherwise throw normally
                                        throw new InvalidOperationException($"Expecting another result set in sproc {sproc}. Current {i+1} out of {readersets.Count}.");
                                    }
                                }
                            }
                        }
                    });

                return results;
            }
        }

        private async Task ExecuteNonQuery(
            string sproc,
            Action<SqlParameterCollection> parameters)
        {
            using (var connection = await this.GetConnection())
            {
                var command = SetupCommand(sproc, parameters, connection);

                await CatiSqlDataLayer.ExecuteSqlReader(
                    async () =>
                        {
                            await command.ExecuteNonQueryAsync();
                            this.HandleSoftError(command);
                    });
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
                case 50003:
                    throw new DuplicateItemException(message ?? "Duplicate item");
                case 50004:
                    throw new RevisionMismatchException(message ?? "Revision id mismatch");
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
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets records for post tags.
        /// </summary>
        /// <param name="tags">The tags.</param>
        /// <returns>The enumerable.</returns>
        private static IEnumerable<SqlDataRecord> GetPostTagRecords(IEnumerable<string> tags)
        {
            return tags.ToDataTable(
                () => new[] { new SqlMetaData("tag", SqlDbType.NVarChar, 64) },
                (record, tag) =>
                    {
                        record.SetValue(0, tag);
                    });
        }

        /// <summary>
        /// Gets records for post content.
        /// </summary>
        /// <param name="content">The content of the post.</param>
        /// <returns>The post content.</returns>
        private static IEnumerable<SqlDataRecord> GetPostContentRecord(IEnumerable<PostContent> content)
        {
            return content.ToDataTable(
                () => new[]
                          {
                              new SqlMetaData("id", SqlDbType.Int),
                              new SqlMetaData("type", SqlDbType.NVarChar, 64),
                              new SqlMetaData("content", SqlDbType.NVarChar, 4000),
                          },
                (record, tag) =>
                    {
                        record.SetValue(0, tag.Index);
                        record.SetValue(1, tag.ContentType);
                        record.SetValue(2, tag.Content);
                    });
        }

        /// <summary>
        /// Gets the role records.
        /// </summary>
        /// <param name="roles">The roles.</param>
        /// <returns>The records.</returns>
        private static IEnumerable<SqlDataRecord> GetRoleRecord(IEnumerable<string> roles)
        {
            return roles.ToDataTable(
                () => new[] { new SqlMetaData("role", SqlDbType.NVarChar, 64) },
                (record, role) =>
                    {
                        record.SetValue(0, role);
                    });
        }

        public Task DeletePost(int id, UserAccessDetails userAccessDetails)
        {
            return this.ExecuteNonQuery(
                "cati.deletepost",
                parrameters =>
                    {
                        parrameters.AddWithValue("id", id);
                        parrameters.AddWithValue("userid", userAccessDetails.UserId);
                    });
        }

        public Task DeletePost(string slug, UserAccessDetails userAccessDetails)
        {
            return this.ExecuteNonQuery(
                "cati.deletepost",
                parrameters =>
                {
                    parrameters.AddWithValue("slug", slug);
                    parrameters.AddWithValue("userid", userAccessDetails.UserId);
                });
        }

        /// <summary>
        /// Gets all user information.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="email">The email.</param>
        /// <param name="token">The token.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task<IEnumerable<User>> GetUser(int? id, string email, byte[] token)
        {
            var result = await this.ExecuteReader(
                "auth.getuserinfo",
                parameters =>
                {
                    parameters.AddWithValue("id", id);
                    parameters.AddWithValue("email", email);
                    parameters.AddWithValue("token", token);
                },
                SqlParsers.ParseRole,
                SqlParsers.ParseUser);

            var users = result.Item2.ToList();

            if (false == users.Any())
            {
                throw new ItemNotFoundException("The user could not be found.");
            }

            var roles = result.Item1.ToLookup(role => (int?)role.UserId, role => role.Role);

            foreach (var user in users)
            {
                user.Roles = new HashSet<string>((roles.Contains(user.Id) ? roles[user.Id] : Enumerable.Empty<string>()));
            }

            return users;
        }

        /// <summary>
        /// Creates a token for a user.
        /// </summary>
        /// <param name="user">The user id.</param>
        /// <param name="token">The token.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns>An async task.</returns>
        public Task CreateToken(int user, byte[] token, DateTime expiry)
        {
            return this.ExecuteNonQuery(
                "auth.settoken",
                parameters =>
                    {
                        parameters.AddWithValue("userid", user);
                        parameters.AddWithValue("token", token);
                        parameters.AddWithValue("expiry", expiry);
                    });
        }

        /// <summary>
        /// Sets a user based on the user model.
        /// </summary>
        /// <param name="usermodel">The user model.</param>
        /// <returns>An async task..</returns>
        public Task SetUser(User usermodel)
        {
            return this.ExecuteNonQuery(
                "auth.setuserinfo",
                parameters =>
                    {
                        parameters.AddWithValue("id", usermodel.Id);
                        parameters.AddWithValue("name", usermodel.Name);
                        parameters.AddWithValue("email", usermodel.Email);
                        parameters.AddWithValue("password", usermodel.Password);
                        var rolelist = parameters.AddWithValue(
                            "rolelist",
                            CatiSqlDataLayer.GetRoleRecord(usermodel.Roles));
                        rolelist.SqlDbType = SqlDbType.Structured;
                        rolelist.TypeName = "auth.rolelist";
                    });
        }

        /// <summary>
        /// Gets the user roles.
        /// </summary>
        /// <returns>The list of roles.</returns>
        public Task<IEnumerable<UserRoleDescription>> GetRoles()
        {
            return this.ExecuteReader(
                "auth.getroles",
                parameters: parameters =>
                    {
                    },
                readerset1: SqlParsers.ParseUserRoleDescription);
        }

        /// <summary>
        /// Deletes a role.
        /// </summary>
        /// <param name="name">The name of role.</param>
        /// <param name="commit">To really delete it. Or just soft delete.</param>
        /// <returns>The task.</returns>
        public Task DeleteRole(string name, bool commit)
        {
            return this.ExecuteNonQuery(
                "auth.deleterole",
                parameters: parameters =>
                    {
                        parameters.AddWithValue("name", name);
                        parameters.AddWithValue("commit", commit);
                    });
        }

        /// <summary>
        /// Creates or edits a role.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public Task SetRole(UserRoleDescription role)
        {
            return this.ExecuteNonQuery(
                "auth.setrole",
                parameters: paramters =>
                {
                    paramters.AddWithValue("name", role.RoleName);
                    paramters.AddWithValue("description", role.Description);
                    paramters.AddWithValue("revive", true);
                });
        }

        /// <summary>
        /// Deauthorize a token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="token">The token.</param>
        /// <returns>An async task.</returns>
        public Task DeauthorizeToken(int user, byte[] token)
        {
            return this.ExecuteNonQuery("auth.settoken", parameters => {
                parameters.AddWithValue("userid", user);
                parameters.AddWithValue("token", token);
                parameters.AddWithValue("expiry", DateTime.UtcNow);
            });
        }
    }
}
