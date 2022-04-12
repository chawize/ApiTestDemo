using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.IO;
using ApiDemo.Request;
using ApiDemo.Response;

namespace ApiDemo
{
    public class ApiHelper
    {
        private RestClient _client;
        private RestRequest _request;
        private const string BaseUrl = "https://jsonplaceholder.typicode.com/";

        public RestClient SetUrl(string baseUrl, string endpoint)
        {
            var url = Path.Combine(baseUrl, endpoint);
            _client = new RestClient(url);
            return _client;
        }

        public RestRequest CreateGetRequest()
        {
            _request = new RestRequest()
            {
                Method = Method.GET
            };
            _request.AddHeader("Accept", "application/json");
            _request.RequestFormat = DataFormat.Json;
            return _request;
        }

        public RestRequest CreatePostRequest<T>(T payload)
        {
            _request = new RestRequest()
            {
                Method = Method.POST
            };
            _request.AddHeader("Accept", "application/json");
            _request.AddBody(payload);
            _request.RequestFormat = DataFormat.Json;
            return _request;
        }

        public IRestResponse GetUsersList()
        {
            var restClient = SetUrl(BaseUrl, "users"); 
            var restRequest = CreateGetRequest();

            IRestResponse response = restClient.Execute(restRequest);
            return response;
        }

        public IRestResponse GetUserById(string id)
        {
            var restClient = SetUrl(BaseUrl, $"users/{id}");
            var restRequest = CreateGetRequest();

            IRestResponse response = restClient.Execute(restRequest);
            return response;
        }

        public static IEnumerable<UserDataRes> GetContentList(IRestResponse response)
        {
            var userList = JsonConvert.DeserializeObject<IEnumerable<UserDataRes>>(response.Content);
            return userList;
        }

        public static UserDataRes GetContent(IRestResponse response)
        {
            var userList = JsonConvert.DeserializeObject<UserDataRes>(response.Content);
            return userList;
        }

        public IRestResponse PostNewUser( dynamic payload)
        {
            var restClient = SetUrl(BaseUrl, $"users");
            var restRequest = CreatePostRequest<UserDataReq>(payload);

            IRestResponse response = restClient.Execute(restRequest);
            return response;
        }

        public static T ParseJson<T>(string file)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(file));
        }
    }
}
