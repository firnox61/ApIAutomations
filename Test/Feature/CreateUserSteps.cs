using ApiAutomation;
using ApiAutomation.Models.Request;
using ApiAutomation.Models.Response;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;
using ApiAutomation.Utility;
//feature de bir özellik tablosu oluştururuz açıklaması özellikleri test senaryolarımızı tanımlayabiliriz ve tüm bu adımları işler.
//ve adımları burda oluşturu
namespace APITests.Features
{
    [Binding]
    public class CreateUserSteps
    {
        private CreateUserReq createUserReq;//kullanıcı isteği oluşturmak için
        private RestResponse response;//restşarpdan cevap almak için
        private ScenarioContext scenarioContext;//senaryo bağıntısı specflowla adımlar hakkında bilgiye sahiptir
        private HttpStatusCode statusCode;//durum kodu yaptıl
        private APIClient api;//api istemcisinin bişr nesnesine erşimek için 
       
        public CreateUserSteps(CreateUserReq createUserReq, ScenarioContext scenarioContext)//kullanıcı isteği ve senaryo eklendi
        {
            this.createUserReq = createUserReq;//kullanıcı isteği oluştururr
            this.scenarioContext= scenarioContext;
            api= new APIClient();
        }

        [Given(@"User payload ""(.*)""")]
        public void GivenUserPayload(string filename)
        {
            string file = HandleContent.GetFilePath(filename);
            var payload = HandleContent.ParseJson<CreateUserReq>(file);//?
            payload.name = "";//payloadda bir güncelleme yapmak istiyorsak kullanırız
            scenarioContext.Add("createUser_payload", payload);//basit bir yük lakin tanımlayabilmemiz için farklı bir ad tutuyoruz
        }


        
        [When(@"Send request to create user")]
        public async System.Threading.Tasks.Task WhenSendRequestToCreateUser()
        {
         createUserReq = scenarioContext.Get<CreateUserReq>("createUser_payload");//özellik dosyaının adını jsona pass ediyorum sonra yükü ppaylaşarak senaryoyya alıyorum
           // var api = new APIClient();//api istemicisi oluşturmam gerekir
            response = await api.CreateUser<CreateUserReq>(createUserReq);
        }
        
        [Then(@"Validate user is created")]
        public void ThenValidateUserIsCreated()
        {//yanıttan durum kodunu alabiliriz
             
            statusCode = response.StatusCode;
            var code = (int)statusCode;
            Assert.That(code, Is.EqualTo(201));
            //özellik dosyasını oluşturabiliriz
            //kuulanıcı yanıt oluştururr ve yanıtı buradan ilet nesne olarak döndür

            var content = HandleContent.GetContent<CreateUserRes>(response);
            //özellik dosyasının beklenenen değerşeri sunucuya gönderdiğimizi şeyi alıcaz

            Assert.AreEqual(createUserReq.name, content.name);
            Assert.AreEqual(createUserReq.job, content.job);

        } 
    }
}
