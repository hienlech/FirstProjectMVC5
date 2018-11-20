using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProjectWebMVC5.Models;

namespace ProjectWebMVC5.Controllers.api
{
    public class CategoriesController : ApiController
    {
        private ApplicationDbContext _context;

        CategoriesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        [HttpGet]
        public IHttpActionResult GetCategory(int id)
        {
            var Category = _context.Categories.SingleOrDefault(x => x.ID == id);
            if (Category == null)
                return NotFound();
            return Content(HttpStatusCode.Found, Category);

        }

        [HttpPost]
        public IHttpActionResult CreateCategory(Category ct)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            _context.Categories.Add(ct);
            _context.SaveChanges();
            return Created(new Uri(Request.RequestUri + "/" + ct.ID), ct);
        }

        [HttpPut]
        public IHttpActionResult UpdateCategory(Category ct, int id)
        {
            var ctInDb = _context.Categories.SingleOrDefault(x => x.ID == id);
            if (ctInDb == null)
                return NotFound();
            ctInDb.CategoryName = ct.CategoryName;
            _context.SaveChanges();
            return Ok();

        }

        [HttpDelete]
        public IHttpActionResult DeleteCategory(int id)
        {
            var ctInDb = _context.Categories.SingleOrDefault(x => x.ID == id);
            if (ctInDb == null)
                return NotFound();
            _context.Categories.Remove(ctInDb);
            _context.SaveChanges();
            return Ok();
        }
    }
}
