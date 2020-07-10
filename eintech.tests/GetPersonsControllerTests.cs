using System;
using System.Threading.Tasks;
using EinTechApplication.Features.GetPersons;
using EinTechApplication.Features.SavePersons;
using EinTechWebApi.Features.GetPersons;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace EintechTests
{
    public class GetPersonsControllerTests
    {
        private IGetPersonHandler _handler;
        private GetPersonsController _controller;

        [SetUp]
        public void Setup()
        {
            _handler = Substitute.For<IGetPersonHandler>();
            _controller = new GetPersonsController(_handler);
        }
        
        [Test]
        public async Task Returns_ok_when_person_exist()
        {
            var personId = Guid.NewGuid();
            
            _handler
                .Get(Arg.Is<GetPersonRequest>(request => request.PersonId.Equals(personId)))
                .Returns(GetPersonResponse.Success(new PersonModel(personId, "any name", DateTime.UtcNow)));

            var result = await _controller.Get(personId);

            (result is OkObjectResult).ShouldBeTrue();
        }
        
        [Test]
        public async Task Returns_person_when_person_exist()
        {
            var personId = Guid.NewGuid();
            var dateCreated = DateTime.UtcNow;
            
            _handler
                .Get(Arg.Is<GetPersonRequest>(request => request.PersonId.Equals(personId)))
                .Returns(GetPersonResponse.Success(new PersonModel(personId, "any name", dateCreated)));

            var result = await _controller.Get(personId);

            var response = result as OkObjectResult;

            var model = (GetPersonsViewModel) response.Value;
            
            model.Id.ShouldBe(personId);
            model.Name.ShouldBe("any name");
            model.DateCreated.ShouldBe(dateCreated);
        }

        [Test]
        public async Task Returns_not_found_when_person_does_not_exist()
        {
            _handler
                .Get(Arg.Any<GetPersonRequest>())
                .Returns(GetPersonResponse.Failed(string.Empty));

            var result = await _controller.Get(Guid.NewGuid());
            
            (result is NotFoundObjectResult).ShouldBeTrue();
        }
        
        [Test]
        public async Task Returns_not_found_error_message_when_person_does_not_exist()
        {
            var message = string.Empty;
            
            _handler
                .Get(Arg.Any<GetPersonRequest>())
                .Returns(GetPersonResponse.Failed(message));

            var result = await _controller.Get(Guid.NewGuid());
            
            var response = result as NotFoundObjectResult;
            
            response.Value.ShouldBe(message);
        }
    }
}