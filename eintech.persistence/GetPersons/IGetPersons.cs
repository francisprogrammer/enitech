using System;
using System.Threading.Tasks;
using EinTechDomain;

namespace eintech.persistence.GetPersons
{
    public interface IGetPersons
    {
        Task<Result<Person>> Get(Guid id);
    }
}