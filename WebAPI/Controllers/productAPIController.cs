using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;
using System.Data.Entity;

namespace WebAPI.Controllers
{
    public class ProductAPIController : ApiController
    {

        public IHttpActionResult GetAllProducts()
        {
            IList<Product> products = null;

            var ctx = new TrialCoreDBEntities();

            var queryToList = (from product in ctx.Products
                                select new
                               {
                                   ProductId = product.ProductId,
                                   ProductName = product.ProductName,
                                   ProductDescr = product.ProductDescr,
                                   ProductRating = product.ProductRating,
                                   ProductReview = product.ProductReview
                                           }).ToList();

            products = queryToList.Select(s => new Product
            {
                ProductId = s.ProductId,
                ProductName = s.ProductName,
                ProductDescr = s.ProductDescr,
                ProductRating = s.ProductRating,
                ProductReview = s.ProductReview
            }).ToList();

      

            if (products.Count == 0)
            {
                return NotFound();
            }

            return Ok(products);
        }

        //public IHttpActionResult GetAllProducts(int productid)
        //{
        //    IList<Product> products = null;

        //    using (var ctx = new TrialCoreDBEntities())
        //    {
        //        products = ctx.Products
        //            .Where(s => s.ProductId == productid)
        //            .Select(s => new Product()
        //            {
        //                ProductId = s.ProductId,
        //                ProductName = s.ProductName,
        //                ProductDescr = s.ProductDescr,
        //                ProductRating = s.ProductRating,
        //                ProductReview = s.ProductReview

        //            }).ToList<Product>();
        //    }

        //    if (products.Count == 0)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(products);
        //}


        public IHttpActionResult Post(Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new TrialCoreDBEntities())
            {
                ctx.Products.Add(new Product()
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductDescr = product.ProductDescr,
                    ProductRating = product.ProductRating,
                    ProductReview = product.ProductReview
                });

                ctx.SaveChanges();
            }

            return Ok();
        }


        public IHttpActionResult Put(Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new TrialCoreDBEntities())
            {
                var existingProduct = ctx.Products.Where(s => s.ProductId == product.ProductId)
                                                        .FirstOrDefault<Product>();

                if (existingProduct != null)
                {
                    existingProduct.ProductName = product.ProductName;
                    existingProduct.ProductDescr = product.ProductDescr;
                    existingProduct.ProductRating = product.ProductRating;
                    existingProduct.ProductReview = product.ProductReview;


                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid product id");

            using (var ctx = new TrialCoreDBEntities())
            {
                var product = ctx.Products
                    .Where(s => s.ProductId == id)
                    .FirstOrDefault();

                ctx.Entry(product).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }

    }
}
