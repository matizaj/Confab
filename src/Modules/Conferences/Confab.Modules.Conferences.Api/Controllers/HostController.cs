using Confab.Modules.Conferences.Core.DTO;
using Confab.Modules.Conferences.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Conferences.Api.Controllers
{
    [Authorize(Policy =Policy)]
    [Route($"{ConferencesModule.BasePath}/[controller]")]
    internal class HostController:ApiBaseController
    {
        private readonly IHostService _hostService;
        private const string Policy = "hosts";

        public HostController(IHostService hostService)
        {
            _hostService = hostService;
        }
     
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<HostDetailsDto>> Get(Guid id)
        {
            return OkOrNotFound(await _hostService.GetAsync(id));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IReadOnlyList<HostDto>>> GetAll(Guid id)
        {
            return Ok(await _hostService.BrowseAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Post(HostDto dto)
        {
            await _hostService.AddAsync(dto);
            return CreatedAtAction(nameof(Get), new {id=dto.Id}, null);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(Guid id, HostDetailsDto dto)
        {
            dto.Id = id;
            await _hostService.UpdateAsync(dto);
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _hostService.RemoveAsync(id);
            return Ok();
        }

    }
}
