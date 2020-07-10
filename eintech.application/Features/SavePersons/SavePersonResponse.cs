using System;

namespace EinTechApplication.Features.SavePersons
{
    public class SavePersonResponse
    {
        public ResponseType ResponseType { get; }
        public string Message { get; }
        public Guid PersonId { get; }

        private SavePersonResponse(ResponseType responseType, string message, Guid personId)
        {
            ResponseType = responseType;
            Message = message;
            PersonId = personId;
        }

        public static SavePersonResponse Failed(string errorMessage)
        {
            return new SavePersonResponse(ResponseType.ValidationError, errorMessage, Guid.Empty);
        }

        public static SavePersonResponse Success(Guid personId)
        {
            return new SavePersonResponse(ResponseType.Created, string.Empty, personId);
        }
    }
}