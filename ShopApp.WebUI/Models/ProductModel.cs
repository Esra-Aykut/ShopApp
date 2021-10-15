using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


//AddProduct'da kullanılıyor

    namespace ShopApp.WebUI.Models

{
    public class ProductModel
    {
        public int ProductId { get; set; }
        [Display(Name="Name", Prompt="Enter product name")]
        public string Name { get; set; }
        public string Url { get; set; }
        public double? Cost { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsApproved { get; set; }
    }
}
