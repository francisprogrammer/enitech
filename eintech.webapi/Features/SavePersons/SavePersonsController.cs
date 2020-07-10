using System.Threading.Tasks;
using EinTechApplication.Features.SavePersons;
using Microsoft.AspNetCore.Mvc;

namespace EinTechWebApi.Features.SavePersons
{
    [ApiController]
    public class SavePersonsController : ControllerBase
    {
        private readonly ISavePersonHandler _savePersonHandler;

        public SavePersonsController(ISavePersonHandler savePersonHandler)
        {
            _savePersonHandler = savePersonHandler;
        }

        [HttpPost]
        [Route("/persons")]
        public async Task<IActionResult> Save(SavePersonModel model)
        {
            var response = await _savePersonHandler.Save(new SavePersonRequest(model.Name, model.DateCreated));
            
            return response.ResponseType == ResponseType.ValidationError
                ? BadRequest(response.Message)
                : (IActionResult)Created($"/persons/{response.PersonId}", null);
        }
    }
}