using System;
using System.Collections.Generic;
using System.Text;
using Demo;
using Newtonsoft.Json;
using RestSharp;

namespace Demo
{
    public class Demo
    {
        public ListOfUsersDTO GetUsersList()
        {
            var restClient = new RestClient("https://jsonplaceholder.typicode.com");
            var restRequest = new RestRequest("/users", Method.Get);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;

            RestResponse response = restClient.Execute(restRequest);
            var content = response.Content;

            var users = JsonConvert.DeserializeObject<ListOfUsersDTO>(content);
            return users;
        }
    }
}
