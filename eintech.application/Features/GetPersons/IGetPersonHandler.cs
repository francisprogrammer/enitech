using System.Threading.Tasks;
using EinTechApplication.Features.SavePersons;

namespace EinTechApplication.Features.GetPersons
{
    public interface IGetPersonHandler
    {
        Task<GetPersonResponse> Get(GetPersonRequest request);
    }
}