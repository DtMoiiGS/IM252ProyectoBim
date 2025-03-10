using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using Domain;
using Infrastructure;
using Application;
using Microsoft.AspNetCore.Http;

namespace Presentation.WebApp.Controllers
{
    public class IM252ProductosController : Controller
    {
        private readonly IM252ProductoDbContext IM252productoDbContext;
        public IM252ProductosController(IConfiguration configuration)
        {
            IM252productoDbContext = new IM252ProductoDbContext(configuration.GetConnectionString("DefaultConnection"));
        }

        public IActionResult Index()
        {
            var data = IM252productoDbContext.List();
            
            return View(data);
        }

        public IActionResult Details(Guid id)
        {
            var data = IM252productoDbContext.Details(id);
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(IM252Producto data, IFormFile file)
        {
            if (file != null)
            {
                data.Foto = FileConverterService.ConvertToBase64(file.OpenReadStream());
            }

            IM252productoDbContext.Create(data);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid id)
        {
            var data = IM252productoDbContext.Details(id);
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(IM252Producto data, IFormFile file)
        {
            if (file != null)
            {
                data.Foto = FileConverterService.ConvertToBase64(file.OpenReadStream());
            }

            IM252productoDbContext.Edit(data);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(Guid id)
        {
            var producto = IM252productoDbContext.Details(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }
        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var producto = IM252productoDbContext.Details(id);
            if (producto == null)
            {
                return NotFound();
            }
            IM252productoDbContext.Delete(id);  
            return RedirectToAction("Index");  
        }

    }
}