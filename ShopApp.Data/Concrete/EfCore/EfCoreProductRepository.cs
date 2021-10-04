using Microsoft.EntityFrameworkCore;
using ShopApp.Data.Abstract;
using ShopApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Data.Concrete.EfCore
{
    public class EfCoreProductRepository : EfCoreGenericRepository<Product, ShopContext>, IProductRepository
    {
        public int GetCountByCategory(string category)
        {
            using (var context = new ShopContext())
            {
                var products = context.Products.Where(i=>i.IsApproved).AsQueryable();    
                if (!string.IsNullOrEmpty(category))
                {
                    products = products
                                    .Include(i => i.ProductCategories)
                                    .ThenInclude(i => i.Category)
                                    .Where(i => i.ProductCategories.Any(a => a.Category.Url == category));
                }
                return products.Count();
            }
        }

 

        // Bu eski. Artık ürün sorgulamasını id'ye göre değil url'ye göre yapacağız.startup'da route'u tanımla.
        public Product GetProductDetails(string url) //"int id" yerine -> "string url" yaz
        {
            using (var context = new ShopContext())
            {
                //Left Join: "Product" tablosu + ordan "ProductCategory" tablosu
                return context.Products
                    .Where(i => i.Url == url)  
                    .Include(i => i.ProductCategories)
                    .ThenInclude(i => i.Category)
                    .FirstOrDefault();
            }
        }




        //herhangi bir değer gelmediğinde page default=1
       public List<Product> GetProductsByCategory(string name, int page, int pageSize)
        {
            using (var context = new ShopContext())
            {
                var products = context
                    .Products
                    .Where(i=>i.IsApproved)
                    .AsQueryable();  //AsQueryable: sorguyu yazıyoruz ama veri tabanına göndermeden önce üzerine ekstradan bir kriter&linq sorgusu eklemek istiyoruz. 
               
                if (!string.IsNullOrEmpty(name))
                {
                    products = products
                                    .Include(i => i.ProductCategories)
                                    .ThenInclude(i => i.Category)
                                    .Where(i => i.ProductCategories.Any(a => a.Category.Url == name));
                }
                //return products.ToList();
                //return products.Skip(5).Take(5).ToList(); //ilk 5 ürünü atla,sonraki 5 ürünü  al
                return products.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            }
        }
 


        public List<Product> GetHomePageProducts()
        {
            using (var context = new ShopContext())
            {
                return context.Products
                    .Where(i=>i.IsApproved && i.IsHome).ToList();
            }
        }

        public List<Product> GetSearchResult(string searchString)
        {
            using (var context = new ShopContext())
            {
                var products = context
                    .Products
                    .Where(i => i.IsApproved && (i.Name.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())))
                    .AsQueryable();   
                return products.ToList();
            }
        }
    }
}
