namespace WebApplication3.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Clock.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public class ClockController : ControllerBase
    {
        private readonly IClockViewModel _viewModel;

        public ClockController(IClockViewModel viewModel) => _viewModel = viewModel;

        [HttpGet]
        public IEnumerable<string> Get()
        {
            yield return $"{_viewModel.Date} {_viewModel.Time}";
        }
    }
}
