using FluentResults;

namespace SMSC.Application.Responses
{
    public sealed class ResponseMapper
    {
        public static CommonResponse MapResponse(ResultBase result, object value = null)
        {
            var commonResponse = new CommonResponse
            {
                IsSuccess = result.IsSuccess
            };

            foreach (var error in result.Errors)
            {
                commonResponse.Errors.Add(error.Message);
            }

            commonResponse.Result = value;

            return commonResponse;
        }

        public static CommonResponse MapErrorResponse(string errorMessage, bool isSuccess)
        {
            var commonResponse = new CommonResponse
            {
                IsSuccess = isSuccess
            };
            commonResponse.Errors.Add(errorMessage);
            return commonResponse;
        }
    }
}
