using helpmeinvest.Models;
using helpmeinvest.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace helpmeinvest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelIntegrationController : ControllerBase
    {
        private readonly ChannelIntegrationService ciService;
        public ChannelIntegrationController(ChannelIntegrationService service)
        {
            ciService = service;
        }

        [HttpGet]
        public IEnumerable<ChannelIntegration> GetChannelIntegrations()
        {
            return ciService.GetChannelIntegrations();
        }

        [HttpPost]
        public string CreateChannelIntegration(ChannelIntegration ci)
        {
            return ciService.CreateChannelIntegration(ci);
        }
    }
}
