namespace Api.Exceptions
{
    public class MissingInputsException: Exception
    {
        public MissingInputsException(string key) : base ($"Missing input. {key}") { }
    }
}
