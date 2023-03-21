using FApiAutors.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace FApiAutors.Controllers
{
    public class AutorController:Controller
    {
        private readonly ILogger<AutorController> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public AutorController(ILogger<AutorController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _clientFactory = httpClientFactory;
        }

            // GET: Autor2Controller
            public async Task<IActionResult> Index()
            {

            var request = new HttpRequestMessage(HttpMethod.Get, "Autores");
            var client = _clientFactory.CreateClient("ApiAutores");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var autores = await response.Content.ReadAsStringAsync();
                ViewData["Autores"] = JsonConvert.DeserializeObject<Autor[]>(autores);
            }
            else
            {
                ViewData["Autores"] = new string[0];
            }

            return View();
            }


        //[HttpPost]
        public async Task<IActionResult> Create(Autor autor)
        {
            var client = _clientFactory.CreateClient("ApiAutores");
            var data = JsonConvert.SerializeObject(autor);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7205/Autores", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "No se pudo crear el registro");
                return View(autor);
            }
        }

       // [HttpPut]
        public async Task<IActionResult> Edits(int id, Autor autor)
        {
            var client = _clientFactory.CreateClient("ApiAutores");
            var data = JsonConvert.SerializeObject(autor);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"https://localhost:7205/Autores/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "No se pudo actualizar el registro");
                return View(autor);
            }
        }
        //[HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _clientFactory.CreateClient("ApiAutores");
            var response = await client.DeleteAsync($"https://localhost:7205/Autores/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "No se pudo eliminar el registro");
                return View();
            }
        }



    }
}
