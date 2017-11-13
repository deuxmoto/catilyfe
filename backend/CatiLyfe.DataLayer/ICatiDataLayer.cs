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
        /// <param name="includeUnpublished">Include the unpublished ones.</param>
        /// <param name="includeDeleted">Include the deleted ones.</param>
        /// <returns>The post metadata</returns>
        Task<IEnumerable<PostMeta>> GetPostMetadata(int? top, int? skip, DateTime? startdate, DateTime? enddate, bool includeUnpublished, bool includeDeleted, IEnumerable<string> tags);

        /// <summary>
        /// The get post.
        /// </summary>
        /// <param name="top">The top.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="startdate">The start date.</param>
        /// <param name="enddate">The end date.</param>
        /// <param name="tags">The list of tags to search.</param>
        /// <param name="includeUnpublished">Include the unpublished ones.</param>
        /// <param name="includeDeleted">Include the deleted ones.</param>
        /// <returns>The list of posts.</returns>
        Task<IEnumerable<Post>> GetPost(int? top, int? skip, DateTime? startdate, DateTime? enddate, bool includeUnpublished, bool includeDeleted, IEnumerable<string> tags);

        /// <summary>
        /// Get a single post
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="includeUnpublished">Include the unpublished ones.</param>
        /// <param name="includeDeleted">Include the deleted ones.</param>
        /// <returns>A post</returns>
        Task<Post> GetPost(int id, bool includeUnpublished, bool includeDeleted);

        /// <summary>
        /// Get a single post
        /// </summary>
        /// <param name="slug">The slug</param>
        /// <param name="includeUnpublished">Include the unpublished ones.</param>
        /// <param name="includeDeleted">Include the deleted ones.</param>
        /// <returns>A post.</returns>
        Task<Post> GetPost(string slug, bool includeUnpublished, bool includeDeleted);

        /// <summary>
        /// Gets all of the tags.
        /// </summary>
        /// <returns>The tags.</returns>
        Task<IEnumerable<PostTag>> GetTags();

        /// <summary>
        /// Set a post.
        /// </summary>
        /// <param name="post">The post.</param>
        /// <param name="userAccessDetails">The user acess details.</param>
        /// <returns>An async task.</returns>
        Task<Post> SetPost(Post post, UserAccessDetails userAccessDetails);

        /// <summary>
        /// Delete a single post
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="userAccessDetails">The user acess details.</param>
        /// <returns>A task</returns>
        Task DeletePost(int id, UserAccessDetails userAccessDetails);

        /// <summary>
        /// Delete a single post
        /// </summary>
        /// <param name="slug">The slug</param>
        /// <param name="userAccessDetails">The user acess details.</param>
        /// <returns>A task.</returns>
        Task DeletePost(string slug, UserAccessDetails userAccessDetails);
    }
}
