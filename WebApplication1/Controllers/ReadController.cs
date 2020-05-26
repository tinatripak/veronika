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
    public class ReadController : ControllerBase
    {
        // GET api/values/inauthorName
        [HttpGet("{name}")]
        public async Task<SearchBooks> Get(string name)
        {
            string key = "AIzaSyCfjBhpALhE_f3OxvGoDVJrLAolPYQ1I8c";
            string url = "https://www.googleapis.com/books/v1/volumes?q=" + name + "&langRestrict=en&filter=free-ebooks&maxResults=5&%20key=" + key;
            var result1 = await GetAsync(url);
            string m = result1;
            SearchBooks obj1 = JsonConvert.DeserializeObject<SearchBooks>(m);
            return obj1;
        }
        private async Task<string> GetAsync(string url)
        {
            HttpClient client = new HttpClient();
            var content = await client.GetStringAsync(url);
            return content;
        }
    }
}