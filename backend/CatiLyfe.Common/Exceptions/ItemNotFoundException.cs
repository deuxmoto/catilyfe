namespace CatiLyfe.Common.Exceptions
{
    public class ItemNotFoundException : CatiFailureException
    {
        public ItemNotFoundException(string message)
            : base(404, "NOT FOUND", message)
        {
        }
    }
}
