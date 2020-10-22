using DataLayer;
using DataTransfertObject;
using DataTransfertObject.DataGridView;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer
{
    public class ProductManager
    {
        public static InvoiceView selectedProductLine { get; private set; }

        public static Product AddANewProductByRefChecking(string newProductRef,
                                                          string newProductName,
                                                          string newProductExclTaxPrice,
                                                          string newProductPrice,
                                                          string newProductQuantity)
        {
            Product newProduct = null;
            var dbContext = new StockContext();
            newProduct = dbContext.Products.Where(c => c.Reference == newProductRef).FirstOrDefault();
            if (newProduct == null)
            {
                var product = new Product
                {
                    Reference = newProductRef,
                    Name = newProductName,
                    ExclTaxPrice = Convert.ToDouble(newProductExclTaxPrice),
                    Price = Convert.ToDouble(newProductPrice),
                    ProductStocks = new List<ProductStock>()
                    {
                        new ProductStock
                        {
                            Quantity = Convert.ToDouble(newProductQuantity)
                        }
                    }
                };
                dbContext.Products.Add(product);
                dbContext.SaveChanges();
                newProduct = product;
            }
            return newProduct;
        }

        public static List<ProductView> GetProductByPriceInterval(string textBoxMin, string textBoxMax)
        {
            List<ProductView> productViewsList = new List<ProductView>();
            var productsList = ProductViewManager.JoinProductAndProductStockTables();

            foreach (var product in productsList)
            {
                if (product.Price >= Convert.ToDouble(textBoxMin) && product.Price <= Convert.ToDouble(textBoxMax))
                {
                    productViewsList.Add(new ProductView()
                    {
                        Reference = product.Reference,
                        Name = product.Name,
                        ExclTaxPrice = product.ExclTaxPrice,
                        Price = product.Price,
                        Quantity = product.Quantity
                    });
                }
            }
            return productViewsList;
        }

        public static List<ProductView> GetProductByGlobalResearch(string input)
        {
            List<ProductView> productViewsList = new List<ProductView>();
            var productsList = ProductViewManager.JoinProductAndProductStockTables();

            foreach (var product in productsList)
            {
                if (product.Reference.ToString().ToLower().Contains(input.ToString().ToLower())
                    || product.Name.ToString().ToLower().Contains(input.ToString().ToLower())
                    || product.Price.ToString().ToLower().Contains(input.ToString().ToLower())
                    || product.Quantity.ToString().ToLower().Contains(input.ToString().ToLower()))
                {
                    productViewsList.Add(new ProductView()
                    {
                        Reference = product.Reference,
                        Name = product.Name,
                        ExclTaxPrice = product.ExclTaxPrice,
                        Price = product.Price,
                        Quantity = product.Quantity
                    });
                }
            }
            return productViewsList;
        }

        public static InvoiceView SelectAselectedProductLine(object selectedRow)
        {
            return selectedProductLine = (InvoiceView)selectedRow;
        }

        public static void RemoveAProductToDataBase(ProductView selectedItem)
        {
            Product productToDelete = null;
            ProductStock productStockToDelete = null;

            var dbContext = new StockContext();
            productToDelete = dbContext.Products.Where(c => c.ProductId == selectedItem.ProductId).FirstOrDefault();
            productStockToDelete = dbContext.ProductStocks.Where(c => c.ProductStockId == selectedItem.ProductId).FirstOrDefault();
            if (productToDelete != null && productStockToDelete != null)
            {
                dbContext.Remove(productToDelete);
                dbContext.Remove(productStockToDelete);
                dbContext.SaveChanges();
            }
        }

        public static void UpdateAProduct(ProductView selectedItem)
        {
            Product productToEdit = null;
            ProductStock productStockToEdit = null;

            var dbContext = new StockContext();
            productToEdit = dbContext.Products.Where(c => c.Reference == selectedItem.Reference).FirstOrDefault();
            productStockToEdit = dbContext.ProductStocks.Where(c => c.ProductStockId == selectedItem.ProductId).FirstOrDefault();

            if (productToEdit != null && productStockToEdit != null)
            {
                productToEdit.Reference = selectedItem.Reference;
                productToEdit.Name = selectedItem.Name;
                productToEdit.ExclTaxPrice = selectedItem.ExclTaxPrice;
                productToEdit.Price = selectedItem.Price;
                productStockToEdit.Quantity = selectedItem.Quantity;
                dbContext.Update(productStockToEdit);
                dbContext.Update(productToEdit);
                dbContext.SaveChanges();
            }
        }
    }
}
