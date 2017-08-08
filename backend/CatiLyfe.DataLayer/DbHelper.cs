namespace CatiLyfe.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.SqlServer.Server;

    /// <summary>
    /// The db helper.
    /// </summary>
    internal static class DbHelper
    {
        /// <summary>
        /// The to data table.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="getAttibutes">The get attibutes.</param>
        /// <param name="alterRecord">The alter record.</param>
        /// <typeparam name="T">the type of each element.</typeparam>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<SqlDataRecord> ToDataTable<T>(
            this IEnumerable<T> input,
            Func<SqlMetaData[]> getAttibutes,
            Action<SqlDataRecord, T> alterRecord)
        {
            if (null == input)
            {
                return null;
            }

            var enumerator = input.GetEnumerator();
            if (false == enumerator.MoveNext())
            {
                enumerator.Dispose();
                return null;
            }

            return DbHelper.ToDataTableHelper(enumerator, getAttibutes, alterRecord);
        }

        /// <summary>
        /// Lazy helper for get input.
        /// </summary>
        /// <typeparam name="T">The type of the input.</typeparam>
        /// <param name="input">The enumerator.</param>
        /// <param name="getAttibutes">Gets the attributes.</param>
        /// <param name="alterRecord">Alters the record.</param>
        /// <returns>An enumerable.</returns>
        private static IEnumerable<SqlDataRecord> ToDataTableHelper<T>(
            IEnumerator<T> input,
            Func<SqlMetaData[]> getAttibutes,
            Action<SqlDataRecord, T> alterRecord)
        {
            var metadatas = getAttibutes();
            var record = new SqlDataRecord(metadatas);
            do
            {
                alterRecord(record, input.Current);
                yield return record;
            }
            while (input.MoveNext());

            input.Dispose();
        }
    }
}
