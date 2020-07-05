using DataLayer;
using DataTransfertObject;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer
{
    public class AlertWindowManager
    {
        public List<ProductView> AddProductsInRows()
        {
            var dbContext = new StockContext();
            var Join = (from p in dbContext.Products
                        join s in dbContext.ProductStocks
                        on p.ProductId equals s.ProductStockId
                        where s.Quantity <= 10
                        select new ProductView
                        {
                            ProductId = p.ProductId,
                            Reference = p.Reference,
                            Name = p.Name,
                            Price = p.Price,
                            Quantity = s.Quantity
                        }).ToList();
            return Join;
        }
    }
}
