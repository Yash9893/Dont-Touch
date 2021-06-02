using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
using ClassLibrary1;
//using System.Web.Mvc;

namespace WebApplication1.Controllers
{

    public class ProductController : ApiController
    {

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            using (ProductEntities1 entities = new ProductEntities1())
            {
                return entities.Products.ToList();
            }
        }
        public HttpResponseMessage Get(int id)
        {
            using (ProductEntities1 entities = new ProductEntities1())
            {
                var entity = entities.Products.FirstOrDefault<Product>(p => p.ID == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Product not found" + entity.ID.ToString());
                }
            }
        }

        [HttpPost]
        public HttpResponseMessage Post(Product product)
        {
            try
            {
                using (ProductEntities1 entities = new ProductEntities1())
                {
                    entities.Products.Add(product);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, product);
                    message.Headers.Location = new Uri(Request.RequestUri + "/" + product.ID.ToString());
                    return message;

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (ProductEntities1 entities = new ProductEntities1())
                {
                    var entity = entities.Products.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Item not found" + id.ToString());
                    }
                    else
                    {
                        entities.Products.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpPut]
        public HttpResponseMessage Put(int id, Product product)
        {
            try
            {
                using (ProductEntities1 entites = new ProductEntities1())
                {
                    var entity = entites.Products.FirstOrDefault(e => e.ID == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No item is present" + id.ToString());
                    }
                    else
                    {
                        entity.NAME = product.NAME;
                        entity.DESCRIPTION = product.DESCRIPTION;
                        entites.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


    }
}
