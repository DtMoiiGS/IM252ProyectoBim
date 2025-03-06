using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Presentation.WebApp.Models;

namespace Presentation.WebApp.Controllers
{
    public class VentasController : Controller
    {
        private static List<Venta> ventas = new List<Venta>
        {
            new Venta { Id = 1, ClienteNombre = "Juan Pérez", Producto = "Laptop", Precio = 15000 },
            new Venta { Id = 2, ClienteNombre = "María López", Producto = "Celular", Precio = 8000 }
        };

        public IActionResult Index()
        {
            return View(ventas);
        }

        public IActionResult Details(int id)
        {
            var venta = ventas.FirstOrDefault(v => v.Id == id);
            if (venta == null) return NotFound();

            return View(venta);
        }

        public IActionResult Edit(int id)
        {
            var venta = ventas.FirstOrDefault(v => v.Id == id);
            if (venta == null) return NotFound();
            return View(venta);
        }

        [HttpPost]
        public IActionResult Edit(int id, Venta ventaEditada)
        {
            var venta = ventas.FirstOrDefault(v => v.Id == id);
            if (venta == null) return NotFound();

            venta.ClienteNombre = ventaEditada.ClienteNombre;
            venta.Producto = ventaEditada.Producto;
            venta.Precio = ventaEditada.Precio;

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var venta = ventas.FirstOrDefault(v => v.Id == id);
            if (venta == null) return NotFound();

            return View(venta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var venta = ventas.FirstOrDefault(v => v.Id == id);
            if (venta != null)
            {
                ventas.Remove(venta);
            }

            return RedirectToAction("Index");
        }
    }
}
