namespace CatiLyfe.DataLayer
{
    public static class CatiDataLayerFactory
    {
        public static ICatiDataLayer CreateDataLayer(string connectionstring)
        {
            return new CatiSqlDataLayer(connectionstring);
        }
    }
}
