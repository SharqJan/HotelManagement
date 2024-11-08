using System.Collections.Generic;

namespace SMSC.Application.Responses
{
    public sealed class CommonResponse
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; } = new();
        public object Result { get; set; } = new(); 
    }
}
