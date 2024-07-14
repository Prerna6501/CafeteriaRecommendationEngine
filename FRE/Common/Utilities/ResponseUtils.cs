using Common.Models;
using System.Text.Json;

namespace Common.Utilities
{
    public static class ResponseUtils
    {
        public static string CreateSuccessJsonResponse(string response)
        {
            var responseModel = new ResponseModel()
            {
                Response = response,
                IsSuccesful = true,
            };
            return JsonSerializer.Serialize(responseModel);
        }

        public static string CreateExceptionJsonResponse(string response)
        {
            var responseModel = new ResponseModel()
            {
                Response = response,
                IsSuccesful = false,
            };
            return JsonSerializer.Serialize(responseModel);
        }
    }
}
