using System;

namespace EinTechApplication.Features.SavePersons
{
    public class GuidService : IGuidService
    {
        public Guid Get()
        {
            return Guid.NewGuid();
        }
    }
}