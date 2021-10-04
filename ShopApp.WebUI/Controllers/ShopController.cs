using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstract;
using ShopApp.Entity;
using ShopApp.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Controllers
{
    public class ShopController : Controller
    {
        private IProductService _productService;
        public ShopController(IProductService productService)
        {
            this._productService = productService;
        }




        // localhost/products/telefon?page=1
        // localhost/products/telefon?page=1&color=red


        public IActionResult List(string category, int page=1)
        {
            const int pageSize = 3; //her syfda kaç ürün görünecek
            var productViewModel = new ProductListViewModel()
            {
                PageInfo = new PageInfo()  
                {
                    //bu bilgilerin hepsini PageInfoda paketleyip View üzerine göndereceğiz.
                    TotalItems = _productService.GetCountByCategory(category),
                    CurrentPage = page,
                    ItemsPerPage = pageSize,      
                    CurrentCategory = category
                },
                Products = _productService.GetProductsByCategory(category, page, pageSize)
            };

            Console.WriteLine("list metooduuu");
            return View(productViewModel);
        }




        #region id'ye göre ürün sorgulaması:
        //public IActionResult Details(int? id) // parametre->nullable. Her zaman id gelmez.
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    Product  product = _productService.GetProductDetails((int)id); 

        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(new ProductDetailModel{
        //        Product = product,
        //        Categories = product.ProductCategories.Select(i=>i.Category).ToList() 
        //    });
        //}
        #endregion

        #region 
        //artık ürün sorgulamasını id'ye göre değil url'e göre yapacağız.startup'da route'u tanımla.
        public IActionResult Details(string url)  
        {
            if (url == null)
            {

                return NotFound();
            }
            Product product = _productService.GetProductDetails(url);

            if (product == null)
            {
                return NotFound("shop--> details");
            }

            return View(new ProductDetailModel
            {
                Product = product,
                Categories = product.ProductCategories.Select(i => i.Category).ToList()
            });
        }
        #endregion




        public IActionResult Search(string q)
        {
            var productViewModel = new ProductListViewModel()
            {
                Products = _productService.GetSearchResult(q)  //getSearchResult'a q'yu gönderelim ve dönen bilgiyi return ile direkt syf'ya(Shop>>Search) taşıyalım.
            };

            return View(productViewModel);
        }

    }
}
