namespace WebApplication2.Controllers
{
    using System.Collections.Generic;
    using Clock.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class ClockController : Controller
    {
        private readonly IClockViewModel _viewModel;

        public ClockController(IClockViewModel viewModel) => _viewModel = viewModel;

        // GET api/boxes
        [HttpGet]
        public IEnumerable<string> Get()
        {
            yield return _viewModel.Date;
            yield return _viewModel.Time;
        }
    }
}
