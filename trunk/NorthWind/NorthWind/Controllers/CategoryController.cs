using NorthWind.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthWind.Controllers
{
    public class CategoryController : Controller
    {
        //
        // GET: /Category/

        public ActionResult Index()
        {
            return View();
        }

        //
        // Details

        public ActionResult Details(int id)
        {
            using (var dao = new Entities())
            {
                CategoryRepository categoryRepository = new CategoryRepository(dao);
                Category category = categoryRepository.GetCategory(id);
                return View(category);
            }
        }

        public ActionResult GetImage(int id)
        {
            using (var dao = new Entities())
            {
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    CategoryRepository categoryRepository = new CategoryRepository(dao);
                    Category category = categoryRepository.GetCategory(id);
                    var image = category.Picture;
                    // 78 is the size of the OLE header for Northwind images
                    ms.Write(image, 78, image.Length - 78);

                    return File(ms.ToArray(), "image/jpeg");
                }
            }
        }

    }
}
