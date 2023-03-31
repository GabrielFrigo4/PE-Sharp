namespace PE_Sharp.Exceptions;
public class ArgException : Exception
{
    public ArgException(string nullReference) : base($"Argument Exception: {nullReference}") { }
}
