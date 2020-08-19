namespace WebApplication2.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Clock.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public class ClockController : Controller
    {
        private readonly IClockViewModel _viewModel;

        public ClockController(IClockViewModel viewModel) => _viewModel = viewModel;


        // GET api/boxes
        [HttpGet]
        public IEnumerable<string> Get()
        {
            yield return $"{_viewModel.Date} {_viewModel.Time}";
        }
    }
}
