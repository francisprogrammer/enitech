using System;

namespace EinTechWebApi.Features.GetPersons
{
    public class GetPersonsViewModel
    {
        public Guid Id { get; }
        public string Name { get; }
        public DateTime DateCreated { get; }
        

        public GetPersonsViewModel(Guid id, string name, DateTime dateCreated)
        {
            Id = id;
            Name = name;
            DateCreated = dateCreated;
        }
    }
}