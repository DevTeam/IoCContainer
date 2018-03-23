namespace WebApplication.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using ConsoleApp;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public class BoxesController : Controller
    {
        private readonly Func<IBox<ICat>> _boxFactory;

        public BoxesController(Func<IBox<ICat>> boxFactory)
        {
            _boxFactory = boxFactory;
        }

        // GET api/boxes
        [HttpGet]
        public IEnumerable<string> Get()
        {
            yield return _boxFactory().ToString();
            yield return _boxFactory().ToString();
        }

        // GET api/boxes/5
        [HttpGet("{id}")]
        public string Get(int id) => _boxFactory().ToString();

        // POST api/boxes
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/boxes/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/boxes/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
