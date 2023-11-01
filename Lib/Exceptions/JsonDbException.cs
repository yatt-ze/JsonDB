namespace JsonDB.Exceptions
{
    public class JsonDbException : Exception
    {
        public JsonDbException(string message) : base(message)
        {
        }

        public JsonDbException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}