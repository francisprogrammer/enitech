using System;
using System.Threading.Tasks;
using EinTechApplication.Features.GetPersons;
using EinTechApplication.Features.SavePersons;
using Microsoft.AspNetCore.Mvc;

namespace EinTechWebApi.Features.GetPersons
{
    [ApiController]
    public class GetPersonsController : ControllerBase
    {
        private readonly IGetPersonHandler _handler;

        public GetPersonsController(IGetPersonHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        [Route("/persons/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _handler.Get(new GetPersonRequest(id));

            if (response.ResponseType == ResponseType.NotFound) return NotFound(response.Message);
            
            var viewModel = new GetPersonsViewModel(response.PersonModel.Id, response.PersonModel.Name, response.PersonModel.DateCreated);
            
            return Ok(viewModel);
        }
    }
}