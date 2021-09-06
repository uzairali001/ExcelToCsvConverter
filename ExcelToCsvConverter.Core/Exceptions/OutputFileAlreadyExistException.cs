using System.Runtime.Serialization;

namespace ExcelToCsvConverter.Core.Exceptions;

[Serializable]
internal class OutputFileAlreadyExistException : Exception
{
    public OutputFileAlreadyExistException()
    {
    }

    public OutputFileAlreadyExistException(string? message) : base(message)
    {
    }

    public OutputFileAlreadyExistException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected OutputFileAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}