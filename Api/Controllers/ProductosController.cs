using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Api.Data;
using Api.Models;

namespace Api.Controllers
{
    public class ProductosController : ApiController
    {
        private ApiContext db = new ApiContext();

        // GET: api/Productos
        public IEnumerable<Productos> GetProductos()
        {
   
            return db.Productos;
        }

        // GET: api/Productos/5
        [ResponseType(typeof(Productos))]
        public IHttpActionResult GetProductos(int id)
        {
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return NotFound();
            }
        
            return Ok(productos);
        }

        // PUT: api/Productos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductos(int id, Productos productos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productos.Id)
            {
                return BadRequest();
            }

            db.Entry(productos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Productos
        [ResponseType(typeof(Productos))]
        public IHttpActionResult PostProductos(Productos productos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Productos.Add(productos);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productos.Id }, productos);
        }

        // DELETE: api/Productos/5
        [ResponseType(typeof(Productos))]
        public IHttpActionResult DeleteProductos(int id)
        {
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return NotFound();
            }
            try
            {
                db.Productos.Remove(productos);
                db.SaveChanges();

                return Ok(productos);
            }
            catch (Exception)
            {
                return BadRequest("No se puede borrar este producto");
            }
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductosExists(int id)
        {
            return db.Productos.Count(e => e.Id == id) > 0;
        }
    }
}