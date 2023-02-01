using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confab.Modules.Conferences.Core.DTO
{
    internal class HostDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength =3)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }
    }

    internal class HostDetailsDto:HostDto
    {
        public List<ConferencesDto> ConferencesDtos { get; set; }
    }
}
