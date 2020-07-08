using DataLayer;
using System.Linq;

namespace BusinessLogicLayer
{
    public class ProductStockManager
    {
        public void EditProductQuantity(InvoiceManager invoiceManager)
        {
            var ticket = invoiceManager.Ticket;
            foreach (var productLine in ticket.ProductLines)
            {
                var dbContext = new StockContext();
                var productToCheck = dbContext.Products.Where(p => p.Name == productLine.Product.Name).FirstOrDefault();

                if (productToCheck != null)
                {
                    var newProductQuantity = dbContext.ProductStocks.Where(p => p.ProductStockId == productToCheck.ProductId).FirstOrDefault();
                    newProductQuantity.Quantity -= productLine.Quantity;
                    dbContext.SaveChanges();
                }
            }
        }
    }
}