using System.Diagnostics.CodeAnalysis;

namespace GitHubMemberSearch.DependencyResolution
{
    using App_Start;
    using StructureMap.Web.Pipeline;
    using System.Web;

    [ExcludeFromCodeCoverage]
    public class StructureMapScopeModule : IHttpModule
    {
        #region Public Methods and Operators

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += (sender, e) => StructuremapMvc.StructureMapDependencyScope.CreateNestedContainer();
            context.EndRequest += (sender, e) =>
            {
                HttpContextLifecycle.DisposeAndClearAll();
                StructuremapMvc.StructureMapDependencyScope.DisposeNestedContainer();
            };
        }

        #endregion Public Methods and Operators
    }
}