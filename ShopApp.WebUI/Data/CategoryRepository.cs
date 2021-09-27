//using ShopApp.Entity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace ShopApp.WebUI.Data
//{
//    public static class CategoryRepository
//    {
//        static List<Category> _categories = null;
        
//        static CategoryRepository()
//        {
//            _categories = new List<Category>
//            {
//               new Category{ CategoryId=1, Name="Telefon", Description="Telefon Kategorisi"},
//               new Category{ CategoryId=2, Name="Laptop", Description="Laptop Kategorisi"},
//               new Category{ CategoryId=3, Name="Elektronik", Description="Elektronik Kategorisi"}
//            }; 
//        } 
        
//        public static List<Category> Categories
//        {
//            get { return _categories; }
//        }
         

//        public static void AddCategory(Category c)
//        {
//            _categories.Add(c);
//        }

//        public static Category GetCategoryById(int id)
//        {
//            return _categories.FirstOrDefault(c => c.CategoryId == id);
//        }


//    }
//}
