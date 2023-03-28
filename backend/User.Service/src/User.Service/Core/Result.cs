namespace User.Service.Core
{
    public class Result<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; }

        public static Result<T> IsSuccess(T value) => new Result<T> {Success = true, Data = value};
        public static Result<T> Failure(string error) => new Result<T> {Success = false, Message = error};
    }
}