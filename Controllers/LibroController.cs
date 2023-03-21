
using FApiAutors.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace FApiAutors.Controllers
{
    public class LibroController:Controller
    {
        private readonly ILogger<LibroController> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public LibroController(ILogger<LibroController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _clientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(Autor autor)
        {

            var request = new HttpRequestMessage(HttpMethod.Get, "Libros");
            var client = _clientFactory.CreateClient("ApiAutor");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var libros = await response.Content.ReadAsStringAsync();
                ViewData["Libros"] = JsonConvert.DeserializeObject<Libro[]>(libros);

            }
            else
            {
                ViewData["Libros"] = new string[0];
            }

            return View();
        }


        //[HttpPost]
        public async Task<IActionResult> Create(Libro libro)
        {
            var client = _clientFactory.CreateClient("ApiAutores");
            var data = JsonConvert.SerializeObject(libro);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7205/api/libros", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "No se pudo crear el registro");
                return View(libro);
            }
        }

        //// [HttpPut]
        //public async Task<IActionResult> Edits(int id, Libro libro)
        //{
        //    var client = _clientFactory.CreateClient("ApiAutores");
        //    var data = JsonConvert.SerializeObject(libro);
        //    var content = new StringContent(data, Encoding.UTF8, "application/json");
        //    var response = await client.PutAsync($"https://localhost:7205/api/libros/{id}", content);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "No se pudo actualizar el registro");
        //        return View(libro);
        //    }
        //}
        ////[HttpDelete]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var client = _clientFactory.CreateClient("ApiAutores");
        //    var response = await client.DeleteAsync($"https://localhost:7205/api/libros/{id}", content);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "No se pudo eliminar el registro");
        //        return View();
        //    }
        //}

    }
}
