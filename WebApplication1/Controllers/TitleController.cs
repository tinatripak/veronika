using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitleController : ControllerBase
    {
        // GET api/title/name
        [HttpGet("{name}")]
        public async Task<SearchBooks> Get(string name)
        {
            string key = "AIzaSyCfjBhpALhE_f3OxvGoDVJrLAolPYQ1I8c";
            string url = "https://www.googleapis.com/books/v1/volumes?q=" + name + "&langRestrict=en&maxResults=5&key=" + key;
            var result = await GetAsync(url);
            string m = result;
            SearchBooks obj = JsonConvert.DeserializeObject<SearchBooks>(m);
            return obj;
        }
        private async Task<string> GetAsync(string url)
        {
            HttpClient HttpClient = new HttpClient();
            var content = await HttpClient.GetStringAsync(url);
            return content;
        }
    }
}