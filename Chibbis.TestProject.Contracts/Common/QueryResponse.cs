namespace Chibbis.TestProject.Contracts.Common
{
    public record QueryResponse<T>
    {
        public T Data { get; init; }
    }
}