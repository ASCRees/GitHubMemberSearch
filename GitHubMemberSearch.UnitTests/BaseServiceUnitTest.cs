using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace GitHubMemberSearch.UnitTests
{ 
    [ExcludeFromCodeCoverage]

    public class BaseServiceUnitTest
    {
        private static string _baseUrl;
        private static string _usersUrl;
        private static string _starredUrl;

        public static string BaseUrl
        {
            get
            {
                if (_baseUrl == null)
                    _baseUrl = ConfigurationManager.AppSettings["RootUrl"] + ConfigurationManager.AppSettings["BaseUrl"];

                return _baseUrl;
            }
        }

        public static string UsersUrl
        {
            get
            {
                if (_usersUrl == null)
                    _usersUrl = BaseUrl + ConfigurationManager.AppSettings["UsersUrl"];

                return _usersUrl;
            }
        }
    }
}