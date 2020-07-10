using System;

namespace EinTechApplication.Features.GetPersons
{
    public class GetPersonRequest
    {
        public Guid PersonId { get; }

        public GetPersonRequest(Guid personId)
        {
            PersonId = personId;
        }
    }
}