namespace Chibbis.TestProject.ProductManagementService.Application.Common
{
    public static class Errors
    {
        #region Product

        public static string ProductHasDuplicate(int? id = null)
        {
            return id != null ? $"Product with id:{id} already created" : "Product already created";
        }
        
        public static string ProductNotFound(int? id = null)
        {
            return id != null ? $"Product with id:{id} not found" : "Product not found";
        }

        #endregion
    }
}