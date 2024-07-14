using Common.Models;
using Newtonsoft.Json;
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
            return JsonConvert.SerializeObject(responseModel, Formatting.Indented);
        }

        public static string CreateExceptionJsonResponse(string response)
        {
            var responseModel = new ResponseModel()
            {
                Response = response,
                IsSuccesful = false,
            };
            return JsonConvert.SerializeObject(responseModel,Formatting.Indented);
        }
    }
}
