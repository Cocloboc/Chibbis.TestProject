using System;

namespace Chibbis.TestProject.ProductManagementService.Application.Exceptions
{
    public class HasDuplicateExceptions: Exception
    {
        public HasDuplicateExceptions(string? message) : base(message)
        {
        }
    }
}