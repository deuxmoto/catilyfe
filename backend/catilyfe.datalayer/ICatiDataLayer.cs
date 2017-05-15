namespace catilyfe.datalayer
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using catilyfe.datalayer.Models;

    /// <summary>
    /// The CatiDataLayer interface.
    /// </summary>
    public interface ICatiDataLayer
    {
        /// <summary>
        /// Gets all post metadata.
        /// </summary>
        /// <returns>The post metadata</returns>
        Task<IEnumerable<PostMeta>> GetPostMetadata();
    }
}
