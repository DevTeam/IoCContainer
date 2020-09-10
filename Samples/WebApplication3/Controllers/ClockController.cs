namespace WebApplication3.Controllers
{
    using System.Collections.Generic;
    using Clock.ViewModels;
    using Microsoft.AspNetCore.DataProtection.KeyManagement;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    [ApiController]
    [Route("api/[controller]")]
    public class ClockController : ControllerBase
    {
        private readonly IClockViewModel _viewModel;

        public ClockController(IClockViewModel viewModel, IConfigureOptions<KeyManagementOptions> options)
        {
            _viewModel = viewModel;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            yield return _viewModel.Date;
            yield return _viewModel.Time;
        }
    }
}
