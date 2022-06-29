using System;

namespace Chibbis.TestProject.ProductManagementService.Application.Exceptions
{
    public class NotFoundException: Exception
    {
        public NotFoundException(string? message) : base(message)
        {
        }
    }
}