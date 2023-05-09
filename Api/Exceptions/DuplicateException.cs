namespace Api.Exceptions
{
    public class DuplicateException : Exception
    {
        public DuplicateException(string key, string duplicate) : base($"{key}: {duplicate} is already in use.") { }
    }
}
