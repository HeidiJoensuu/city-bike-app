namespace Api.Exceptions
{
    public class InvalidInputException: Exception
    {
        public InvalidInputException(string key,int? number) : base($"Invalid number. {key}: {number}") { }
        public InvalidInputException(string key, int id, string name ) :base ($"Invalid station. {key}: id = {id}, name = {name}"){ }
        public InvalidInputException(string key, string? value) : base($"Invalid input. {key}: {value}") { }
    }
}
