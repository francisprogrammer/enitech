using System;
using System.Threading.Tasks;
using eintech.persistence.SavePersons;
using EinTechDomain;

namespace EinTechApplication.Features.SavePersons
{
    public class SavePersonsHandler : ISavePersonHandler
    {
        private readonly ISavePerson _savePerson;
        private readonly IGuidService _guidService;

        public SavePersonsHandler(ISavePerson savePerson, IGuidService guidService)
        {
            _savePerson = savePerson;
            _guidService = guidService;
        }
        
        public async Task<SavePersonResponse> Save(SavePersonRequest request)
        {
            var personId = _guidService.Get();
            
            var personResult = Person.Create(personId, request.Name, request.DateCreated);

            if (!personResult.IsSuccess) 
                return SavePersonResponse.Failed(personResult.ErrorMessage);

            await _savePerson.Save(personResult.Value);

            return SavePersonResponse.Success(personId);
        }
    }
}