namespace PE_Sharp.Exceptions;
public class NullException : Exception
{
    public NullException(string nullReference) : base($"{nullReference} is null") { }
}
