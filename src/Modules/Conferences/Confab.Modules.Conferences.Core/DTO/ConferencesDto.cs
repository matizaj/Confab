using System.ComponentModel.DataAnnotations;

namespace Confab.Modules.Conferences.Core.DTO
{
    internal class ConferencesDto
    {
        public Guid Id { get; set; }
        public Guid HostId { get; set; }
        public string HostName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string LogoUrl { get; set; }
        public int? ParticipantLimit { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }

    internal class ConferenceDetailsDto: ConferencesDto
    {
        public string Description { get; set; }
    }
}