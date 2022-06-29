namespace Chibbis.TestProject.CartService.Application.Consumers.Cart.Events.CartDeleted
{
    public record CartDeletedEvent
    {
        public string UserUUID { get; init; }
    }
}