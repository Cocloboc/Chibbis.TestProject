using System;

namespace Chibbis.TestProject.CartService.Application.Exceptions
{
    public class NotFoundException: Exception
    {
        public NotFoundException(string? message) : base(message)
        {
        }
    }
}