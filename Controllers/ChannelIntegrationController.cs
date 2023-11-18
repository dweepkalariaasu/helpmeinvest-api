using helpmeinvest.Models;
using helpmeinvest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace helpmeinvest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelIntegrationController : ControllerBase
    {
        private readonly ChannelIntegrationService _ciService;
        public ChannelIntegrationController(ChannelIntegrationService service)
        {
            _ciService = service;
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<ChannelIntegration> GetChannelIntegrations()
        {
            return _ciService.GetChannelIntegrations();
        }

        [HttpPost]
        [Authorize]
        public string CreateChannelIntegration(ChannelIntegration ci)
        {
            return _ciService.CreateChannelIntegration(ci);
        }
    }
}
