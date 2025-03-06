using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Presentation.WebApp.Models;

namespace Presentation.WebApp.Controllers
{
    public class ClientesController : Controller
    {
        private static List<Cliente> clientes = new List<Cliente>
        {
            new Cliente { Id = 1, Nombre = "Juan Pérez", Direccion = "Calle 123, CDMX", Telefono = "555-123-4567" },
            new Cliente { Id = 2, Nombre = "María López", Direccion = "Av. Reforma 456, CDMX", Telefono = "555-987-6543" }
        };

        public IActionResult Index()
        {
            return View(clientes);
        }

        public IActionResult Details(int id)
        {
            var cliente = clientes.FirstOrDefault(c => c.Id == id);
            if (cliente == null) return NotFound();

            return View(cliente);
        }

        public IActionResult Create()
{
    return View();
}

[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Create(Cliente nuevoCliente)
{
    if (ModelState.IsValid)
    {
        nuevoCliente.Id = clientes.Count > 0 ? clientes.Max(c => c.Id) + 1 : 1;
        clientes.Add(nuevoCliente);
        return RedirectToAction("Index");
    }
    
    return View(nuevoCliente);
}


        public IActionResult Edit(int id)
        {
            var cliente = clientes.FirstOrDefault(c => c.Id == id);
            if (cliente == null) return NotFound();
            return View(cliente);
        }

        [HttpPost]
        public IActionResult Edit(int id, Cliente clienteEditado)
        {
            var cliente = clientes.FirstOrDefault(c => c.Id == id);
            if (cliente == null) return NotFound();

            cliente.Nombre = clienteEditado.Nombre;
            cliente.Direccion = clienteEditado.Direccion;
            cliente.Telefono = clienteEditado.Telefono;

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var cliente = clientes.FirstOrDefault(c => c.Id == id);
            if (cliente == null) return NotFound();

            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var cliente = clientes.FirstOrDefault(c => c.Id == id);
            if (cliente != null)
            {
                clientes.Remove(cliente);
            }

            return RedirectToAction("Index");
        }
    }
}
