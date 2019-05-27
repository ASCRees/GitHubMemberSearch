using AutoMapper;
using GitHubMemberSearch.Models;
using GitHubMemberSearch.Services.Models;

namespace GitHubMemberSearch.App_Start
{
    public class AutoMapperConfig : AutoMapper.Profile
    {
        /// <summary>
        /// Creates the mappings.
        /// </summary>
        public static void CreateMappings()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<GitHubUserServiceModel, GitHubUserViewModel>();
            });
        }
    }
}