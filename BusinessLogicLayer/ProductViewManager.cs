using DataLayer;
using DataTransfertObject;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer
{
    public class ProductViewManager
    {
        public static ProductView CurrentItemSelected { get; private set; }

        public static List<ProductView> JoinProductAndProductStockTables()
        {
            var dbContext = new StockContext();
            var Join = (from p in dbContext.Products
                        join s in dbContext.ProductStocks
                        on p.ProductId equals s.ProductStockId
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

        public ProductView SelectAProductByRow(ProductView currentRow)
        {
            return CurrentItemSelected = currentRow;
        }

        public ProductView SelectAProductByRow(object currentRow)
        {
            return CurrentItemSelected = (ProductView)currentRow;
        }
    }
}
