using DataLayer;
using DataTransfertObject;
using System;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    public class ProductManager
    {
        public static Product selectedItem { get; private set; }
        public static ProductLine selectedProductToSell { get; private set; }

        public static List<Product> LoadProductsDataBase()
        {
            using (var dbContext = new StockContext())
            {
                List<Product> productAdded = new List<Product>();

                var products = dbContext.Products;

                foreach (var product in products)
                {
                    productAdded.Add(new Product()
                    {
                        ProductId = product.ProductId,
                        Reference = product.Reference,
                        Name = product.Name,
                        Price = product.Price,
                        Quantity = product.Quantity
                    });
                }
                return productAdded;
            }
        }

        public static Product AddANewProductByRefChecking(string newProductRef, string newProductName, string newProductPrice, string newProductQuantity)
        {
            Product newProduct = null;
            using (var dbContext = new StockContext())
            {
                var products = dbContext.Products;
                bool duplicate = false;

                foreach (var product in products)
                {
                    if (product.Reference.ToLower() == newProductRef.ToLower())
                    {
                        duplicate = true;
                        break;
                    }
                    newProduct = null;
                }
                if (!duplicate)
                {
                    var product = new Product()
                    {
                        Reference = newProductRef,
                        Name = newProductName,
                        Price = Convert.ToDouble(newProductPrice),
                        Quantity = Convert.ToInt32(newProductQuantity)
                    };
                    products.Add(product);
                    dbContext.SaveChanges();
                    newProduct = product;
                }
                return newProduct;
            }
        }

        public static List<Product> GetProductByPriceInterval(string textBoxMin, string textBoxMax)
        {
            List<Product> productAdded = new List<Product>();
            using (var dbContext = new StockContext())
            {
                var products = dbContext.Products;

                foreach (var product in products)
                {
                    if (product.Price >= Convert.ToDouble(textBoxMin) && product.Price <= Convert.ToDouble(textBoxMax))
                    {
                        productAdded.Add(new Product()
                        {
                            Reference = product.Reference,
                            Name = product.Name,
                            Price = product.Price,
                            Quantity = product.Quantity
                        });
                    }
                }
                return productAdded;
            }
        }

        public static List<Product> GetProductByGlobalResearch(string input)
        {
            List<Product> productAdded = new List<Product>();

            using (var dbContext = new StockContext())
            {
                var products = dbContext.Products;

                foreach (var product in products)
                {
                    if (product.Reference.ToString().ToLower().Contains(input.ToString().ToLower())
                        || product.Name.ToString().ToLower().Contains(input.ToString().ToLower())
                        || product.Price.ToString().ToLower().Contains(input.ToString().ToLower())
                        || product.Quantity.ToString().ToLower().Contains(input.ToString().ToLower()))
                    {
                        productAdded.Add(new Product()
                        {
                            Reference = product.Reference,
                            Name = product.Name,
                            Price = product.Price,
                            Quantity = product.Quantity
                        });
                    }
                }
                return productAdded;
            }
        }

        public static Product SelectAProductByRow(object selectedRow)
        {
            return selectedItem = (Product)selectedRow;
        }

        public static ProductLine SelectAProductToSellByRow(object selectedRow)
        {
            return selectedProductToSell = (ProductLine)selectedRow;
        }

        public static void RemoveAProductToDataBase(object obj)
        {
            using (var dbContext = new StockContext())
            {
                dbContext.Remove(obj);
                dbContext.SaveChanges();
            }
        }

        public static void UpdateAProduct(object obj)
        {
            using (var dbContext = new StockContext())
            {
                dbContext.Update(obj);
                dbContext.SaveChanges();
            }
        }
    }
}
