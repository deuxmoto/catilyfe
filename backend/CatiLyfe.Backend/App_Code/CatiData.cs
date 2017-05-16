using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatiLyfe.Backend.App_Code
{
    using CatiLyfe.DataLayer;

    public static class CatiData
    {
        /// <summary>
        /// Gets the datalayer.
        /// </summary>
        public static ICatiDataLayer Datalayer {get; private set; }

        public static void InitializeDataLayer(string connectionString)
        {
            CatiData.Datalayer = CatiDataLayerFactory.CreateDataLayer(connectionString);
        }
    }
}
