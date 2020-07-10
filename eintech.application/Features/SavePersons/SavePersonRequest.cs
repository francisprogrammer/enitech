using System;

namespace EinTechApplication.Features.SavePersons
{
    public class SavePersonRequest
    {
        public string Name { get; }
        public DateTime DateCreated { get; }

        public SavePersonRequest(string name, DateTime dateCreated)
        {
            Name = name;
            DateCreated = dateCreated;
        }
    }
}