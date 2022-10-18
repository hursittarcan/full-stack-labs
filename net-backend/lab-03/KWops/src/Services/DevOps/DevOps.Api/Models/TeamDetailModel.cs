using AutoMapper;
using DevOps.Domain;

namespace DevOps.Api.Models
{
    public class TeamDetailModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IList<DeveloperDetailModel> Developers { get; set; } = new List<DeveloperDetailModel>();

        private class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Team, TeamDetailModel>();
            }
        }
    }
}