using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace GitHubMemberSearch.UnitTests
{ 
    [ExcludeFromCodeCoverage]

    public class BaseServiceUnitTest
    {
        private static string _baseURL;
        private static string _usersURL;
        private static string _starredURL;

        public static string BaseUrl
        {
            get
            {
                if (_baseURL == null)
                    _baseURL = ConfigurationManager.AppSettings["RootUrl"] + ConfigurationManager.AppSettings["BaseUrl"];

                return _baseURL;
            }
        }

        public static string UsersUrl
        {
            get
            {
                if (_usersURL == null)
                    _usersURL = BaseUrl + ConfigurationManager.AppSettings["UsersUrl"];

                return _usersURL;
            }
        }
    }
}