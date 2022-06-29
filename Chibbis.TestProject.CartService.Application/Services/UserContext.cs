using System;

namespace Chibbis.TestProject.CartService.Application.Services
{
    public class UserContext : IUserContext
    {
        public string UserUUID { get; private set; }

        public void SetUserUUID(string userUUID)
        {
            UserUUID = userUUID;
        }
    }
}