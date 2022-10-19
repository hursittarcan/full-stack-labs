using AutoMapper;
using DevOps.Domain;

namespace DevOps.Api.Models
{
    public class DeveloperDetailModel
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public double Rating { get; set; }

        private class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Developer, DeveloperDetailModel>();
            }
        }
    }
}