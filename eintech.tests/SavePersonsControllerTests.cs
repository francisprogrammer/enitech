using System;
using System.Threading.Tasks;
using EinTechApplication.Features.SavePersons;
using EinTechWebApi.Features.SavePersons;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace EintechTests
{
    public class SavePersonsControllerTests
    {
        private ISavePersonHandler _savePersonHandler;
        private SavePersonsController _controller;

        [SetUp]
        public void SetUp()
        {
            _savePersonHandler = Substitute.For<ISavePersonHandler>();
            _controller = new SavePersonsController(_savePersonHandler);
        }
        
        [Test]
        public async Task Returns_created_result_when_no_validation_errors()
        {
            var personId = Guid.NewGuid();
            var anyName = "any name";

            _savePersonHandler.Save(Arg.Is<SavePersonRequest>(request => request.Name.Equals(anyName))).Returns(SavePersonResponse.Success(personId));

            var result = await _controller.Save(new SavePersonModel { Name = anyName, DateCreated = DateTime.UtcNow});

            (result is CreatedResult).ShouldBeTrue();
        }
        
        [Test]
        public async Task Returns_url_for_newly_created_resource_when_no_validation_errors()
        {
            var personId = Guid.NewGuid();

            var anyName = "any name";

            _savePersonHandler.Save(Arg.Is<SavePersonRequest>(request => request.Name.Equals(anyName))).Returns(SavePersonResponse.Success(personId));

            var result = await _controller.Save(new SavePersonModel { Name = anyName, DateCreated = DateTime.UtcNow});

            var response = result as CreatedResult;
            
            response.Location.ShouldBe($"/persons/{personId}");
        }
        
        [Test]
        public async Task Returns_bad_request_result_when_validation_errors()
        {
            _savePersonHandler.Save(Arg.Is<SavePersonRequest>(request => request.Name == "any name")).Returns(SavePersonResponse.Failed(string.Empty));

            var result = await _controller.Save(new SavePersonModel { Name = "any name", DateCreated = DateTime.UtcNow});

            (result is BadRequestObjectResult).ShouldBeTrue();
            
        }
        
        [Test]
        public async Task Returns_error_message_when_validation_errors()
        {
            var anyErrorMessage = "any error message";
            
            _savePersonHandler.Save(Arg.Is<SavePersonRequest>(request => request.Name.Equals("any name"))).Returns(SavePersonResponse.Failed(anyErrorMessage));

            var result = await _controller.Save(new SavePersonModel { Name = "any name", DateCreated = DateTime.UtcNow});

            var response = result as BadRequestObjectResult;
            
            response.Value.ShouldBe(anyErrorMessage);
            
        }
    }
}