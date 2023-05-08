using ApiAutomation.Models.Request;
using ApiAutomation;
using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;
using ApiAutomation.Utility;
using ApiAutomation.Models.Response;
using ApiAutomation.Auth;

namespace Test.Feature
{
    [Binding]
    public class UpdateUserSteps
    {
    private UpdateUserReq updateUserReq;//kullanıcı isteği oluşturmak için
    private CreateUserRes createUserRes;
    private RestResponse response;//restşarpdan cevap almak için
    private ScenarioContext scenarioContext;//senaryo bağıntısı specflowla adımlar hakkında bilgiye sahiptir
    private HttpStatusCode statusCode;//durum kodu yaptıl
    private APIClient api;//api istemcisinin bişr nesnesine erşimek için 
        private APIAuthenticator authenticator;
       // private readonly string id;

        public UpdateUserSteps(UpdateUserReq updateUserReq, ScenarioContext scenarioContext, CreateUserRes createUserRes)//kullanıcı isteği ve senaryo eklendi
        {
            this.updateUserReq = updateUserReq;//kullanıcı isteği oluştururr
            this.scenarioContext = scenarioContext;
            this.createUserRes = createUserRes;
            
            api = new APIClient();

        }
        [Given(@"\[User payload ""([^""]*)""]")]
        public void GivenUserPayload(string filename)
        {
            string file = HandleContent.GetFilePath(filename);
            var payload = HandleContent.ParseJson<UpdateUserReq>(file);//?
            payload.name = "";//payloadda bir güncelleme yapmak istiyorsak kullanırız
           // string id = HandleContent.GetContent<CreateUserRes>();
          
            scenarioContext.Add("updateUser_payload", payload);//basit bir yük lakin tanımlayabilmemiz için farklı bir ad tutuyoruz
                                                               //  String id = createUserRes.id;
                                                               //   scenarioContext.Add("User_ID", id);
            var content = HandleContent.GetContent<CreateUserRes>(response);
            scenarioContext.Add("updateUser_content", content.id);
        }

        [When(@"\[Send request to Update user]")]
        public async System.Threading.Tasks.Task  WhenSendRequestToUpdateUser()
        {
            
            
            updateUserReq = scenarioContext.Get<UpdateUserReq>("updateUser_payload");//özellik dosyaının adını jsona pass ediyorum sonra yükü ppaylaşarak senaryoyya alıyorum
                                                                                     // String id= scenarioContext.Get<CreateUserRes>("User_ID");
                                                                                     // createUserRes= scenarioContext.Get<CreateUserRes>("User_ID");                                                                        // var api = new APIClient();//api istemicisi oluşturmam gerekir
            createUserRes = scenarioContext.Get<CreateUserRes>("updateUser_content");
            response = await api.UpdateUser<UpdateUserReq>(updateUserReq,createUserRes.id);

        }

        [Then(@"\[Validate user is update]")]
        public void ThenValidateUserİsUpdate()
        {
            statusCode = response.StatusCode;
            var code = (int)statusCode;
            Assert.That(code, Is.EqualTo(201));
            //özellik dosyasını oluşturabiliriz
            //kuulanıcı yanıt oluştururr ve yanıtı buradan ilet nesne olarak döndür

            var content = HandleContent.GetContent<CreateUserRes>(response);
            //özellik dosyasının beklenenen değerşeri sunucuya gönderdiğimizi şeyi alıcaz

            Assert.AreEqual(updateUserReq.name, content.name);
            Assert.AreEqual(updateUserReq.job, content.job);
            Assert.AreEqual(createUserRes.id, content.id);
        }
    }
}
  