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
    //[RoutePrefix("api/Ventas")]
    public class VentasController : ApiController
    {
        private ApiContext db = new ApiContext();

        // GET: api/Ventas
        public IQueryable<Ventas> GetVentas()
        {
            return db.Ventas;
        }

        // GET: api/Ventas/5
        [ResponseType(typeof(Ventas))]
        public IHttpActionResult GetVentas(int id)
        {
            //Ventas ventas = db.Ventas.Find(id);
            var ventas = db.Ventas.Include(x => x.ProductosVenta).Where(x => x.Id == id).FirstOrDefault();
            if (ventas == null)
            {
                return NotFound();
            }

            return Ok(ventas);
        }
        /*
        [HttpGet]
        [Route("Concepto/{idVenta}")]
        public IHttpActionResult GetVentaConcepto(int idVenta)
        {
            var ventas = db.Ventas.Include(x => x.ProductosVenta).Where(x => x.Id == idVenta).ToList();
            if (ventas == null)
            {
                return NotFound();
            }

            return Ok(ventas);
        }
        */

        // PUT: api/Ventas/5
        /*
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVentas(int id, Ventas ventas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ventas.Id)
            {
                return BadRequest();
            }

            db.Entry(ventas).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VentasExists(id))
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
        */
        // POST: api/Ventas
        [ResponseType(typeof(Ventas))]
        public IHttpActionResult PostVentas(ProductosVentasViewModel ventas)
        {
            int idSale = 0;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var lista = new List<ProductosVentas>();
                    var sale = new Ventas();
                    sale.Fecha = DateTime.Now;
                    sale.Total = 0;
                     
                    foreach (var item in ventas.Products)
                    {
                        int unidades = item.UnidadesVendidas;
                        var inventario = db.Productos.FirstOrDefault(x => x.Id == item.ProductosId);
                        int unidadesInv = inventario.Unidades;
                        
                        if (unidadesInv >= unidades)
                        {
                            var products = new ProductosVentas();
                            sale.Total = sale.Total + (inventario.Precio * item.UnidadesVendidas);
                            products.VentasId = idSale;
                            products.ProductosId = item.ProductosId;
                            products.UnidadesVendidas = item.UnidadesVendidas;
                            lista.Add(products);
                            //db.ProductosVentas.Add(products);
                            //db.SaveChanges();
                            //return CreatedAtRoute("DefaultApi", new { id = ventas.Id }, ventas);
                        }
                        else
                        {
                            return BadRequest("No hay suficiente inventario del producto con id " + item.ProductosId);

                        }
                    }
                    db.Ventas.Add(sale);
                    db.SaveChanges();
                    idSale = sale.Id;

                    foreach(var item in lista)
                    {
                        item.VentasId=idSale;
                        db.ProductosVentas.Add(item);
                    }
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                    //return StatusCode(HttpStatusCode.NoContent);
                    return CreatedAtRoute("DefaultApi", new { id = sale.Id }, sale);
                }
                catch(Exception ex)
                {
                    dbContextTransaction.Rollback();
                    Console.WriteLine(ex.Message);
                    return BadRequest("Fallo algo");
                }
            }
        }

        // DELETE: api/Ventas/5
        [ResponseType(typeof(Ventas))]
        public IHttpActionResult DeleteVentas(int id)
        {
            Ventas ventas = db.Ventas.Find(id);
            if (ventas == null)
            {
                return NotFound();
            }

            db.Ventas.Remove(ventas);
            db.SaveChanges();

            return Ok(ventas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VentasExists(int id)
        {
            return db.Ventas.Count(e => e.Id == id) > 0;
        }
    }
}