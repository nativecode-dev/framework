namespace Common.Models.Models
{
    using System;

    public abstract class Response
    {
        public string ErrorId { get; set; }

        public string ErrorMessage { get; set; }

        public DateTimeOffset ResponseDate { get; set; } = DateTimeOffset.UtcNow;

        public bool Success { get; set; }

        public static T Fail<T>(Exception exception) where T : Response, new()
        {
            return Fail<T>(exception.Message, exception.GetType().Name);
        }

        public static T Fail<T>(string message, string id = default(string)) where T : Response, new()
        {
            return new T { ErrorId = id, ErrorMessage = message, Success = false };
        }

        public static T Succeed<T>() where T : Response, new()
        {
            return new T { Success = true };
        }

        public static T Succeed<T>(Action<T> initializer) where T : Response, new()
        {
            var response = new T { Success = true };
            initializer(response);

            return response;
        }
    }
}