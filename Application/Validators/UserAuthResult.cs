using FluentValidation;
using MediatR;

namespace hahn.Application.Validators
{
    public class UserAuthResult<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string token { get; set; } = string.Empty;
        public List<string>? Errors { get; set; }

        public static UserAuthResult<T> Ok(T data, string token) => new UserAuthResult<T> { Success = true, Data = data, token=token };
        public static UserAuthResult<T> Fail(List<string> errors) => new UserAuthResult<T> { Success = false, Errors = errors };
    }
}