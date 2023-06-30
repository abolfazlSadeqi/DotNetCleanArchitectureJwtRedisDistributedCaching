namespace Application.Common.Exceptions;

public class ValidationLogicException : Exception
{
    public ValidationLogicException()
        : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }


    public ValidationLogicException(List<Exception> failures)
        : this()
    {


        Errors = failures
            .GroupBy(e => e.Source == null ? "" : e.Source, e => e.Message)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());



    }

    public IDictionary<string, string[]> Errors { get; }
}
