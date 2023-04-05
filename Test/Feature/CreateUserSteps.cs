using APIAutomation;
using APIAutomation.Models.Request;
using APIAutomation.Models.Response.Utility;
using APIAutomation.Models.Response;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

using APIAutomation;
using APIAutomation.Models;
using APIAutomation.Models.Request;
using System;
using System.Diagnostics;
using System.Net;
using TechTalk.SpecFlow;

namespace APITests.Features
{
    [Binding]
    public class CreateUserSteps
    {
        private CreateUserReq createUserReq;
        private RestResponse response;
        private ScenarioContext scenarioContext;
        private HttpStatusCode statusCode;
        /* private const string BASE_URL = "https://reqres.in/";
         private readonly CreateUserReq createUserReq;
         private RestResponse response;*/
      //  private readonly CreateUserReq createUserReq;
        public CreateUserSteps(CreateUserReq createUserReq, ScenarioContext scenarioContext)
        {
            this.createUserReq = createUserReq;//kullanıcı isteği oluştururr
            this.scenarioContext= scenarioContext;
        }

        [Given(@"User with name ""(.*)""")]
        public void GivenUserWithName(string name)
        {
            createUserReq.name = name;
        }



        
        [Given(@"user with job ""(.*)""")]
        public void GivenUserWithJob(string job)
        {
            createUserReq.job = job;
        }


        
        [When(@"Send request to create user")]
        public async System.Threading.Tasks.Task WhenSendRequestToCreateUser()
        {
            var api = new APIClient();
            response = await api.CreateUser<CreateUserReq>(createUserReq);
        }
        
        [Then(@"validate user is created")]
        public void ThenValidateUserIsCreated()
        {
             
            statusCode = response.StatusCode;
            var code = (int)statusCode;
            Assert.AreEqual(201, code);
            //özellik dosyasını oluşturabiliriz
            var content = HandleContent.GetContent<CreateUserRes>(response);
            Assert.AreEqual(createUserReq.name, content.name);
            Assert.AreEqual(createUserReq.job, content.job);

        }
    }
}
