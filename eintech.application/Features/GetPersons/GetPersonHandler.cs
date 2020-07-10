using System.Threading.Tasks;
using eintech.persistence.GetPersons;

namespace EinTechApplication.Features.GetPersons
{
    public class GetPersonHandler : IGetPersonHandler
    {
        private readonly IGetPersons _getPersons;

        public GetPersonHandler(IGetPersons getPersons)
        {
            _getPersons = getPersons;
        }
        
        public async Task<GetPersonResponse> Get(GetPersonRequest request)
        {
            var personResult = await _getPersons.Get(request.PersonId);
            
            return !personResult.IsSuccess 
                ? GetPersonResponse.Failed(personResult.ErrorMessage) 
                : GetPersonResponse.Success(new PersonModel(personResult.Value.Id, personResult.Value.Name, personResult.Value.DateCreated));
        }
    }
}