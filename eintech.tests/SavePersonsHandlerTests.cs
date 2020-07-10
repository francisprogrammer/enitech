using System;
using System.Threading.Tasks;
using eintech.persistence.SavePersons;
using EinTechApplication.Features.SavePersons;
using EinTechDomain;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace EintechTests
{
    public class SavePersonsHandlerTests
    {
        private ISavePerson _mockSavePerson;
        private IGuidService _stubGuidService;
        private SavePersonsHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockSavePerson = Substitute.For<ISavePerson>();
            _stubGuidService = Substitute.For<IGuidService>();
            
            _handler = new SavePersonsHandler(_mockSavePerson, _stubGuidService);
        }
        
        [Test]
        public async Task Returns_response_type_created_when_creating_person_successfully()
        {
            var response = await _handler.Save(new SavePersonRequest("anyName", DateTime.UtcNow));
            response.ResponseType.ShouldBe(ResponseType.Created);
        }
        
        [Test]
        public async Task Returns_person_id_when_creating_person_successfully()
        {
            var personId = Guid.NewGuid();
            
            _stubGuidService.Get().Returns(personId);
            
            var response = await _handler.Save(new SavePersonRequest("anyName", DateTime.UtcNow));
            
            response.PersonId.ShouldBe(personId);
        }

        [Test]
        public async Task Returns_empty_message_when_creating_person_successfully()
        {
            var response = await _handler.Save(new SavePersonRequest("anyName", DateTime.UtcNow));
            response.Message.ShouldBe(string.Empty);
        }

        [Test]
        public async Task Saves_person_when_validation_successful()
        {
            var personId = Guid.NewGuid();
            var dateCreated = DateTime.UtcNow;
            var anyName = "anyName";

            _stubGuidService.Get().Returns(personId);

            await _handler.Save(new SavePersonRequest(anyName, dateCreated));
            
            _mockSavePerson.Received().Save(Arg.Is<Person>(person => person.Id.Equals(personId)));
            _mockSavePerson.Received().Save(Arg.Is<Person>(person => person.Name.Equals(anyName)));
            _mockSavePerson.Received().Save(Arg.Is<Person>(person => person.DateCreated.Equals(dateCreated)));
        }

        [Test]
        public async Task Returns_response_type_validation_error_when_validation_fails()
        {
            var response = await _handler.Save(new SavePersonRequest(string.Empty, DateTime.UtcNow));
            response.ResponseType.ShouldBe(ResponseType.ValidationError);
        }
        
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public async Task Returns_message_when_person_name_is_empty(string name)
        {
            var response = await _handler.Save(new SavePersonRequest(string.Empty, DateTime.UtcNow));
            response.Message.ShouldBe("Name is required");
        }
    }
}