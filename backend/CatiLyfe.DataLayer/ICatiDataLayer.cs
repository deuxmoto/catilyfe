namespace CatiLyfe.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CatiLyfe.DataLayer.Models;

    /// <summary>
    /// The CatiDataLayer interface.
    /// </summary>
    public interface ICatiDataLayer
    {
        /// <summary>
        /// Gets all post metadata.
        /// </summary>
        /// <param name="top">The number of items to get.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="startdate">The start date.</param>
        /// <param name="enddate">The end date.</param>
        /// <param name="tags">The post tags.</param>
        /// <param name="isAdmin">Is the user an admin.</param>
        /// <returns>The post metadata</returns>
        Task<IEnumerable<PostMeta>> GetPostMetadata(int? top, int? skip, DateTime? startdate, DateTime? enddate, IEnumerable<string> tags, bool isAdmin = false);

        /// <summary>
        /// The get post.
        /// </summary>
        /// <param name="top">The top.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="startdate">The start date.</param>
        /// <param name="enddate">The end date.</param>
        /// <param name="tags">The list of tags to search.</param>
        /// <param name="isAdmin">Is the user an admin.</param>
        /// <returns>The list of posts.</returns>
        Task<IEnumerable<Post>> GetPost(int? top, int? skip, DateTime? startdate, DateTime? enddate, IEnumerable<string> tags, bool isAdmin = false);

        /// <summary>
        /// Get a single post
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="isAdmin">Is the user an admin.</param>
        /// <returns>A post</returns>
        Task<Post> GetPost(int id, bool isAdmin = false);

        /// <summary>
        /// Get a single post
        /// </summary>
        /// <param name="slug">The slug</param>
        /// <param name="isAdmin">Is the user an admin.</param>
        /// <returns>A post.</returns>
        Task<Post> GetPost(string slug, bool isAdmin = false);

        /// <summary>
        /// Gets all of the tags.
        /// </summary>
        /// <returns>The tags.</returns>
        Task<IEnumerable<PostTag>> GetTags();

        /// <summary>
        /// Set a post.
        /// </summary>
        /// <param name="post">The post.</param>
        /// <returns>An async task.</returns>
        Task<Post> SetPost(Post post);

        /// <summary>
        /// Delete a single post
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns>A task</returns>
        Task DeletePost(int id);

        /// <summary>
        /// Delete a single post
        /// </summary>
        /// <param name="slug">The slug</param>
        /// <returns>A task.</returns>
        Task DeletePost(string slug);
    }
}
