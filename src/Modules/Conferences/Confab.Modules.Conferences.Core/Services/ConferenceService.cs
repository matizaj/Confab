using Confab.Modules.Conferences.Core.DTO;
using Confab.Modules.Conferences.Core.Entities;
using Confab.Modules.Conferences.Core.Exceptions;
using Confab.Modules.Conferences.Core.Policies;
using Confab.Modules.Conferences.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Conferences.Core.Services
{
    internal class ConferenceService : IConferenceService
    {
        private readonly IConferenceRepository _conferenceRepository;
        private readonly IHostRepository _hostRepository;
        private readonly IConferencesDeletionPolicy _conferencesDeletionPolicy;

        public ConferenceService(IConferenceRepository conferenceRepository, IHostRepository hostRepository, IConferencesDeletionPolicy conferencesDeletionPolicy)
        {
            _conferenceRepository = conferenceRepository;
            _hostRepository = hostRepository;
            _conferencesDeletionPolicy = conferencesDeletionPolicy;
        }
        public async Task AddAsync(ConferenceDetailsDto dto)
        {
            if(await _hostRepository.GetAsync(dto.HostId) is null) 
            {
                throw new HostNotFoundException(dto.HostId);
            }

            dto.Id=Guid.NewGuid();
            var conf = new Conference()
            {
                HostId = dto.HostId,
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                From = dto.From,
                To = dto.To,
                Location = dto.Location,
                LogoUrl = dto.LogoUrl,
                ParticipantLimit = dto.ParticipantLimit,
            };
            await _conferenceRepository.AddAsync(conf);
        }

        public async Task<IReadOnlyList<ConferencesDto>> BrowseAsync()
        {
            var conferences = await _conferenceRepository.GetAll();
           
            return conferences.Select(Map<ConferencesDto>).ToList();
        }

        public async Task<ConferenceDetailsDto> GetAsync(Guid id)
        {
            var conference = await _conferenceRepository.GetAsync(id);
            if (conference is null)
            {
                throw new ConferenceNotFoundException(id);
            }

            var dto = Map<ConferenceDetailsDto>(conference);
            dto.Description= conference.Description;

            return dto;

        }

        public async Task RemoveAsync(Guid id)
        {
            var conference = await _conferenceRepository.GetAsync(id);
            if (conference is null) 
            {
                throw new ConferenceNotFoundException(id);
            }

            if(await _conferencesDeletionPolicy.CanDeleteAsync(conference) is false)
            {
                throw new ConnotDeleteConferenceException(id);
            }

            await _conferenceRepository.RemoveAsync(conference);
        }

        public async Task UpdateAsync(ConferenceDetailsDto dto)
        {
            var conference = await _conferenceRepository.GetAsync(dto.Id);
            if (conference is null)
            {
                throw new ConferenceNotFoundException(dto.Id);
            }

            conference.Description = dto.Description;
            conference.Location= dto.Location;
            conference.Name = dto.Name;
            conference.ParticipantLimit = dto.ParticipantLimit;
            conference.From = dto.From;
            conference.To = dto.To;
            conference.LogoUrl = dto.LogoUrl;

            await _conferenceRepository.UpdateAsync(conference);
            
        }

        private static T Map<T>(Conference conference) where T : ConferencesDto, new()
        {
            return new T()
            {
                Id = conference.Id,
                Name = conference.Name,
                Location = conference.Location,
                HostId= conference.HostId,
                From= conference.From,  
                To= conference.To,  
                ParticipantLimit=conference.ParticipantLimit,
                LogoUrl=conference.LogoUrl,
                HostName = conference.Host?.Name
            };
        }
    }
}
