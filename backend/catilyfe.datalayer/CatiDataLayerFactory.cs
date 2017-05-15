using System;
using System.Collections.Generic;
using System.Text;

namespace catilyfe.datalayer
{
    public static class CatiDataLayerFactory
    {
        public static ICatiDataLayer CreateDataLayer(string connectionstring)
        {
            return new CatiSqlDataLayer(connectionstring);
        }
    }
}
