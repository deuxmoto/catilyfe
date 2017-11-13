using System;
using System.Collections.Generic;
using System.Text;

namespace CatiLyfe.DataLayer.Sql
{
    using System.Data.SqlClient;

    using CatiLyfe.DataLayer.Models;
    using CatiLyfe.DataLayer.Sql.Models;

    internal static class SqlParsers
    {
        public static PostMeta ParsePostMeta(SqlDataReader reader)
        {
            return new PostMeta(
                id: (int)reader["id"],
                slug: (string)reader["slug"],
                title: (string)reader["title"],
                description: (string)reader["description"],
                whencreated: new DateTimeOffset((DateTime)reader["created"]),
                goeslive: new DateTimeOffset((DateTime)reader["goeslive"]),
                isReserved: (bool)reader["isreserved"],
                isPublished: (bool)reader["ispublished"],
                isDeleted: (bool)reader["isdeleted"]);
        }

        /// <summary>
        /// Parses a post to tag mapping.
        /// </summary>
        /// <param name="reader">The sql data reader.</param>
        /// <returns>A post to tag mapping object.</returns>
        public static PostToTagMapping ParsePostTag(SqlDataReader reader)
        {
            return new PostToTagMapping((int)reader["post"], (string)reader["tag"]);
        }

        /// <summary>
        /// Parse a post tags object from the reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>The post tag.</returns>
        public static PostTag ParseTag(SqlDataReader reader)
        {
            return new PostTag((string)reader["tag"], (int)reader["posts"]);
        }

        /// <summary>
        /// Parses a row of post content from the the reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>A peice of post content.</returns>
        public static PostContent ParsePostContent(SqlDataReader reader)
        {
            return new PostContent((int)reader["id"], (int)reader["postid"], (string)reader["type"], (string)reader["content"]);
        }

        /// <summary>
        /// The from reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>The <see cref="UserRoleDescription"/>.</returns>
        public static UserRoleDescription ParseUserRoleDescription(SqlDataReader reader)
        {
            return new UserRoleDescription((string)reader["role"], (string)reader["description"]);
        }

        /// <summary>
        /// Parses a user from the reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>The user.</returns>
        public static User ParseUser(SqlDataReader reader)
        {
            return new User((int)reader["id"], (string)reader["name"], (string)reader["email"], (byte[])reader["pass"]);
        }

        /// <summary>
        /// Parse a role from a reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>The role.</returns>
        public static UserRole ParseRole(SqlDataReader reader)
        {
            return new UserRole((int)reader["userid"], (string)reader["role"]);
        }

    }
}
