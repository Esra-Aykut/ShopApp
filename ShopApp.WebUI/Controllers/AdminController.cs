using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopApp.WebUI.Models;
using ShopApp.Business.Abstract;
using ShopApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Controllers
{
    public class AdminController : Controller
    {
        
        private IProductService _productService;
        private ICategoryService _categoryService;
        
        public AdminController (IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }


        public IActionResult ProductList()
        {
            return View(new ProductListViewModel()
            {
                Products = _productService.GetAll()
            });
        }        
        
        public IActionResult CategoryList()
        {
            return View(new CategoryListViewModel()
            {
                Categories = _categoryService.GetAll()
            });
        }


        [HttpGet]
        public IActionResult ProductCreate()
        {
            return View();
        }


        [HttpPost]
        public IActionResult ProductCreate(ProductModel model)
        {
            var entity = new Product()
            {
                Name = model.Name,
                Url = model.Url,
                Cost = model.Cost,
                Description = model.Description,
                ImageUrl = model.ImageUrl
            };

            _productService.Create(entity);

            //TempData["message"] = $"{entity.Name} isimli ürün eklendi.";   
            var msg = new AlertMessage()
            {
                Message = $"{entity.Name} isimli ürün eklendi",
                AlertType = "success"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public IActionResult CategoryCreate()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CategoryCreate(CategoryModel model)
        {
            var entity = new Category()
            {
                Name = model.Name,
                Url = model.Url,
            };

            _categoryService.Create(entity);


            var msg = new AlertMessage()
            {
                Message = $"{entity.Name} isimli category eklendi",
                AlertType = "success"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("CategoryList");
        }


        [HttpGet]
        public IActionResult ProductEdit(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var entity = _productService.GetById((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            //bu aşamaya gelmişsek DB'de entity var. DB'den bu veriyi alıp, model şeklinde oluşturup syf'ya(Edit.cshtml) gönderelim.
            var model = new ProductModel()
            {
                ProductId = entity.ProductId,
                Name = entity.Name,
                Url = entity.Url,
                Cost = entity.Cost,
                ImageUrl = entity.ImageUrl,
                Description = entity.Description
            };

           return View(model);

        }       
        
        [HttpPost]
        public IActionResult ProductEdit(ProductModel model)
        {
            var entity = _productService.GetById(model.ProductId);
            if (entity == null)
            {
                return NotFound();
            }
            entity.Name = model.Name;
            entity.Cost = model.Cost;
            entity.Url = model.Url;
            entity.ImageUrl = model.ImageUrl;
            entity.Description = model.Description;

            _productService.Update(entity);

            //1) TempData["message"] = $"{entity.Name} isimli ürün güncellendi."; 

            //2)
            //TempData["message"] = new AlertMessage()
            //{
            //    Message = $"{entity.Name} isimli ürün güncellendi",
            //    AlertType = "success"
            //};

            //3---------------------------------------------------
            var msg = new AlertMessage()
            {
                Message = $"{entity.Name} isimli ürün güncellendi.",
                AlertType = "success"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);  
            //-----------------------------------------------------

            return RedirectToAction("ProductList");
        }



        [HttpGet]
        public IActionResult CategoryEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = _categoryService.GetById((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new ProductModel()
            {
                ProductId = entity.CategoryId,
                Name = entity.Name,
                Url = entity.Url
            };

            return View(model);

        }

        [HttpPost]
        public IActionResult CategoryEdit(CategoryModel model)
        {
            var entity = _categoryService.GetById(model.CategoryId);
            if (entity == null)
            {
                return NotFound();
            }

            entity.Name = model.Name;
            entity.Url = model.Url;

            _categoryService.Update(entity);
 
            var msg = new AlertMessage()
            {
                Message = $"{entity.Name} isimli ürün güncellendi.",
                AlertType = "success"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("ProductList");
        }


        public IActionResult DeleteProduct(int productId)
        {
            var entity = _productService.GetById(productId);
            if (entity != null)
            {
                _productService.Delete(entity);
            }

            //1) TempData["message"] = $"{entity.Name} isimli ürün silindi.";   
            //2:
            var msg = new AlertMessage()
            {
                Message = $"{entity.Name} isimli ürün silindi",
                AlertType = "danger"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("ProductList");
        }

        public IActionResult DeleteCategory(int categoryId)
        {
            var entity = _categoryService.GetById(categoryId);
           
            if (entity != null)
            {
                _categoryService.Delete(entity);
            }

            var msg = new AlertMessage()
            {
                Message = $"{entity.Name} isimli category silindi",
                AlertType = "danger"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("CategoryList");
        }





    }
}
