using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatiLyfe.Backend.Web.App_Start
{
    using CatiLyfe.DataLayer;

    public static class CatiDataLayer
    {
        /// <summary>
        /// Gets the datalayer.
        /// </summary>
        public static ICatiDataLayer Datalayer { get; private set; }

        public static ICatiAuthDataLayer AuthDataLayer { get; private set; }

        /// <summary>
        /// Initialize the data layer.
        /// </summary>
        /// <param name="connectionString"></param>
        public static void InitializeDataLayer(string connectionString)
        {
            CatiDataLayer.Datalayer = CatiDataLayerFactory.CreateDataLayer(connectionString);
            CatiDataLayer.AuthDataLayer = CatiDataLayerFactory.CreateAuthDataLayer(connectionString);
        }
    }
}