using Newtonsoft.Json;
using Refaccionaria.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Json;

namespace Refaccionaria.Controllers
{
    public class ProductosController : Controller
    {
        // GET: Productos
        
        public async Task<ActionResult> Index()
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("https://localhost:44351/api/Productos");
            var listProductos = JsonConvert.DeserializeObject<List<Productos>>(json);
            return View(listProductos);
        }

        // GET: Productos/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("https://localhost:44351/api/Productos/"+id);
            var Producto = JsonConvert.DeserializeObject<Productos>(json);
            return View(Producto);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Producto,Descripcion,Precio,Unidades")] Productos productos)
        {
            try
            {
                // TODO: Add insert logic here
                String url = "https://localhost:44351/api/Productos/";

                var jsonSerializerOption = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true};

                var httpClient = new HttpClient();
                var respuesta = await httpClient.PostAsJsonAsync(url,productos);
                if (respuesta.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(productos);
                }
                
            }
            catch
            {
                return View(productos);
            }
        }

        // GET: Productos/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("https://localhost:44351/api/Productos/"+id);
            var Producto = JsonConvert.DeserializeObject<Productos>(json);
            return View(Producto);
        }

        // POST: Productos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Producto,Descripcion,Precio,Unidades")] int id, Productos productos)
        {
            try
            {
                // TODO: Add update logic here
                String url = "https://localhost:44351/api/Productos/"+id;

                var jsonSerializerOption = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

                var httpClient = new HttpClient();
                var respuesta = await httpClient.PutAsJsonAsync(url,productos);
                if (respuesta.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(productos);
                }

                
            }
            catch
            {
                return View(productos);
            }
        }

        // GET: Productos/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("https://localhost:44351/api/Productos/"+id);
            var Producto = JsonConvert.DeserializeObject<Productos>(json);
            return View(Producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirm(int id)
        {
            try
            {
                // TODO: Add delete logic here
                String url = "https://localhost:44351/api/Productos/"+id;

                var jsonSerializerOption = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

                var httpClient = new HttpClient();
                var respuesta = await httpClient.DeleteAsync(url);
                if (respuesta.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Alert = "No se puede eliminar este producto";
                    return View("Delete");
                }

            }
            catch
            {
                ViewBag.Alert = "No se puede eliminar este producto";
                return View("Delete");
            }
        }
    }
}
