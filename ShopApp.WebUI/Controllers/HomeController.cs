using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstract;
using ShopApp.Data.Abstract;
using ShopApp.WebUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Controllers
{
    public class HomeController: Controller
    {
        //private IProductRepository _productRepository;  //IPorductRepository çağrıldığı zaman biz bunun dolu versiyonunu(EfCoreProductRepository) çağırmalıyız. Startup>>ConfigureService -> services.AddScoped<IProductRepository, EfCoreProductRepository>();     
        private IProductService _productService;

        public HomeController(IProductService productService)
        {
            //this._productRepository = productRepository;
            this._productService = productService;
        }

        public IActionResult Index()
        {

            var productViewModel = new ProductListViewModel()
            {
                //Products = _productRepository.GetAll()
                Products = _productService.GetAll()
            };
            return View(productViewModel);


            //1------------------------------------------
            //var p_laptops = new List<Product>()
            //{
            //    new Product { Name = "Asus", Cost = 4000, Description = "4 GB RAM", IsApproved=false },
            //    new Product { Name = "Lenovo", Cost = 5000, Description = "8 GB RAM",IsApproved=true },
            //    new Product { Name = "Mpnster", Cost = 6000, Description = "8 GB RAM", IsApproved =true},
            //    new Product { Name = "Iphone11", Cost = 7000, Description = "16 GB RAM" } //default false
            //};

            //2------------------------------------------
            //var productViewModel = new ProductViewModel()
            //{
            //    //Products = p_laptops
            //    Products=ProductRepository.Products
            //};
            //return View(productViewModel);
 
        }


        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View("MyView");
        }
    }
}
