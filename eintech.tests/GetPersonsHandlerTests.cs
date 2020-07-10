using System;
using System.Threading.Tasks;
using eintech.persistence.GetPersons;
using EinTechApplication.Features.GetPersons;
using EinTechApplication.Features.SavePersons;
using EinTechDomain;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace EintechTests
{
    public class GetPersonsHandlerTests
    {
        private IGetPersons _stubGetPerson;
        private GetPersonHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _stubGetPerson = Substitute.For<IGetPersons>();
            _handler = new GetPersonHandler(_stubGetPerson);
        }
        
        [Test]
        public async Task Returns_person_when_found()
        {
            var personId = Guid.NewGuid();
            var dateTime = DateTime.UtcNow;
            
            var result = Person.Create(personId, "any name", dateTime);

            _stubGetPerson.Get(personId).Returns(Result<Person>.Success(result.Value));

            var response = await _handler.Get(new GetPersonRequest(personId));

            response.PersonModel.Id.Equals(personId);
            response.PersonModel.Name.Equals("any name");
            response.PersonModel.DateCreated.Equals(dateTime);
            response.ResponseType.ShouldBe(ResponseType.Found);
        }
        
        [Test]
        public async Task Returns_response_not_found_person_when_does_not_exist()
        {
            _stubGetPerson.Get(Arg.Any<Guid>()).Returns(Result<Person>.Failed("any message"));
            
            var response = await _handler.Get(new GetPersonRequest(Guid.NewGuid()));

            response.PersonModel.ShouldBeNull();
            response.ResponseType.ShouldBe(ResponseType.NotFound);
            response.Message.ShouldBe("any message");
        }
    }
}