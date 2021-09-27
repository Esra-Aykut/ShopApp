//using ShopApp.Entity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace ShopApp.WebUI.Data
//{
//    public static class ProductRepository //veri tabanı görevi görecek. Örnği oluşturulmasın=>static
//    {
//        private static List<Product> _products = null;

//        static ProductRepository() {
//            _products = new List<Product>
//            {
//              new Product { ProductId=1, Name = "Iphone7", Cost = 4000, Description = "iyi telefon", IsApproved=false, ImageUrl="1.jpg", CategoryId=1 },
//              new Product { ProductId=2, Name = "Iphone8", Cost = 5000, Description = "çok iyi telefon",IsApproved=true, ImageUrl="2.jpg", CategoryId=1 },
//              new Product { ProductId=3, Name = "IphoneX", Cost = 6000, Description = "hoş telefon", IsApproved =true, ImageUrl="3.jpg", CategoryId=1},
//              new Product { ProductId=4, Name = "Iphone11", Cost = 7000, Description = "güzel telefon", ImageUrl="4.jpg", CategoryId=1 }, //default false
            
//              new Product { ProductId=5, Name = "Lenovo7", Cost = 4000, Description = "iyi bilgisayar", IsApproved=false, ImageUrl="1.jpg", CategoryId=2 },
//              new Product { ProductId=6, Name = "Lenovo8", Cost = 5000, Description = "çok iyi bilgisayar",IsApproved=true, ImageUrl="2.jpg", CategoryId=2 },

//              new Product { ProductId=7, Name = "LenovoX", Cost = 6000, Description = "iyi bilgisayar \n category:3", IsApproved =true, ImageUrl="3.jpg", CategoryId=3},
//              new Product { ProductId=8, Name = "Lenovo11", Cost = 7000, Description = "çok iyi bilgisayar \n category:3", ImageUrl="4.jpg", CategoryId=3 },
//            };
//        }

//        public static List<Product> Products
//        {
//            get { return _products; }
//        }

//        public static void AddProduct(Product product)
//        {
//            _products.Add(product);
//        }

//        public static Product GetProductById(int id)
//        {
//            return _products.FirstOrDefault(p => p.ProductId == id);
//        }

//        public static void EditProduct(Product product)
//        {
//            foreach (var p in _products)
//            {
//                if (p.ProductId == product.ProductId)  //Edit.cshtml'ye:  <input type="hidden" name="ProductId" value="@Model.ProductId" />
//                {
//                    p.Name = product.Name;
//                    p.Cost = product.Cost;
//                    p.ImageUrl = product.ImageUrl;
//                    p.Description = product.Description;
//                    p.IsApproved = product.IsApproved;
//                    p.CategoryId = product.CategoryId;
//                }
//            }
//        }

//        public static void DeleteProduct(int id)
//        {
//            var product = GetProductById(id);
//            if (product != null)
//            {
//                _products.Remove(product);
//            }
//        }

//    }
//}
