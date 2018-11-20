using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectWebMVC5.Models;

namespace ProjectWebMVC5.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index(int? id)
        {
            if (!id.HasValue)
            {
                return View(_context.Products.ToList());
            }
            return View(_context.Products.Where(x=>x.CategoryID==id).ToList());
        }

        public PartialViewResult CategoriesMenu()
        {
            
            return PartialView(
                _context.Categories.ToList()
                );
        }

        public ActionResult New()
        {
            ProductFormViewModel viewModel = new ProductFormViewModel()
            {
                Product = new Product(),
                Category = new Category()

            };
            return View("FormView", viewModel);
        }
        public ActionResult Save( ProductFormViewModel viewModel)
        {
            if(!ModelState.IsValid)
                return View("FormView", viewModel);
           
            var catInDb = _context.Categories.SingleOrDefault(x => x.CategoryName == viewModel.Category.CategoryName);
            if (catInDb == null)
            {
               
                _context.Categories.Add(viewModel.Category);
                _context.SaveChanges();
                viewModel.Product.CategoryID =
                    _context.Categories.Single(x => x.CategoryName == viewModel.Category.CategoryName).ID;
                _context.Products.Add(viewModel.Product);
                
            }
            else
            {
                viewModel.Product.CategoryID = catInDb.ID;
                _context.Products.Add(viewModel.Product);
            }
           
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var productInDb = _context.Products.Single(x => x.ID == id);
            var ViewModel = new ProductFormViewModel()
            {
                Product = productInDb,
                Category = _context.Categories.Single(x=>x.ID==productInDb.CategoryID)
                
            };
            return View("FormView", ViewModel);
        }
    }
}