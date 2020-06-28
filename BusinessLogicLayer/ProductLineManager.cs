using DataLayer;
using DataTransfertObject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BusinessLogicLayer
{
    public class ProductLineManager
    {
        public static void SaveProductLine(ObservableCollection<ProductLine> productLinesList)
        {
            using (var dbContext = new StockContext())
            {
                var productLines = dbContext.ProductLines;
                foreach (var productLine in productLinesList)
                {
                    productLines.Add(productLine);
                }
                dbContext.SaveChanges();
            }
        }
    }
}
