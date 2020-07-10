using System;

namespace EinTechDomain
{
    public class Person
    {
        public Guid Id { get; }
        public string Name { get; }
        public DateTime DateCreated { get; }

        private Person(Guid id, string name, DateTime dateCreated)
        {
            Name = name;
            DateCreated = dateCreated;
            Id = id;
        }

        public static Result<Person> Create(Guid id, string name, DateTime dateTime)
        {
            return string.IsNullOrEmpty(name)
                ? Result<Person>.Failed("Name is required")
                : Result<Person>.Success(new Person(id, name, dateTime));
        }
    }
}