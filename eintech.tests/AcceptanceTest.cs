using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EinTechWebApi;
using EinTechWebApi.Features.GetPersons;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using NUnit.Framework;
using Shouldly;

namespace EintechTests
{
    public class AcceptanceTest
    {
        [Test]
        public async Task Saves_person_when_no_validation_errors_occur()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            var client = server.CreateClient();

            var dateTime = DateTime.UtcNow;
            
            var postResponse = await 
                client.PostAsync("/persons", new StringContent(JsonConvert.SerializeObject(new {name = "any name", dateCreated = dateTime}), Encoding.Default, "application/json"));
            
            postResponse.EnsureSuccessStatusCode();

            var getResponse = await client.GetAsync(postResponse.Headers.Location);

            var model = JsonConvert.DeserializeObject<GetPersonsViewModel>(await getResponse.Content.ReadAsStringAsync());
            
            model.Id.ShouldNotBe(Guid.Empty);
            model.Name.ShouldBe("any name");
            model.DateCreated.ShouldBe(dateTime);
            
        }
    }
}