using EinTechApplication.Features.SavePersons;

namespace EinTechApplication.Features.GetPersons
{
    public class GetPersonResponse
    {
        public ResponseType ResponseType { get; }
        public PersonModel PersonModel { get; }
        public string Message { get; }

        private GetPersonResponse(ResponseType responseType, PersonModel personModel, string message)
        {
            ResponseType = responseType;
            PersonModel = personModel;
            Message = message;
        }

        public static GetPersonResponse Success(PersonModel model)
        {
            return new GetPersonResponse(ResponseType.Found, model, string.Empty);
        }

        public static GetPersonResponse Failed(string errorMessage)
        {
            return new GetPersonResponse(ResponseType.NotFound, default, errorMessage);
        }
    }
}