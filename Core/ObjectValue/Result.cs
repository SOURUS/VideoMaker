namespace Core.ObjectValue
{
    using System.Collections.Generic;

    public class Result<T>
    {
        public Result()
        {
            Errors = new List<Error>();
        }

        public T Data { get; set; }

        public bool Success { get; set; } = true;

        public List<Error> Errors { get; set; }
    }
}
