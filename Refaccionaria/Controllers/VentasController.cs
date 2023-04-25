using Newtonsoft.Json;
using Refaccionaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Refaccionaria.Controllers
{
    public class VentasController : Controller
    {
        // GET: Ventas
        public async Task<ActionResult> Index()
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("https://localhost:44351/api/Ventas");
            var listVentas = JsonConvert.DeserializeObject<List<Ventas>>(json);
            return View(listVentas);
        }

        // GET: Ventas/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("https://localhost:44351/api/Ventas/"+id);
            var venta = JsonConvert.DeserializeObject<Ventas>(json);

            var json2 = await httpClient.GetStringAsync("https://localhost:44351/api/Productos");
            var listProductos = JsonConvert.DeserializeObject<List<Productos>>(json2);

            var productosComprados = new List<Productos>();
            var tabla = new List<tablaDetailsViewModel>();
            foreach (var item in venta.ProductosVenta)
            {
                var p = listProductos.Find(x => x.Id == item.ProductosId);
                var fila = new tablaDetailsViewModel();
                fila.id = item.ProductosId;
                fila.unidades = item.UnidadesVendidas;
                fila.name = p.Producto;
                fila.precio = p.Precio;
                fila.subtotal = item.UnidadesVendidas * p.Precio;
                tabla.Add(fila);
            }

            

            ViewBag.ProductoVenta = tabla;

            return View(venta);
        }

        // GET: Ventas/Create
        public async Task<ActionResult> Create()
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("https://localhost:44351/api/Productos");
            var listProductos = JsonConvert.DeserializeObject<List<Productos>>(json);
            ViewBag.ProductoVenta = listProductos;
            return View();
          
        }

        // POST: Ventas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Create([Bind(Include = "ProductosId,UnidadesVendidas")] VentasViewModel ventas)
        {
            
            try
            {

                var viewModel = new ProductosVentasViewModel();
                var lista = new List<ProductosVentasView>();
       

                if (ventas.ProductosId.Count == ventas.UnidadesVendidas.Count) {
                    for (int i = 0; i < ventas.ProductosId.Count; i++)
                    {
                        var prod = new ProductosVentasView();
                        prod.ProductosId = ventas.ProductosId[i];
                        prod.UnidadesVendidas = ventas.UnidadesVendidas[i];
                        lista.Add(prod);
                    }

                    viewModel.Products= lista;

                    // TODO: Add insert logic here
                    String url = "https://localhost:44351/api/Ventas/";

                    var prodVendido = new Ventas();
                    var jsonSerializerOption = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var httpClient = new HttpClient();
                    //for(int i=0; i<ventas.ProductosId.Count; i++)
                    //{
                    //prodVendido.ProductosId = ventas.ProductosId[i];
                    //prodVendido.UnidadesVendidas = ventas.UnidadesVendidas[i];
                    //prodVendido.Total = 0;

                    var respuesta = await httpClient.PostAsJsonAsync(url, viewModel);
                    if (respuesta.IsSuccessStatusCode)
                    {

                    }
                    else
                    {
                        ViewBag.Alert = "Ocurrio un error";
                        return RedirectToAction("Create");
                    }
                    //}
                }
                ViewBag.Alert = "Registro Guardado";
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Alert = "Ocurrio un error";
                return RedirectToAction("Create");
            }
            
        }

        /*  
        public async Task<ActionResult> Create([Bind(Include = "ProductosId,UnidadesVendidas,Total")] Ventas ventas)
        {
            try
            {
                // TODO: Add insert logic here
                String url = "https://localhost:44351/api/Ventas/";

                var jsonSerializerOption = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

                var httpClient = new HttpClient();
                var respuesta = await httpClient.PostAsJsonAsync(url, ventas);
                if (respuesta.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Alert = "No hay suficiente Inventario";
                    return View(ventas);
                }

            }
            catch
            {
                return View(ventas);
            }
        }
        */

        // GET: Ventas/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("https://localhost:44351/api/Ventas/"+id);
            var venta = JsonConvert.DeserializeObject<Ventas>(json);
            return View(venta);
        }

        // POST: Ventas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UnidadesVendidas,Total")] int id, Ventas ventas)
        {
            try
            {
                // TODO: Add update logic here
                String url = "https://localhost:44351/api/Ventas/"+id;

                var jsonSerializerOption = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

                var httpClient = new HttpClient();
                var respuesta = await httpClient.PutAsJsonAsync(url, ventas);
                if (respuesta.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(ventas);
                }


            }
            catch
            {
                return View(ventas); 
            }
        }

        // GET: Ventas/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("https://localhost:44351/api/Ventas/"+id);
            var venta = JsonConvert.DeserializeObject<Ventas>(json);
            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirm(int id)
        {
            try
            {
                // TODO: Add delete logic here
                String url = "https://localhost:44351/api/Ventas/"+id;

                var jsonSerializerOption = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

                var httpClient = new HttpClient();
                var respuesta = await httpClient.DeleteAsync(url);
                if (respuesta.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Alert = "No se puede eliminar esta venta";
                    return View("Delete");
                }

            }
            catch
            {
                ViewBag.Alert = "No se puede eliminar esta venta";
                return View("Delete");
            }
        }
    }
}
