using System.Threading.Tasks;
using EinTechDomain;

namespace eintech.persistence.SavePersons
{
    public interface ISavePerson
    {
        Task Save(Person person);
    }
}
