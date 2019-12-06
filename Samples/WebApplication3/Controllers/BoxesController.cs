namespace WebApplication3.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Mvc;
    using ShroedingersCat;

    [ApiController]
    [Route("api/[controller]")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public class BoxesController : ControllerBase
    {
        private readonly Func<IBox<ICat>> _boxFactory;

        public BoxesController(Func<IBox<ICat>> boxFactory) => 
            _boxFactory = boxFactory;

        [HttpGet]
        public IEnumerable<string> Get()
        {
            yield return _boxFactory().Content.ToString();
            yield return _boxFactory().Content.ToString();
        }
    }
}
