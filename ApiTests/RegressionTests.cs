using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Net;
using ApiDemo;
using ApiDemo.Request;

namespace ApiTests
{
    [TestClass]
    public class RegressionTests
    {
        [TestMethod]
        [Description("Get user list and verify total count")]
        public void TestGetUsersCount()
        {
            var apiHelper = new ApiHelper();

            var response = apiHelper.GetUsersList();
            var userList = ApiHelper.GetContentList(response);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Unexpected status code");
            Assert.AreEqual(10, userList.Count(), "Unexpected user count");
        }

        [TestMethod]
        [Description("Get user name information by ID")]
        public void TestGetUserById()
        {
            var apiHelper2 = new ApiHelper();

            var response = apiHelper2.GetUserById("8");
            var userInfo = ApiHelper.GetContent(response);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Unexpected status code");
            Assert.AreEqual("Nicholas Runolfsdottir V", userInfo.Name, "Unexpected name value");
        }

        [TestMethod]
        [Description("Post new user and verify if successful")]
        public void TestPostUsers()
        {
            var payload = ApiHelper.ParseJson<UserDataReq>("TestData/CreateUser.json");
            var apiHelper2 = new ApiHelper();

            var response = apiHelper2.PostNewUser(payload);
            var userInfo = ApiHelper.GetContent(response);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode, "Unexpected status code");
            Assert.AreEqual(11, userInfo.Id, "Unexpected user id");
        }
    }
}
