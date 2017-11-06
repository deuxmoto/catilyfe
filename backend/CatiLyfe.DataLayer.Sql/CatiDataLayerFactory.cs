namespace CatiLyfe.DataLayer.Sql
{
    public static class CatiDataLayerFactory
    {
        public static ICatiDataLayer CreateDataLayer(string connectionstring)
        {
            return new CatiSqlDataLayer(connectionstring);
        }

        public static ICatiAuthDataLayer CreateAuthDataLayer(string connectionString)
        {
            return new CatiSqlDataLayer(connectionString);
        }
    }
}
