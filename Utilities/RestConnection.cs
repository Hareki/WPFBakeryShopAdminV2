using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace WPFBakeryShopAdminV2.Utilities
{
    public class RestConnection
    {
        public static readonly string ADMIN_BASE_CONNECTION_STRING = "http://localhost:8080/api/admin/";
        public static readonly string ACCOUNT_BASE_CONNECTION_STRING = "http://localhost:8080/api/account/";
        public static readonly string AUTHENTICATE_BASE_CONNECTION_STRING = "http://localhost:8080/api/";
        public static readonly string PUBLIC_BASE_CONNECTION_STRING = "http://localhost:8080/api/public";
        //public static readonly string AUTHENTICATE_BASE_CONNECTION_STRING = "https://bakeryshop-web-service.herokuapp.com/api/";

        private static RestClient _managementRestClient;
        private static RestClient _accountRestClient;
        private static RestClient _authRestClient;
        private static RestClient _publicRestClient;

        private static int _delayTime = 0;


        #region Base
        public static void EstablishConnection(string token)
        {
            BearerToken = token;

            ManagementRestClient = new RestClient(ADMIN_BASE_CONNECTION_STRING);
            AccountRestClient = new RestClient(ACCOUNT_BASE_CONNECTION_STRING);
            AuthRestClient = new RestClient(AUTHENTICATE_BASE_CONNECTION_STRING);
            PublicRestClient = new RestClient(PUBLIC_BASE_CONNECTION_STRING);
        }
        public static async Task<RestResponse> ExecuteRequestAsync(RestClient restClient, Method method, string requestURl, string requestBody = null, string contentType = null)
        {
            var request = new RestRequest(requestURl, method);
            if (!string.IsNullOrEmpty(requestBody))
                request.AddBody(requestBody, contentType);
            await Task.Delay(_delayTime);
            return await restClient.ExecuteAsync(request);
        }
        public static async Task<RestResponse> ExecuteParameterRequestAsync(RestClient restClient, Method method, string requestURl, List<KeyValuePair<string, string>> parameters)
        {
            var request = new RestRequest(requestURl, method);
            if (parameters != null && parameters.Count > 0)
            {
                foreach (KeyValuePair<string, string> element in parameters)
                {
                    request.AddParameter(element.Key, element.Value);
                }
            }
            await Task.Delay(_delayTime);
            return await restClient.ExecuteAsync(request);
        }
        public static async Task<RestResponse> ExecuteFileRequestAsync(RestClient restClient, Method method, string requestUrl, List<KeyValuePair<string, string>> images)
        {
            var request = new RestRequest(requestUrl, method);
            foreach (KeyValuePair<string, string> image in images)
            {
                request.AddFile(image.Key, image.Value, "multipart/form-data");
            }
            await Task.Delay(_delayTime);
            return await restClient.ExecuteAsync(request);
        }
        #endregion

        #region Properties
        public static RestClient AccountRestClient
        {
            get
            { return _managementRestClient; }
            set
            {
                _managementRestClient = value;
                _managementRestClient.Authenticator = new JwtAuthenticator(BearerToken);
            }
        }
        public static RestClient ManagementRestClient
        {
            get
            { return _accountRestClient; }
            set
            {
                _accountRestClient = value;
                _accountRestClient.Authenticator = new JwtAuthenticator(BearerToken);
            }
        }
        public static RestClient AuthRestClient
        {
            get
            { return _authRestClient; }
            set
            {
                _authRestClient = value;
                _authRestClient.Authenticator = new JwtAuthenticator(BearerToken);
            }
        }
        public static RestClient PublicRestClient
        {
            get
            { return _publicRestClient; }
            set
            {
                _publicRestClient = value;
                _publicRestClient.Authenticator = new JwtAuthenticator(BearerToken);
            }
        }
        private static string _bearerToken;
        public static bool RememberMe { get; set; }
        public static string BearerToken
        {
            get
            {
                return _bearerToken;
            }
            set
            {
                _bearerToken = value;
                if (RememberMe)
                    SavedBearerToken = value;
            }
        }

        public static string SavedBearerToken
        {
            get
            {
                return Properties.Settings.Default.token;
            }
            set
            {
                Properties.Settings.Default.token = value;
                Properties.Settings.Default.Save();
            }
        }
        public static void LogOut()
        {
            SavedBearerToken = string.Empty;
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
        #endregion
    }
}
