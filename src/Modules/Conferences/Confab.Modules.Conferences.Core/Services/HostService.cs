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
    internal class HostService : IHostService
    {
        private readonly IHostRepository _hostRepository;
        private readonly IHostDeletionPolicy _hostDeletionPolicy;

        public HostService(IHostRepository hostRepository, IHostDeletionPolicy hostDeletionPolicy)
        {
            _hostRepository = hostRepository;
            _hostDeletionPolicy = hostDeletionPolicy;
        }
        public async Task AddAsync(HostDto dto)
        {
            dto.Id = Guid.NewGuid();

            await _hostRepository.AddAsync(new Entities.Host
            {
                Name = dto.Name,
                Id = dto.Id,
                Description = dto.Description,
            });
        }

        public async Task<IReadOnlyList<HostDto>> BrowseAsync()
        {
            var hosts = await _hostRepository.GetAll();
            return hosts.Select(Map<HostDto>).ToList();
        }

        public async Task<HostDetailsDto> GetAsync(Guid id)
        {
            var host  = await _hostRepository.GetAsync(id);
            if(host is null)
            {
                return null;
            }

            var dto = Map<HostDetailsDto>(host);
            dto.ConferencesDtos = host.Conferences?.Select(x => new ConferencesDto
            {
                Id = x.Id,
                HostId = x.HostId,
                HostName = x.Host.Name,
                From = x.From,
                To = x.To,
                Name = x.Name,
                Location = x.Location,
                LogoUrl = x.LogoUrl,
                ParticipantLimit = x.ParticipantLimit,
            }).ToList() ?? new List<ConferencesDto>();
            //dto.ConferencesDtos = new List<ConferencesDto>();
            Console.WriteLine(dto);
            return dto;
        }

        public async Task RemoveAsync(Guid id)
        {
            var host = await _hostRepository.GetAsync(id);
            if (host is null)
            {
                throw new HostNotFoundException(id);
            }
            if (await _hostDeletionPolicy.CanDeleteAsync(host) is false)
            {
                throw new HostDeletionException(host);
            }
            await _hostRepository.RemoveAsync(host);
        }

        public async Task UpdateAsync(HostDetailsDto dto)
        {
            var host  = await _hostRepository.GetAsync(dto.Id);
            if(host is null)
            {
                throw new HostNotFoundException(dto.Id);
            }
            host.Name= dto.Name;
            host.Description= dto.Description;

            await _hostRepository.UpdateAsync(host);
        }

        private static T Map<T>(Host host) where T : HostDto, new()
        {
            return new T()
            {
                Id = host.Id,
                Name = host.Name,
                Description = host.Description,
            };
        }
    }
}
