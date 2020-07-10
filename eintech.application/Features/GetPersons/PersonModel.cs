using System;

namespace EinTechApplication.Features.GetPersons
{
    public class PersonModel
    {
        public Guid Id { get; }
        public string Name { get; }
        public DateTime DateCreated { get; }
        

        public PersonModel(Guid id, string name, DateTime dateCreated)
        {
            Id = id;
            Name = name;
            DateCreated = dateCreated;
        }
    }
}