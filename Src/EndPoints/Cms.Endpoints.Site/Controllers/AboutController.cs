﻿using Cmd.Application.Models.News.Queries.GetAboutMuseum;
using Cmd.Application.Models.News.Queries.GetAboutWithFilter;
using Cmd.Application.Models.News.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cms.Endpoints.Site.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecific")]
    public class AboutController : ControllerBase
    {

        private readonly ISender _sender;

        public AboutController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAboutMuseum(GetAboutWithFilterQuery query)
        {
            var result =await _sender.Send(query);

            return Ok(result);
        }
    }
}
