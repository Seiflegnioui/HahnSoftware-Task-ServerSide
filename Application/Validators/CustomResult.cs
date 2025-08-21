using FluentValidation;
using MediatR;

namespace hahn.Application.Validators
{
    public class CustomResult<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

        public static CustomResult<T> Ok(T data) => new CustomResult<T> { Success = true, Data = data };
        public static CustomResult<T> Fail(List<string> errors) => new CustomResult<T> { Success = false, Errors = errors };
    }
}