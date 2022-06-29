namespace Chibbis.TestProject.CartService.Application.Services
{
    public interface IUserContext
    {
        string UserUUID { get; }
        void SetUserUUID(string userUUID);
    }
}