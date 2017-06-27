﻿namespace CatiLyfe.DataLayer
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
        /// <returns>The post metadata</returns>
        Task<IEnumerable<PostMeta>> GetPostMetadata(int? top, int? skip, DateTime? startdate, DateTime? enddate);

        Task<IEnumerable<Post>> GetPost(int? top, int? skip, DateTime? startdate, DateTime? enddate);

        /// <summary>
        /// Get a single post
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns></returns>
        Task<Post> GetPost(int id);

        /// <summary>
        /// Get a single post
        /// </summary>
        /// <param name="slug">The slug</param>
        /// <returns></returns>
        Task<Post> GetPost(string slug);
    }
}
