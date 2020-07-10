using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using eintech.persistence.GetPersons;
using eintech.persistence.SavePersons;
using EinTechDomain;

namespace eintech.persistence
{
    public class PersonRepository : ISavePerson, IGetPersons
    {
        public static readonly ConcurrentBag<Person> _Persons = new ConcurrentBag<Person>();
        
        public async Task Save(Person person)
        {
            await Task.Run(() => _Persons.Add(person));
        }

        public async Task<Result<Person>> Get(Guid id)
        {
            return await Task.Run(() => _Persons.Any(x => x.Id.Equals(id)))
                ? Result<Person>.Success(_Persons.Single(x => x.Id.Equals(id)))
                : Result<Person>.Failed("No person found");
        }
    }
}