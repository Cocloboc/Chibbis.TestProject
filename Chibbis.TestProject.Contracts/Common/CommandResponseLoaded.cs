namespace Chibbis.TestProject.Contracts.Common
{
    public record CommandResponseLoaded<T>: CommandResponse
    {
        public T Data { get; init; }
    }
}