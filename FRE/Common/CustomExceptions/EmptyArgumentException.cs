namespace Common.CustomExceptions
{
    public class EmptyArgumentException : Exception
    {
        public EmptyArgumentException(string message) : base(message) { }
    }
}
