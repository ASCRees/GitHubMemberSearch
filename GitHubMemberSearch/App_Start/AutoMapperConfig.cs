using AutoMapper;
using GitHubMemberSearch.Models;
using GitHubMemberSearch.Services.Models;
using System.Diagnostics.CodeAnalysis;

namespace GitHubMemberSearch.App_Start
{
    [ExcludeFromCodeCoverage]

    public class AutoMapperConfig : Profile
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