namespace EinTechDomain
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T Value { get; }
        public string ErrorMessage { get; }

        private Result(bool isSuccess, T value, string errorMessage)
        {
            IsSuccess = isSuccess;
            Value = value;
            ErrorMessage = errorMessage;
        }

        public static Result<T> Failed(string errorMessage)
        {
            return new Result<T>(false, default, errorMessage);
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(true, value, string.Empty);
        }
    }
}