using ProjectWebMVC5.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectWebMVC5.Controllers.api
{
    public class ProductsController : ApiController
    {
        private ApplicationDbContext _context;

        ProductsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.Include(x=>x.Categories).ToList();
        }

        [HttpGet]
        public IHttpActionResult GetProductByCategory(int id)
        {
            var Category = _context.Categories.SingleOrDefault(x => x.ID == id);
            if (Category == null)
                return NotFound();
            return Content(HttpStatusCode.OK, _context.Products.Where(x=>x.CategoryID==id).Include(x=>x.Categories).ToList());

        }

        [HttpPost]
        public IHttpActionResult CreateProduct(Product ct)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            _context.Products.Add(ct);
            _context.SaveChanges();
            return Created(new Uri(Request.RequestUri + "/" + ct.ID), ct);
        }

        [HttpPut]
        public IHttpActionResult UpdateProduct(Product ct, int id)
        {
            var ctInDb = _context.Products.SingleOrDefault(x => x.ID == id);
            if (ctInDb == null)
                return NotFound();
            ctInDb.Name = ct.Name;
            ctInDb.Description = ct.Description;
            ctInDb.CategoryID = ct.CategoryID;
            ct.NumberInStock = ct.NumberInStock;
            _context.SaveChanges();
            return Ok();

        }

        [HttpDelete]
        public IHttpActionResult DeleteCategory(int id)
        {
            var ctInDb = _context.Products.SingleOrDefault(x => x.ID == id);
            if (ctInDb == null)
                return NotFound();
            _context.Products.Remove(ctInDb);
            _context.SaveChanges();
            return Ok();
        }
    }
}
