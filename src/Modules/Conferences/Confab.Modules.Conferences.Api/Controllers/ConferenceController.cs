using Confab.Modules.Conferences.Core.DTO;
using Confab.Modules.Conferences.Core.Repositories;
using Confab.Modules.Conferences.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Conferences.Api.Controllers
{
    [Route($"{BasePath}/[controller]")]
    internal class ConferenceController:ApiBaseController   
    {
        private readonly IConferenceService _conferenceService;

        public ConferenceController(IConferenceService conferenceService)
        {
            this._conferenceService = conferenceService;
        }
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ConferenceDetailsDto>> Get(Guid id)
        {
            return OkOrNotFound(await _conferenceService.GetAsync(id));
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<HostDto>>> GetAll(Guid id)
        {
            return Ok(await _conferenceService.BrowseAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Post(ConferenceDetailsDto dto)
        {
            await _conferenceService.AddAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = dto.Id }, null);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(Guid id, ConferenceDetailsDto dto)
        {
            dto.Id = id;
            await _conferenceService.UpdateAsync(dto);
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _conferenceService.RemoveAsync(id);
            return Ok();
        }
    }
}
