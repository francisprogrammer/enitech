using System.Threading.Tasks;

namespace EinTechApplication.Features.SavePersons
{
    public interface ISavePersonHandler
    {
        Task<SavePersonResponse> Save(SavePersonRequest request);
    }
}