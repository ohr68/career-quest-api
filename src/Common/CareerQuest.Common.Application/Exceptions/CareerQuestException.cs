using CareerQuest.Common.Domain.Abstractions;

namespace CareerQuest.Common.Application.Exceptions;

public sealed class CareerQuestException(string requestName, Error? error = null, Exception? innerException = null)
    : Exception("Application exception", innerException)
{
    public string RequestName { get; } = requestName;
    public Error? Error { get; } = error;
}
